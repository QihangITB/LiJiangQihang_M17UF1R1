using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShooterEnemy : MonoBehaviour
{
    const string SpawnerName = "Spawner",  IgnoreRaycastLayer = "Ignore Raycast";
    const int HalfDivider = 2;
    const float CollisionOffset = 0.1f;

    public GameObject projectilePrefab;
    public float speed;

    private Stack<GameObject> projectilePool;
    private GameObject projectileSpawner, projectile;
    private Collider2D collisionObject;
    private LayerMask ignoreRaycastMask;
    private float collisionRadius;

    void Start()
    {
        projectilePool = new Stack<GameObject>();
        projectileSpawner = transform.Find(SpawnerName).gameObject;
        projectile = Instantiate(projectilePrefab, projectileSpawner.transform.position, Quaternion.identity);

        //Como queremos el radeo, dividimos el tamaño entre 2.
        collisionRadius = (projectile.GetComponent<CapsuleCollider2D>().size.x / HalfDivider) - CollisionOffset;

        //Cogemos la capa que del jugador que ignora el raycast.
        ignoreRaycastMask = LayerMask.GetMask(IgnoreRaycastLayer);
    }

    void Update()
    {
        if(projectile != null)
        {
            ProjectileMovement();

            //Solo colisionara con la capa "Ignore Raycast" porque es la del jugador, por lo tanto, crearemos paredes especificas con esta capa para que también colisione con ellas.
            collisionObject = Physics2D.OverlapCircle(projectile.transform.position, collisionRadius, ignoreRaycastMask);

            //Dibujamos el círculo de colisión del proyectil para visualizarlo en Scene, utilizando una funcion de CHATGPT.
            DrawDebugCircle(projectile.transform.position, collisionRadius, Color.red);

            this.ProjectileCollision(projectile, collisionObject);
        }
    }

    private void ProjectileMovement()
    {
        projectile.transform.position += Vector3.right * speed * Time.deltaTime;
    }

    private void ProjectileCollision(GameObject projectile, Collider2D collider)
    {
        //Si el proyectil colisiona con algo que no sea si mismo, lo guardamos y lo desactivamos al Stack.
        if (collider != null && collider != projectile)
        {
            Debug.Log("Projectile collision with: " + collider.name);
            this.PushProjectile(projectile);
        }
    }

    //Este metodo se llamara desde el Animator con un evento.
    public void ProjectileActivation()
    {
        if (!this.IsStackVoid())
        {
            Debug.Log(projectilePool.Count);
            projectile = this.PopProjectile();
            if (projectile != null)
            {
                projectile.transform.position = projectileSpawner.transform.position;
                projectile.SetActive(true);
            }
        }
    }

    public void PushProjectile(GameObject projectile)
    {
        //Comprobamos que el stack esté vacio porque solo queremos 1 proyectil.
        if (this.IsStackVoid())
        {
            //Mete los proyectiles dentro del stack.
            projectilePool.Push(projectile);

            //Desactiva el GameObject del proyectil.
            projectile.SetActive(false);
        }
    }

    public GameObject PopProjectile()
    {
        //Devuelve el ultimo proyectil que se metio en el stack.
        return projectilePool.Pop();
    }

    private bool IsStackVoid()
    {
        //Comprueba si hay proyectiles dentro del stack.
        return projectilePool.Count == 0;
    }





    //METODO DEL CHATGPT PARA VISUALIZAR EL CIRCULO DE COLISION DEL PROYECTIL EN SCENE.
    void DrawDebugCircle(Vector3 position, float radius, Color color)
    {
        float angleStep = 360f / 20;

        Vector3 lastPoint = position + new Vector3(Mathf.Cos(0), Mathf.Sin(0), 0) * radius;

        for (int i = 1; i <= 20; i++)
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
