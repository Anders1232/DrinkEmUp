using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScareCrow : MonoBehaviour {
    public float timeBetweenActions;
    public float timeForTheNextMove;
    public float rangeToAttack;
    private float speed;
    private Vector2 movementDirection;
    public float minSpeed;
    public float maxSpeed;

    public float life;

    public float attackAnimationTime;
    public float attackAnimationTimeCounter;

    public float damagePerHit;

    public SpriteRenderer stillSpriteRenderer;
    public SpriteRenderer walkingSpriteRenderer;
    public SpriteRenderer punchingSpriteRenderer;

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
    private bool damageAlreadyDone;
    public Bar playerLifeBar;
	// Use this for initialization
	void Start () {
        scareCrowState = ScareCrowState.PASSIVE;
        timeForTheNextMove = timeBetweenActions;
        player= GameObject.Find("Player");
        if (null == player) {
            print("Ue");
        }
        playerBeerBar = GameObject.Find("BeerBar").GetComponent<Bar>();
        playerLifeBar = GameObject.Find("LifeBar").GetComponent<Bar>();
        walkingSpriteRenderer.gameObject.SetActive(false);
    }

    // Update is called once per frame
    void Update () {
        if (life == 0) {
            Destroy(gameObject);
        }
        speed = (maxSpeed - minSpeed) * playerBeerBar.CurrentValue / playerBeerBar.TotalValue + minSpeed;
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
                    walkingSpriteRenderer.gameObject.SetActive(false);
                    stillSpriteRenderer.gameObject.SetActive(false);
                    punchingSpriteRenderer.gameObject.SetActive(true);
                    damageAlreadyDone = false;

                    attackAnimationTimeCounter = attackAnimationTime;


                    //colocar aqui código para atacar, esse ataque deve durar timeBetweenActions para acontecer para que depois o espantalho fique parado por um tempo
                }
                else
                {
                    scareCrowState = ScareCrowState.MOVING;
                    timeForTheNextMove = 2 * timeBetweenActions;
                    if (playerBeerBar.CurrentValue > playerBeerBar.TotalValue / 2)
                    {
                        stillSpriteRenderer.gameObject.SetActive(false);
                        walkingSpriteRenderer.gameObject.SetActive(true);
                        punchingSpriteRenderer.gameObject.SetActive(false);
                    }
                    //colocar aqui
                }
            }
            else
            {
                scareCrowState = ScareCrowState.PASSIVE;
                timeForTheNextMove = timeBetweenActions;
                walkingSpriteRenderer.gameObject.SetActive(false);
                stillSpriteRenderer.gameObject.SetActive(true);
                punchingSpriteRenderer.gameObject.SetActive(false);
            }
        }
        if (scareCrowState == ScareCrowState.ATTACKING || scareCrowState == ScareCrowState.REACTING) {
            if (timeForTheNextMove > timeBetweenActions)
            {
                attackAnimationTimeCounter -= Time.deltaTime;
                if(attackAnimationTimeCounter < attackAnimationTime/2)
                {
                    if(!damageAlreadyDone)
                    {
                        playerLifeBar.ChangeValue(-damagePerHit);
                        damageAlreadyDone = true;
                    }
                }
                if(attackAnimationTimeCounter < 0)
                {
                    scareCrowState = ScareCrowState.PASSIVE;
                }
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
