using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarRadio : MonoBehaviour
{
    public float RadioOn;



    void Update()
    {
        RadioKeep();
    }


    void RadioKeep()
    {
        if(RadioOn == 0)
        {
            gameObject.GetComponent<AudioSource>().volume = 0;
        }

        if (RadioOn == 1)
        {
            gameObject.GetComponent<AudioSource>().volume = 0.5f;
        }

        if (RadioOn == 2)
        {
           RadioOn = 0;
        }

        if (Input.GetKeyDown(KeyCode.T))
        {
            RadioOn += 1;
        }
    }
}
