using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    const string AnimatorDead = "isDead", DamageObject = "Damage";

    public static GameManager gameManager;
    public Animator animator;
    
    private bool dead = false;

    public void Awake()
    {
        if( gameManager != null && gameManager != this )
            Destroy(this.gameObject);
        gameManager = this;
    }

    public void Update()
    {
        if (IsDead())
        {
            animator.SetBool(AnimatorDead, true);
        }
    }

    public bool IsDead()
    {
        return dead;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == DamageObject)
        {
            dead = true;
        }
    }
}
