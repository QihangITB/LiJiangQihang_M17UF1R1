using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;

public class MovingEnemy : MonoBehaviour
{
    const string PlayerTag = "Player", AnimatorMoving = "isMoving";
    const int Double = 2;
    const float MinimumDistance = 0.5f;

    public Animator animator;
    public float speed;
    public float waitingTime;
    public GameObject pointA, pointB;

    private SpriteRenderer sprite;
    private bool canMove = true;
    private Vector2 direction;
    private float distance;
    private RaycastHit2D hit;

    void Start()
    {
        sprite = GetComponent<SpriteRenderer>();
        StartCoroutine(MovingBehaviour());
    }

    void Update()
    {
        AnimationHandler();
        Debug.Log(canMove);
    }

    private IEnumerator MovingBehaviour()
    {
        GameObject destination = pointA;

        //La corrutina se mantine activo hasta que lo indiquemos nosotros.
        while (true)
        {
            yield return MoveTo(destination.transform);
            yield return new WaitForSeconds(waitingTime);

            //Al cambiar de destino, cambiamos la dirección del sprite.
            destination = (destination == pointA) ? pointB : pointA;
            sprite.flipX = !sprite.flipX;
        }
    }

    private IEnumerator MoveTo(Transform destination)
    {
        while (Vector2.Distance(transform.position, destination.position) > MinimumDistance)
        {
            //Mueve hacia el destino si su movilidad esta activado (dependiendo de si el jugador esta cerca).
            if(canMove)
            {
                direction = (destination.position - transform.position).normalized;
                transform.position += (Vector3) direction * SpeedHandler(destination) * Time.deltaTime;

                //Debug.Log("Moving to: " + destination.name);
                //Debug.Log("Speed: " + SpeedHandler(destination));
            }
            else
            {
                //Espera un tiempo antes de poder moverse.
                yield return new WaitForSeconds(waitingTime);
                canMove = true;
            }
            //Espera un frame para que no se mueva tan rápido.
            yield return null;
        }
        //Al llegar al destino, se bloquea su movilidad, pero más tarde volvera a activarse.
        canMove = false;
    }

    private float SpeedHandler(Transform destination)
    {
        //Si el enemigo detecta al jugador, su velocidad se duplica.
        return PlayerDetection(destination) ? speed * Double : speed;
    }

    private bool PlayerDetection(Transform destiantion)
    {
        direction = (destiantion.position - transform.position).normalized;
        distance = Vector2.Distance(transform.position, destiantion.position);

        hit = Physics2D.Raycast(transform.position, direction, distance);

        //Lo dibuja en la escena
        Debug.DrawRay(transform.position, direction * distance, Color.red);

        //Si no detecta nada, devuelve false y la funcion se acaba aqui.
        if (hit.collider == null) return false;

        //En este caso nos referimos si el enemigo esta cerca del jugador.
        bool isPlayerNear = Vector2.Distance(transform.position, hit.collider.transform.position) < MinimumDistance;
        canMove = !isPlayerNear;

        //Si detecta al jugador y no esta cerca, devuelve true.
        return (hit.collider.CompareTag(PlayerTag) && !isPlayerNear) ? true : false;
    }

    private void AnimationHandler()
    {
        if(HasAnimatorParameter(AnimatorMoving))
        {
            //Si el enemigo puede moverse, activa la animación de movimiento.
            animator.SetBool(AnimatorMoving, canMove);
        }
    }

    //Comprueba si el Animator tiene el parametro que le especificamos.
    private bool HasAnimatorParameter(string parameterName)
    {
        foreach (AnimatorControllerParameter parameter in animator.parameters)
        {
            if (parameter.name == parameterName)
                return true;
        }
        return false;
    }
}
