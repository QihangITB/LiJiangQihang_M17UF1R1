using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PhaseConnection : MonoBehaviour
{
    private const string PlayerTag = "Player";

    [SerializeField] private GameObject entrance;
    [SerializeField] private GameObject exit;
    [SerializeField] private GameObject previousPhase;
    [SerializeField] private GameObject previousPhaseExit;
    [SerializeField] private GameObject nextPhase;
    [SerializeField] private GameObject nextPhaseEntrance;

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
            PlayerSpawn(nextPhase, nextPhaseEntrance.transform); //La salida esta conectada con la entrada de la fase siguiente.
        }

        //Si hay una fase anterior, accede a ella.
        if (playerCollision.IsTouching(entranceCollision) && previousPhase != null)
        {
            ChangePhase(previousPhase);
            PlayerSpawn(previousPhase, previousPhaseExit.transform); //La entrada esta conectada con la salida de la fase anterior.
        }
    }

    //Activa el GameObject de la nueva fase (puede ser la anterior o la siguiente) y desactiva el actual.
    private void ChangePhase(GameObject newPhase)
    {
        this.gameObject.SetActive(false);
        newPhase.SetActive(true);
    }

    //Coloca al jugador en la entrada/salida de la nueva fase, dependiendo de si se accede a la fase anterior o siguiente.
    private void PlayerSpawn(GameObject newPhase, Transform spawnPoint)
    {
        GameObject player = GameObject.FindGameObjectWithTag(PlayerTag);
        if (spawnPoint != null)
            player.transform.position = spawnPoint.position;
    }
}
