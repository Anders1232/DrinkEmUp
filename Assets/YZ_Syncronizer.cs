using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class YZ_Syncronizer : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        transform.Translate(new Vector3(0, 0, transform.position.y - transform.position.z));
    }
}
