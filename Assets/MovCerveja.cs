using UnityEngine;
using System.Collections;

public class MovCerveja : MonoBehaviour
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

        Vector3 distance3D = transform.TransformPoint(transform.position+ new Vector3(myCollider.offset.x, myCollider.offset.y) ) - (player.transform.position+ new Vector3(playerCollider.gameObject.transform.localPosition.x, playerCollider.gameObject.transform.localPosition.y) );
        Vector2 distance = new Vector2(distance3D.x, distance3D.y);
        //        print("BeerPos= (" + (transform.position + new Vector3(myCollider.offset.x, myCollider.offset.y)).x + ", " + (transform.position + new Vector3(myCollider.offset.x, myCollider.offset.y)).y + ")");
                print("PlayerPos= (" + (player.transform.position + new Vector3(playerCollider.offset.x, playerCollider.offset.y) ).x + ", " + (player.transform.position + new Vector3(playerCollider.offset.x, playerCollider.offset.y) ).y + ")");
        //        print("distance magnetude = " + distance.magnitude+ ", radious add = " + playerCollider.radius + myCollider.radius);
        if (distance.magnitude < playerCollider.radius + myCollider.radius) {
            GameObject.Find("BeerBar").GetComponent<Bar>().ChangeValue(alchoolInBeer);
            Destroy(gameObject);
        }
    }
}