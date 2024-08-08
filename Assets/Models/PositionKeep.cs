using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PositionKeep : MonoBehaviour
{
    public GameObject GameOb;

    public Transform PositionToKeep;
    public Transform RotationToKeep;
    public string Type;

    void Start()
    {

    }
    
    // Update is called once per frame
    void Update()
    {
        
        
            GameOb.transform.position = PositionToKeep.position;
            GameOb.transform.rotation = RotationToKeep.rotation;

        if (Input.GetKeyDown(KeyCode.N))
        {
            gameObject.SetActive(false);
        }
    }
}