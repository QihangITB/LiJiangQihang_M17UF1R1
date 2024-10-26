using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;

public class MovingEnemy : MonoBehaviour
{
    const string PlayerTag = "Player", IgnoreRaycastLayer = "Ignore Raycast";
    const int Double = 2;

    public float speed;
    public float waitingTime;
    public GameObject pointA, pointB;

    private SpriteRenderer sprite;
    private LayerMask ignoreRaycastMask;

    void Start()
    {
        sprite = GetComponent<SpriteRenderer>();
        ignoreRaycastMask = LayerMask.GetMask(IgnoreRaycastLayer);
        StartCoroutine(MovingBehaviour());
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
        while (Vector2.Distance(transform.position, destination.position) > 0.1f)
        {
            //Mueve hacia el destino
            transform.position += (destination.position - transform.position).normalized * SpeedHandler(destination) * Time.deltaTime;
            //Espera un frame para que no se mueva tan rápido.
            yield return null;
        }
    }

    private float SpeedHandler(Transform destination)
    {
        //Si el enemigo detecta al jugador, su velocidad se duplica.
        return PlayerDetection(destination) ? speed * Double : speed;
    }

    private bool PlayerDetection(Transform destiantion)
    {
        Vector2 direction = (destiantion.position - transform.position).normalized;
        float distance = Vector2.Distance(transform.position, destiantion.position);

        RaycastHit2D hit = Physics2D.Raycast(transform.position, direction, distance, ignoreRaycastMask);

        //Lo dibuja en la escena
        Debug.DrawRay(transform.position, direction * distance, Color.red);

        //Si no detecta nada, devuelve false.
        if (hit.collider == null) return false;

        //Si el raycast detecta al jugador y no esta en su posicion, devuelve true.
        return (hit.collider.CompareTag(PlayerTag) && transform.position != hit.collider.transform.position) ? true : false;
    }
}
