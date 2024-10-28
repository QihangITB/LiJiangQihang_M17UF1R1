using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Todo el tema de ajustes del juego.
public class GameManager : MonoBehaviour
{
    public static GameManager manager;

    public void Awake()
    {
        if(manager != null && manager != this )
            Destroy(this.gameObject);

        manager = this;
    }
}
