using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileSpawner : MonoBehaviour
{
    private Stack<GameObject> projectilePool;
    public GameObject projectilePrefab;

    private void Start()
    {
        projectilePool = new Stack<GameObject>();
        Instantiate(projectilePrefab, transform.position, Quaternion.identity);
    }

    public void Push(GameObject projectile)
    {
        projectilePool.Push(projectile);
    }

    public void Pop()
    {
        projectilePool.Pop().SetActive(true);
    }

    public void Peek()
    {
        projectilePool.Peek();
    }
}
