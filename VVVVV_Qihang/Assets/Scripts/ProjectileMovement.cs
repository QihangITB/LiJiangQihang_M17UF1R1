using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class ProjectileMovement : MonoBehaviour
{
    public float speed;

    private Rigidbody2D rb;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        Vector3 direction = transform.forward;

        // Calcula la nueva posición
        Vector3 newPosition = transform.position + direction;

        // Asigna la nueva posición
        transform.position = newPosition;
        rb.velocity = newPosition * speed;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision != null)
        {
            Debug.Log("Collision detected");
        }
    }

}
