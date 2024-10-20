using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PhaseConnection : MonoBehaviour
{
    private const string PlayerTag = "Player";

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
        }

        //Si hay una fase anterior, accede a ella.
        if (playerCollision.IsTouching(entranceCollision) && previousPhase != null)
        {
            ChangePhase(previousPhase);
        }
    }

    //Activa el GameObject de la nueva fase (puede ser la anterior o la siguiente) y desactiva el actual.
    private void ChangePhase(GameObject newPhase)
    {
        this.gameObject.SetActive(false);
        newPhase.SetActive(true);
    }
}
