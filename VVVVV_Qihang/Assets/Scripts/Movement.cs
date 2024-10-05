using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mov : MonoBehaviour
{
    const string IsMoving = "isMoving", IsJumping = "isJumping";
    const int FlatAngle = 180, NullAngle = 0;

    public float speed;
    public Animator animator;

    private Rigidbody2D rb;
    private bool isGravityInverted;


    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        isGravityInverted = false;
    }

    // Update is called once per frame
    void Update()
    {
        RunMovement();
        ChangeGravity();
    }

    public void RunMovement()
    {
        float playerDirection, playerOrientation;

        if (Input.GetKey(KeyCode.D) || Input.GetKey(KeyCode.A))
        {
            //MOVEMENT
            playerDirection = Input.GetKey(KeyCode.D) ? speed : -speed;
            transform.position += new Vector3(playerDirection, NullAngle, NullAngle) * Time.deltaTime;

            //ROTATION
            //Si el personaje esta invertido, se le sumara 180 grados por estar en la inversa.
            playerOrientation = (playerDirection > 0 ? NullAngle : FlatAngle) + (isGravityInverted ? FlatAngle : NullAngle);
            transform.rotation = Quaternion.Euler(transform.eulerAngles.x, playerOrientation, transform.eulerAngles.z);

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
            isGravityInverted = !isGravityInverted;
            transform.rotation = Quaternion.Euler(FlatAngle, transform.eulerAngles.y, transform.eulerAngles.z);
            animator.SetBool(IsJumping, true);
        }
        //Si el personaje esta en el suelo, se le quita la animacion de salto. UTILIZA RAYCAST

    }
}
