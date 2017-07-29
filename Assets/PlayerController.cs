using UnityEngine;
using System.Collections;



public class PlayerController : MonoBehaviour {
    public const float MOVEMENT_MAX_X = 6f;
    public const float MOVEMENT_MIN_X = -7.5f;
    public const float MOVEMENT_MAX_Y = 1.7f;
    public const float MOVEMENT_MIN_Y = -2.7f;


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
        person.transform.Translate(new Vector2(horizontalMove, verticalMove)*speed);
        if (person.transform.position.x > MOVEMENT_MAX_X) {
            person.transform.Translate(new Vector2(MOVEMENT_MAX_X - person.transform.position.x, 0f));
        }
        else if (person.transform.position.x < MOVEMENT_MIN_X)
        {
            person.transform.Translate(new Vector2(MOVEMENT_MIN_X - person.transform.position.x, 0f));
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
