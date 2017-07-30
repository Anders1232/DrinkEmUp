using UnityEngine;
using System.Collections;

public class Cerveja : MonoBehaviour
{
    public float sinCounter;
    public float sinCounterStep;
    public float shakeAmplitude;
    public float alchoolInBeer;
    private GameObject player;
    public CircleCollider2D playerCollider;
    private CircleCollider2D myCollider;

    private void Start()
    {
        player = GameObject.Find("Person");
        if (null == player) {
            print("Oops");
        }
        playerCollider = player.GetComponentInChildren<CircleCollider2D>();
        myCollider = GetComponent<CircleCollider2D>();
    }

    void FixedUpdate()
    {
        sinCounter += sinCounterStep;
        gameObject.transform.Translate(new Vector2(0, Mathf.Sin(sinCounter)* shakeAmplitude));
        Vector3 myColliderOffset = new Vector3(myCollider.offset.x, myCollider.offset.y);
        Vector3 myPosition = transform.position+ myColliderOffset;

        Vector3 playerColliderOffset = new Vector3(playerCollider.offset.x, playerCollider.offset.y);
        Vector3 playerPosition = player.transform.position + playerColliderOffset;

        Vector3 distanceBetweenMeAndThePlayer3D = playerPosition - myPosition;
        Vector2 distanceBetweenMeAndThePlayer2D = new Vector2(distanceBetweenMeAndThePlayer3D.x, distanceBetweenMeAndThePlayer3D.y);

        //        Vector3 distance3D = transform.TransformPoint(transform.position + ) - (player.transform.position + new Vector3(playerCollider.gameObject.transform.localPosition.x, playerCollider.gameObject.transform.localPosition.y) );
//        Vector2 distance = new Vector2(distance3D.x, distance3D.y);
        if (distanceBetweenMeAndThePlayer2D.magnitude < (playerCollider.radius + myCollider.radius))
        {
            GameObject.Find("BeerBar").GetComponent<Bar>().ChangeValue(alchoolInBeer);
            Destroy(gameObject);
        }
    }
}