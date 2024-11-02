using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class PlayerMovement : MonoBehaviour
{
    const string AnimatorMoving = "isMoving", AnimatorJumping = "isJumping", SurfaceLayer = "Surface";
    const int OpenAngle = 180, CloseAngle = 0, Zero = 0;
    const float RayDistance = 0.8f, SoundVolume = 1f;

    public float speed;
    public Animator animator;
    public AudioClip changeGravitySound;

    private Rigidbody2D rb;
    private SpriteRenderer spriteRenderer;
    private AudioSource audioSource;
    private bool isGravityInverted;
    private LayerMask surfaceLayer;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        surfaceLayer = LayerMask.GetMask(SurfaceLayer);
        isGravityInverted = false;
        audioSource = GetComponent<AudioSource>();
    }

    void Update()
    {
        if (!PlayerManager.player.CanPlayerMove())
        {
            DisableRunAnimation();
            return;
        }

        if (OnTheFloor())
        {
            animator.SetBool(AnimatorJumping, false);
            ChangeGravity();
        }

        RunMovement();
    }

    private void RunMovement()
    {
        float playerDirection, playerOrientation;

        if (Input.GetKey(KeyCode.D) || Input.GetKey(KeyCode.A))
        {
            //MOVIMIENTO
            playerDirection = Input.GetKey(KeyCode.D) ? speed : -speed;
            transform.position += new Vector3(playerDirection, Zero, Zero) * Time.deltaTime;

            //ROTACION
            playerOrientation = (playerDirection > Zero ? CloseAngle : OpenAngle);
            transform.rotation = Quaternion.Euler(Zero, playerOrientation, Zero);

            animator.SetBool(AnimatorMoving, true);
        }

        if (Input.GetKeyUp(KeyCode.D) || Input.GetKeyUp(KeyCode.A))
            DisableRunAnimation();
    }

    private void DisableRunAnimation()
    {
        animator.SetBool(AnimatorMoving, false);
    }

    private void ChangeGravity()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            //Ejecuta el sonido una vez.
            audioSource.PlayOneShot(changeGravitySound, SoundVolume);

            rb.gravityScale = -rb.gravityScale;
            isGravityInverted = !isGravityInverted;

            //Cambiamos giramos el srpite del personaje.
            spriteRenderer.flipY = !spriteRenderer.flipY;

            animator.SetBool(AnimatorJumping, true);
        }
    }

    private bool OnTheFloor()
    {
        RaycastHit2D hitDown = Physics2D.Raycast(transform.position, Vector2.down, RayDistance, surfaceLayer);
        RaycastHit2D hitUp = Physics2D.Raycast(transform.position, Vector2.up, RayDistance, surfaceLayer);

        //Muestra el raycast en el editor de escenas.
        Debug.DrawRay(transform.position, Vector2.down * RayDistance, Color.red);
        Debug.DrawRay(transform.position, Vector2.up * RayDistance, Color.red);

        return hitDown.collider != null || hitUp.collider != null;
    }
}
