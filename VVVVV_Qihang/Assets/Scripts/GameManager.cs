using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    const string AnimatorDead = "isDead", DamageObject = "Damage";

    public static GameManager manager;
    public Animator animator;

    private Transform checkPoint;
    private bool canMove = true;

    public void Awake()
    {
        if(manager != null && manager != this )
            Destroy(this.gameObject);

        manager = this;
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
        animator.gameObject.transform.position = checkPoint.position;

        //Permitimos sus movimientos
        canMove = true;
    }

    private void Death()
    {
        animator.SetBool(AnimatorDead, true);
        canMove = false;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag(DamageObject))
            Death();
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag(DamageObject))
            Death();
    }
}
