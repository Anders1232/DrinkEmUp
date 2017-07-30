using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BeerSpawnner : MonoBehaviour {

    public float timeBetweenSpawns;
    public float timeBetweenSpawnTimer;
    public float offsetX;
    public float yMin;
    public float yMax;
    public GameObject beerPrefab;
    public Transform cameraTransform;
	// Use this for initialization
	void Start () {
        yMax = -1.0f;
        yMin = -3.5f;
    }
	
	// Update is called once per frame
	void Update () {
        timeBetweenSpawnTimer -= Time.deltaTime;
        if (timeBetweenSpawnTimer < 0) {
            GameObject.Instantiate(beerPrefab, new Vector3(cameraTransform.position.x + offsetX, Random.Range(yMin, yMax) ), Quaternion.identity);
            timeBetweenSpawnTimer = timeBetweenSpawns;
        }
	}
}
