using UnityEngine;
using System.Collections;

public class PlayerCollider : MonoBehaviour {
    void OnTriggerEnter(Collider other)
    {
        print("OnTriggerEnter!");
        Destroy(other.gameObject);
    }
    void OnCollisionEnter(Collision other)
    {
        print("OnCollisionEnter!");
        Destroy(other.gameObject);
    }
    void OnTriggerStay(Collider other)
    {
        print("OnTriggerStay!");
        Destroy(other.gameObject);
    }
    void OnCollisionStay(Collision other)
    {
        print("OnCollisionStay!");
        Destroy(other.gameObject);
    }
    void OnTriggerExit(Collider other)
    {
        print("OnTriggerExit!");
        Destroy(other.gameObject);
    }
    void OnCollisionExit(Collision other)
    {
        print("OnCollisionExit!");
        Destroy(other.gameObject);
    }
}
