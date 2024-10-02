using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mov : MonoBehaviour
{
    public float speed;
    public Animator animator;
    private Rigidbody2D rb;
    private float playerDirection, playerOrientation;


    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        RunMovement();
        ChangeGravity();
    }

    public void RunMovement()
    {
        const string IsMoving = "isMoving";
        const int flatAngle = 180, nullAngle = 0;

        if (Input.GetKey(KeyCode.D) || Input.GetKey(KeyCode.A))
        {
            playerDirection = Input.GetKey(KeyCode.D) ? speed : -speed;
            transform.position = transform.position + new Vector3(playerDirection, 0, 0) * Time.deltaTime;

            //ROTACION DEL PERSONAJE
            playerOrientation = Input.GetKey(KeyCode.D) ? nullAngle : flatAngle;
            transform.rotation =   ;

            animator.SetBool(IsMoving, true);
        }

        if (Input.GetKeyUp(KeyCode.D) || Input.GetKeyUp(KeyCode.A))
            animator.SetBool(IsMoving, false);
    }

    public void ChangeGravity()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            rb.gravityScale = -rb.gravityScale;
            transform.Rotate(180, 0, 0);
        }
    }
}
