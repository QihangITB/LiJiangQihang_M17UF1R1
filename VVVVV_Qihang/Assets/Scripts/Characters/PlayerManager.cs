using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerManager : MonoBehaviour
{
    const string AnimatorDead = "isDead", AnimatorCheckpoint = "CheckpointActivation";
    const string DamageTag = "Damage", CheckpointTag = "Checkpoint";
    const float SoundVolum = 1f;

    public static PlayerManager player;
    public Animator playerAnimator;
    public AudioClip deathSound;

    private Transform checkPoint;
    private Rigidbody2D rb;
    private SpriteRenderer spriteRenderer;
    private AudioSource audioSource;
    private bool canMove = true, lastSpriteFlipY;
    private float lastGravity;

    public void Awake()
    {
        if (player != null && player != this)
        {
            Destroy(this.gameObject);
        }
        else
        {
            player = this;
        }
    }
    public void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        audioSource = GetComponent<AudioSource>();
    }
    public bool CanPlayerMove()
    {
        return canMove;
    }

    public void SetCheckPoint(Transform newCheckPoint)
    {
        checkPoint = newCheckPoint;
    }

    public Transform GetCheckPoint()
    {
        return checkPoint;
    }

    private void SaveCheckpoint(Collider2D collision)
    {
        //Activamos la animacion del checkpoint.
        Animator checkpointAnimator = collision.gameObject.GetComponent<Animator>();
        checkpointAnimator.SetTrigger(AnimatorCheckpoint);

        //Guardamos los datos del juagdor.
        SetCheckPoint(collision.transform);
        lastGravity = rb.gravityScale;
        lastSpriteFlipY = spriteRenderer.flipY;
        //Debug.Log("Checkpoint: " + transform.position + " " + lastGravity); ;
    }
    public void Respawn()
    {
        //El jugador aparece en el checkpoint con los datos que le pertenece.
        rb.gravityScale = lastGravity;
        spriteRenderer.flipY = lastSpriteFlipY;
        transform.position = GetCheckPoint().position;

        //Permitimos sus movimientos
        canMove = true;
        //Debug.Log("Respawn: " + transform.position + " " + lastGravity);
    }

    private void Death()
    {
        //Ejecuta el sonido una vez mientras se activa la animacion.
        audioSource.PlayOneShot(deathSound, SoundVolum);
        playerAnimator.SetBool(AnimatorDead, true);
        canMove = false;
        //Debug.Log("Death: " + transform.position + " " + lastGravity);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag(DamageTag))
            Death();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag(DamageTag))
            Death();
        else if (collision.gameObject.CompareTag(CheckpointTag))
        {
            SaveCheckpoint(collision);
        }
    }
}
