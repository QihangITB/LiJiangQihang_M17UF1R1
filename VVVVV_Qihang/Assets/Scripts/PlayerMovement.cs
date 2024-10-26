using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    const string AnimatorMoving = "isMoving", AnimatorJumping = "isJumping", SurfaceLayer = "Surface";
    const int OpenAngle = 180, CloseAngle = 0, HalfDivider = 2, Zero = 0;
    const float RayDistance = 1f;

    public float speed;
    public Animator animator;

    private Rigidbody2D rb;
    private bool isGravityInverted;
    private LayerMask surfaceLayer;


    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        surfaceLayer = LayerMask.GetMask(SurfaceLayer);
        isGravityInverted = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (!GameManager.manager.CanPlayerMove())
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
            //Si el personaje esta invertido, se le sumara 180 grados por estar en la inversa, de esta manera las direcciones X y Y coinciden con el sprite.
            playerOrientation = (playerDirection > Zero ? CloseAngle : OpenAngle) + (isGravityInverted ? OpenAngle : CloseAngle);
            transform.rotation = Quaternion.Euler(transform.eulerAngles.x, playerOrientation, transform.eulerAngles.z);

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
            rb.gravityScale = -rb.gravityScale;
            isGravityInverted = !isGravityInverted;

            //Cambiamos la rotacion del personaje al cambiar la gravedad
            transform.rotation = Quaternion.Euler(OpenAngle, transform.eulerAngles.y, transform.eulerAngles.z);

            animator.SetBool(AnimatorJumping, true);
        }
    }

    private bool OnTheFloor()
    {
        //Siempre indica el vector que va hacia abajo del personaje.
        Vector2 downDirection = transform.TransformDirection(Vector2.down);

        RaycastHit2D hit = Physics2D.Raycast(transform.position, downDirection, RayDistance, surfaceLayer);

        //Muestra el raycast en el editor de escenas.
        Debug.DrawRay(transform.position, downDirection * RayDistance, Color.red);

        return hit.collider != null;
    }
}
