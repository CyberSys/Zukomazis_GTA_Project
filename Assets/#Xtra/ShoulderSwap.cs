using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShoulderSwap : MonoBehaviour
{
    public float Side;

    void Start()
    {
        
    }

    

    void Update()
    {
        if(Side == 0)
        {
            gameObject.transform.localPosition = new Vector3(0.35f, 1.7f, -0.33f);
        }

        if (Side == 1)
        {
            gameObject.transform.localPosition = new Vector3(-0.35f, 1.7f, -0.33f);
        }


        if(Side == 2)
        {
            Side = 0;
        }

        if (Input.GetKeyDown(KeyCode.E))
        {
            Side += 1;
        }
    }
}
