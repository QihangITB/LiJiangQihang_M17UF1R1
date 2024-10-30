using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerManager : MonoBehaviour
{
    const string AnimatorDead = "isDead", AnimatorCheckpoint = "CheckpointActivation";
    const string DamageTag = "Damage", CheckpointTag = "Checkpoint";

    public static PlayerManager player;
    public Animator playerAnimator, checkpointAnimator;

    private Transform checkPoint;
    private bool canMove = true;

    public void Awake()
    {
        if (player != null && player != this)
        {
            Destroy(this.gameObject);
        }
        else
        {
            player = this;
            DontDestroyOnLoad(this.gameObject);
        }
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
    public void Respawn()
    {
        //El jugador aparece en el checkpoint
        playerAnimator.gameObject.transform.position = checkPoint.position;

        //Permitimos sus movimientos
        canMove = true;
    }

    private void Death()
    {
        playerAnimator.SetBool(AnimatorDead, true);
        canMove = false;
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
            SetCheckPoint(collision.gameObject.transform);
            checkpointAnimator.SetTrigger(AnimatorCheckpoint);
            Debug.Log("Checkpoint");
        }
    }
}
