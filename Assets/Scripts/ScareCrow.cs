using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScareCrow : MonoBehaviour {
    public float timeBetweenActions;
    public float timeForTheNextMove;
    public float rangeToAttack;
    public float speed;
    private Vector2 movementDirection;
    public enum ScareCrowState {
        PASSIVE,
        MOVING,
        ATTACKING,
        REACTING,
        SCARE_CROW_STATE_SIZE
    };
    public ScareCrowState scareCrowState; 

    private GameObject player;
    private Bar playerBeerBar;
	// Use this for initialization
	void Start () {
        scareCrowState = ScareCrowState.PASSIVE;
        timeForTheNextMove = timeBetweenActions;
        player= GameObject.Find("Player");
        if (null == player) {
            print("Ue");
        }
        playerBeerBar = GameObject.Find("BeerBar").GetComponent<Bar>();



    }

    // Update is called once per frame
    void Update () {
        timeForTheNextMove -= Time.deltaTime;
        if (timeForTheNextMove <= 0) {
            float contestResult = Random.Range(0f, 1f);
            if (contestResult < playerBeerBar.CurrentValue / playerBeerBar.TotalValue)
            {
                movementDirection = (player.transform.position - transform.TransformDirection(transform.position));
                if (movementDirection.magnitude < rangeToAttack)
                {
                    scareCrowState = ScareCrowState.ATTACKING;
                    timeForTheNextMove = 2 * timeBetweenActions;
    //colocar aqui código para atacar, esse ataque deve durar timeBetweenActions para acontecer para que depois o espantalho fique parado por um tempo
                }
                else
                {
                    scareCrowState = ScareCrowState.MOVING;
                    timeForTheNextMove = 2 * timeBetweenActions;
                    //colocar aqui
                }
            }
            else
            {
                scareCrowState = ScareCrowState.PASSIVE;
                timeForTheNextMove = timeBetweenActions;
            }
        }
        if (scareCrowState == ScareCrowState.ATTACKING || scareCrowState == ScareCrowState.REACTING) {
            if (timeForTheNextMove > timeBetweenActions)
                {
                    //colocar aqui código para atacar, esse ataque deve durar timeBetweenActions para acontecer para que depois o espantalho fique parado por um tempo
                }
                else
                {
                    scareCrowState = ScareCrowState.PASSIVE;
                }
        }
        else if (scareCrowState == ScareCrowState.MOVING) {
            if (timeForTheNextMove > timeBetweenActions)
            {
                transform.Translate(movementDirection * speed);
            }
            else {
                scareCrowState = ScareCrowState.PASSIVE;
            }
        }
    }
}
