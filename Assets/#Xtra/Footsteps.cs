using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Footsteps : MonoBehaviour
{
    public AudioSource Footstep;

    private void OnTriggerEnter(Collider other)
    {
        Footstep.Play();
    }
}
