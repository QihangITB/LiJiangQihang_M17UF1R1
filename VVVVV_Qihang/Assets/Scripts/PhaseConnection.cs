using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PhaseConnection : MonoBehaviour
{
    private const string PlayerTag = "Player";
    private const string ConnectionGroup = "Connection"; //GameObject que contiene los puntos de conexión.
    private const string EntranceName = "Entrance"; //GameObject de entrada que esta dentro de "Connection".
    private const string ExitName = "Exit"; //GameObject de salida que esta dentro de "Connection".


    [SerializeField] private GameObject entrance;
    [SerializeField] private GameObject exit;
    [SerializeField] private GameObject previousPhase;
    [SerializeField] private GameObject nextPhase;

    private Collider2D playerCollision;
    private Collider2D entranceCollision;
    private Collider2D exitCollision;

    private void Awake()
    {
        playerCollision = GameObject.FindGameObjectWithTag(PlayerTag).GetComponent<Collider2D>();
        entranceCollision = entrance.GetComponent<Collider2D>();
        exitCollision = exit.GetComponent<Collider2D>();
    }

    void Update()
    {
        //Si hay una siguiete fase, accede a ella.
        if (playerCollision.IsTouching(exitCollision) && nextPhase != null)
        {
            ChangePhase(nextPhase);
            PlayerSpawn(nextPhase, EntranceName); //La salida esta conectada con la entrada de la fase siguiente.
        }

        //Si hay una fase anterior, accede a ella.
        if (playerCollision.IsTouching(entranceCollision) && previousPhase != null)
        {
            ChangePhase(previousPhase);
            PlayerSpawn(previousPhase, ExitName); //La entrada esta conectada con la salida de la fase anterior.
        }
    }

    //Activa el GameObject de la nueva fase (puede ser la anterior o la siguiente) y desactiva el actual.
    private void ChangePhase(GameObject newPhase)
    {
        this.gameObject.SetActive(false);
        newPhase.SetActive(true);
    }

    //Coloca al jugador en la entrada/salida de la nueva fase, dependiendo de si se accede a la fase anterior o siguiente.
    private void PlayerSpawn(GameObject newPhase, string spawnPoint)
    {
        GameObject player = GameObject.FindGameObjectWithTag(PlayerTag);
        Transform spawn = newPhase.transform.Find(ConnectionGroup).Find(spawnPoint);

        if (spawn != null)
        {
            player.transform.position = spawn.position;

            //Guardamos el checkpoint de la nueva fase en el GameManager.
            GameManager.manager.SetCheckPoint(spawn);
        }
    }
}
