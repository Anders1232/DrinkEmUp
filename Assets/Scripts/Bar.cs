using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bar : MonoBehaviour
{

    public float TotalValue;
    public float CurrentValue;
    public float DecayValue;

    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    void FixedUpdate()
    {
        ChangeValue(-DecayValue);
    }

    void Refresh()
    {
        transform.localScale = new Vector3((CurrentValue / TotalValue), 1, 1);
    }

    public void ChangeValue(float DeltaValue)
    {
        float NewValue = CurrentValue + DeltaValue;

        if (NewValue < 0)
        {
            CurrentValue = 0;
        }
        else if (NewValue > TotalValue)
        {
            CurrentValue = TotalValue;
        }
        else
        {
            CurrentValue = NewValue;
        }

        Refresh();

    }
}
