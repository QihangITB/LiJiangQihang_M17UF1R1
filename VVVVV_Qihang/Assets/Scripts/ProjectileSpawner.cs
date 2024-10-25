using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileSpawner : MonoBehaviour
{
    const string PlayerTag = "Player", IgnoreRaycastLayer = "Ignore Raycast";
    const int HalfDivider = 2;

    public GameObject projectilePrefab;
    public float speed;

    private Stack<GameObject> projectilePool;
    private GameObject projectile;
    private Collider2D objectCollider;
    private float collisionRadius;

    void Start()
    {
        projectilePool = new Stack<GameObject>();
        projectile = Instantiate(projectilePrefab, transform.position + Vector3.right * 1.5f, Quaternion.identity);

        //Como queremos el radeo, dividimos el tamaño entre 2.
        collisionRadius = projectile.GetComponent<CapsuleCollider2D>().size.x / HalfDivider; 
    }

    void Update()
    {
        if(projectile != null)
        {
            projectile.transform.position += Vector3.right * speed * Time.deltaTime;

            //En este caso incluymos tambien la capa que en teoria ignora los Raycast.
            LayerMask mask = LayerMask.GetMask(IgnoreRaycastLayer);
            objectCollider = Physics2D.OverlapCircle(projectile.transform.position, collisionRadius, mask);

            DrawDebugCircle(projectile.transform.position, collisionRadius, 20, Color.red);

            this.ProjectileCollision(projectile, objectCollider);
        }

    }

    public void ProjectileCollision(GameObject projectile, Collider2D collider)
    {
        if (collider != null)
        {
            Debug.Log(collider.name);
            Debug.Log("Collision detected");
            Destroy(projectile);
            //Push(projectile);
        }
    }

    public void Push(GameObject projectile)
    {
        //Mete los proyectiles dentro del stack.
        projectilePool.Push(projectile);
    }

    public GameObject Pop()
    {
        //Si hay proyectiles dentro del stack las devuelve, sino devuelve null.
        return this.Peek() != null ? projectilePool.Pop() : null;
    }

    private GameObject Peek()
    {
        //Comprueba si hay proyectiles dentro del stack.
        return projectilePool.Peek();
    }


    //METODO DEL CHATGPT PARA VISUALIZAR EL CIRCULO DE COLISION DEL PROYECTIL EN SCENE.
    void DrawDebugCircle(Vector3 position, float radius, int segments, Color color)
    {
        float angleStep = 360f / segments;

        Vector3 lastPoint = position + new Vector3(Mathf.Cos(0), Mathf.Sin(0), 0) * radius;

        for (int i = 1; i <= segments; i++)
        {
            float angle = i * angleStep * Mathf.Deg2Rad;
            Vector3 nextPoint = position + new Vector3(Mathf.Cos(angle), Mathf.Sin(angle), 0) * radius;

            // Dibujar una línea entre el punto anterior y el siguiente
            Debug.DrawLine(lastPoint, nextPoint, color);

            // Actualizar el último punto
            lastPoint = nextPoint;
        }
    }
}
