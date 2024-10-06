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
    private RaycastHit2D hit;
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
        Vector2 downRay = transform.TransformDirection(Vector2.down);
        hit = Physics2D.Raycast(transform.position, downRay, 1f);
        Debug.DrawRay(transform.position, downRay * 1f, Color.red); //Muestra el raycast en el editor

        if (hit.collider != null)
        {
            animator.SetBool(IsJumping, false);
            ChangeGravity();
        }

        RunMovement();
    }

    public void RunMovement()
    {
        float playerDirection, playerOrientation;

        if (Input.GetKey(KeyCode.D) || Input.GetKey(KeyCode.A))
        {
            //MOVIMIENTO
            playerDirection = Input.GetKey(KeyCode.D) ? speed : -speed;
            transform.position += new Vector3(playerDirection, NullAngle, NullAngle) * Time.deltaTime;

            //ROTACION
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

            //Cambiamos la rotacion del personaje al cambiar la gravedad
            transform.rotation = Quaternion.Euler(FlatAngle, transform.eulerAngles.y, transform.eulerAngles.z);

            animator.SetBool(IsJumping, true);
        }
    }
}
