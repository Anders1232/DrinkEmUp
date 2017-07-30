using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerController : MonoBehaviour {
    public Transform cameraTransform;
    public float cameraExtraSlide;
    public float cameraSlideSlow;

    public const float MOVEMENT_MAX_X = 6f;
    public const float MOVEMENT_MIN_X = -7.5f;
    public const float MOVEMENT_MAX_Y = -1.0f;
    public const float MOVEMENT_MIN_Y = -3.5f;
    public GameObject drunkIdle;
    public GameObject drunkMoving;
    public GameObject Attacking;
    public GameObject dying;
    public GameObject dizzy;

    public float speed;
    public CircleCollider2D circleCollider;
    public Collider2D moveLimits;
    public GameObject person;

    public Bar healthBar;
    public Bar beerBar;

    public float attackAnimationTime;
    public int numberOfSubSpritesInAttacking;
    public int spriteWhichTheAttackHappens;
    public float timeRemainingInTheAttack;

    public float dyingAnimationTime;
    public float dyingAnimationTimeCounter;

    public float timeThinkingAboutWhatToDo;
    public float timeBetweenThinkies;
    public float timeBetweenThinkiesCounter;

    public Vector2 forcedMoveDirection;

    private bool damageAlreadyDone;
    public float damagePerHit;

    public float rangeToAttack;


    public enum PlayerState
    {
        MOVING,
        WAITING,
        ATTACKING,
        DYING,
        CONFUSED,
        FORCE_MOVING,
        SIZE
    };

    public PlayerState playerState;

    // Use this for initialization
    void Start () {
        playerState = PlayerState.WAITING;
        timeBetweenThinkiesCounter = timeBetweenThinkies;

    }

    void Translate(Vector2 movement) { 
        person.transform.Translate(movement);
        if (person.transform.position.x > MOVEMENT_MAX_X + cameraTransform.position.x)
        {
            person.transform.Translate(new Vector2(MOVEMENT_MAX_X + cameraTransform.position.x - person.transform.position.x, 0f));
            cameraTransform.Translate(new Vector2(cameraExtraSlide, 0f));
        }
        else if (person.transform.position.x < MOVEMENT_MIN_X + cameraTransform.position.x)
        {
            person.transform.Translate(new Vector2(MOVEMENT_MIN_X + cameraTransform.position.x - person.transform.position.x, 0f));
            cameraTransform.transform.Translate(new Vector2(-cameraSlideSlow, 0f));
        }

        if (person.transform.position.y > MOVEMENT_MAX_Y)
        {
            person.transform.Translate(new Vector2(0f, MOVEMENT_MAX_Y - person.transform.position.y));
        }
        else if (person.transform.position.y < MOVEMENT_MIN_Y)
        {
            person.transform.Translate(new Vector2(0f, MOVEMENT_MIN_Y - person.transform.position.y));
        }
    }

    // Update is called once per frame
    void FixedUpdate () {
        float horizontalMove = Input.GetAxis("Horizontal");
        float verticalMove = Input.GetAxis("Vertical");
        PlayerState newStatus= PlayerState.WAITING;
        timeBetweenThinkiesCounter -= Time.fixedDeltaTime;

        if (playerState == PlayerState.FORCE_MOVING)
        {
            if (healthBar.CurrentValue <= 0)
            {
                newStatus = PlayerState.DYING;
            }
            else
            {
                newStatus = PlayerState.FORCE_MOVING;
                Translate(forcedMoveDirection * speed);
//                timeBetweenThinkiesCounter -= timeBetweenThinkies;
                if (0 >= timeBetweenThinkiesCounter) {
                    timeBetweenThinkiesCounter = timeBetweenThinkies;
                    newStatus = PlayerState.WAITING;
                    print("Devo parar de andar");
                }
            }
        }
        else if (playerState == PlayerState.WAITING)
        {
            if (healthBar.CurrentValue <= 0)
            {
                newStatus = PlayerState.DYING;
            }
            else if (Input.GetKeyDown("e"))
            {
                newStatus = PlayerState.ATTACKING;
                damageAlreadyDone = false;
            }
            else if (horizontalMove != 0 || verticalMove != 0)
            {
                newStatus = PlayerState.MOVING;
            }
            else
            {
                newStatus = PlayerState.WAITING;
            }

            Translate(new Vector2(horizontalMove, verticalMove / 2) * speed);

            if (circleCollider.IsTouching(moveLimits))
            {
                print("Aqui!");
            }

        }
        else if (playerState == PlayerState.MOVING)
        {
            if (healthBar.CurrentValue <= 0)
            {
                newStatus = PlayerState.DYING;
            }
            else if (horizontalMove != 0f || verticalMove != 0f)
            {
                if (Input.GetKeyDown("e"))
                {
                    newStatus = PlayerState.ATTACKING;
                }
                else
                {
                    Translate(new Vector2(horizontalMove, verticalMove / 2) * speed);
                    newStatus = PlayerState.MOVING;
                }
            }
            else
            {
                newStatus = PlayerState.WAITING;
            }
        }
        else if (playerState == PlayerState.ATTACKING)
        {
            if (healthBar.CurrentValue <= 0)
            {
                newStatus = PlayerState.DYING;
            }
            else
            {
                newStatus = PlayerState.ATTACKING;
                Translate(new Vector2(0f, 0f));
                timeRemainingInTheAttack -= Time.fixedDeltaTime;
                float timeWindowOfStartOfTheDamage = attackAnimationTime - (float)spriteWhichTheAttackHappens / (float)numberOfSubSpritesInAttacking;
                float timeWindowOfEndOfTheDamage = attackAnimationTime - (float)spriteWhichTheAttackHappens + 1 / (float)numberOfSubSpritesInAttacking;
                if (timeRemainingInTheAttack < timeWindowOfStartOfTheDamage && timeRemainingInTheAttack > timeWindowOfEndOfTheDamage)
                {
                    print("Causar dano");
                    if (!damageAlreadyDone)
                    {
                        GameObject[] enemies = GameObject.FindGameObjectsWithTag("Enemy");
                        foreach (GameObject obj in enemies)
                        {
                            Vector3 movementDirection3D = (transform.position - obj.transform.position);
                            Vector2 movementDirection = new Vector2(movementDirection3D.x, movementDirection3D.y);
                            if (movementDirection.magnitude < rangeToAttack)
                            {
                                obj.GetComponent<ScareCrow>().life -= damagePerHit;
                            }
                        }
                        damageAlreadyDone = true;
                    }
                }
                if (timeRemainingInTheAttack <= 0)
                {
                    newStatus = PlayerState.WAITING;
                }
            }
        }
        else if (playerState == PlayerState.DYING)
        {
            newStatus = PlayerState.DYING;
            Translate(new Vector2(0f, 0f));
            dyingAnimationTimeCounter += Time.fixedDeltaTime;
            if (dyingAnimationTimeCounter >= dyingAnimationTime)
            {
                print("VocE mOrrEU");
                GameOver();
                //Destroy(gameObject);
            }
        }
        else if (playerState == PlayerState.CONFUSED)
        {
            //            timeBetweenThinkiesCounter -= Time.fixedDeltaTime;
            //            print("Estou parado pensando na vida");
            Translate(new Vector2(0f, 0f));
            if (timeBetweenThinkiesCounter <= 0)
            {
                print("Decidindo o q farei");
                int randonNumber = (int)(Random.value * 100);
                if (randonNumber < 50)
                {
                    //            MOVING,
                    newStatus = PlayerState.FORCE_MOVING;
                    float angle = Random.value * 360;
                    forcedMoveDirection = new Vector2(Mathf.Cos(angle), Mathf.Sin(angle));
                    Translate(forcedMoveDirection * speed);
                    print("Vou me mover");
                }
                else if (randonNumber < 75)
                {
                    newStatus = PlayerState.ATTACKING;
                    print("Vou atacar");
                }
                else
                {
                    newStatus = PlayerState.CONFUSED;
                    timeBetweenThinkiesCounter = timeBetweenThinkies;
                    print("Vou continuar confuso");
                }
                timeBetweenThinkiesCounter = timeBetweenThinkies;
            }
            if (newStatus == PlayerState.WAITING)
            {
                newStatus = PlayerState.CONFUSED;
            }
        }
        if (timeBetweenThinkiesCounter <= 0f && (  playerState == PlayerState.WAITING || playerState == PlayerState.MOVING ) )
        {
            print("Chegou aqui");
            bool ChangeAction = ( Random.Range(0f, beerBar.TotalValue) < (beerBar.CurrentValue / 3*2) );
            timeBetweenThinkiesCounter = timeThinkingAboutWhatToDo;
            if (ChangeAction)
            {
                print("Estás confuso!");
                newStatus = PlayerState.CONFUSED;
                timeBetweenThinkiesCounter = timeBetweenThinkies;

            }
        }


        if (newStatus != playerState)
        {
            if (newStatus == PlayerState.ATTACKING)
            {
                drunkIdle.SetActive(false);
                drunkMoving.SetActive(false);
                dying.SetActive(false);
                Attacking.SetActive(true);
                dizzy.SetActive(false);
                timeRemainingInTheAttack = attackAnimationTime;
            }
            else if (newStatus == PlayerState.MOVING)
            {
                drunkIdle.SetActive(false);
                drunkMoving.SetActive(true);
                dying.SetActive(false);
                Attacking.SetActive(false);
                dizzy.SetActive(false);
            }
            else if (newStatus == PlayerState.FORCE_MOVING)
            {
                print("newstatus = forcemoving");
                drunkIdle.SetActive(false);
                drunkMoving.SetActive(true);
                dying.SetActive(false);
                Attacking.SetActive(false);
                dizzy.SetActive(true);
            }
            else if (newStatus == PlayerState.DYING)
            {
                drunkIdle.SetActive(false);
                drunkMoving.SetActive(false);
                dying.SetActive(true);
                Attacking.SetActive(false);
                dizzy.SetActive(false);
                dyingAnimationTimeCounter = 0;
            }
            else if (newStatus == PlayerState.WAITING)
            {
                drunkIdle.SetActive(true);
                drunkMoving.SetActive(false);
                dying.SetActive(false);
                Attacking.SetActive(false);
                dizzy.SetActive(false);
            }
            else if (newStatus == PlayerState.CONFUSED)
            {
                drunkIdle.SetActive(true);
                drunkMoving.SetActive(false);
                dying.SetActive(false);
                Attacking.SetActive(false);
                dizzy.SetActive(true);
                timeBetweenThinkiesCounter = timeThinkingAboutWhatToDo;
            }
        }


        playerState = newStatus;
    }

    void GameOver()
    {
        //Time.timeScale = 0;

        if (Input.GetKeyDown("r")) {
            Application.LoadLevel(Application.loadedLevel);
        }
        else if (Input.GetKeyDown("q"))
            {
                // save any game data here
                #if UNITY_EDITOR
                // Application.Quit() does not work in the editor so
                // UnityEditor.EditorApplication.isPlaying need to be set to false to end the game
                    UnityEditor.EditorApplication.isPlaying = false;
                #else
                    Application.Quit();
                #endif
        }

    }
}
