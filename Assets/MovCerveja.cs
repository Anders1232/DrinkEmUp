using UnityEngine;
using System.Collections;

public class MovCerveja : MonoBehaviour
{
    public float sinCounter;
    public float sinCounterStep;
    public float shakeAmplitude;

    void FixedUpdate()
    {
        sinCounter += sinCounterStep;
        gameObject.transform.Translate(new Vector2(0, Mathf.Sin(sinCounter)* shakeAmplitude));
    }
}