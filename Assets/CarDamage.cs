using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarDamage : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if(other.tag  == "Enemy")
        {
            other.GetComponent<Health>().CurrentHealth -= 10000;
        }
    }
}
