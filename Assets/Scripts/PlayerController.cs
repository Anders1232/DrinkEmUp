using UnityEngine;
using System.Collections;

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
    private bool moving = false;

    public float speed;
    public CircleCollider2D circleCollider;
    public Collider2D moveLimits;
    public GameObject person;

    // Use this for initialization
    void Start () {
	
	}
	
	// Update is called once per frame
	void FixedUpdate () {
        float horizontalMove = Input.GetAxis("Horizontal");
        float verticalMove = Input.GetAxis("Vertical");
        bool newStatusAboutMoving;
        if (horizontalMove == 0 && verticalMove == 0)
        {
            newStatusAboutMoving = false;
        }
        else {
            newStatusAboutMoving = true;
        }

        if (newStatusAboutMoving != moving) {
            if (newStatusAboutMoving)
            {
                drunkIdle.SetActive(false);
                drunkMoving.SetActive(true);
            }
            else {
                drunkIdle.SetActive(true);
                drunkMoving.SetActive(false);
            }
            moving = newStatusAboutMoving;
        }


        person.transform.Translate(new Vector2(horizontalMove, verticalMove/2)*speed);
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

        if (circleCollider.IsTouching(moveLimits)) {
            print("Aqui!");
        }
   }
}
