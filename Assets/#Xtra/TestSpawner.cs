using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestSpawner : MonoBehaviour
{
    public GameObject Spawnee;

    public Transform SpawnPos;


    public void Spawn()
    {
        Instantiate(Spawnee, SpawnPos.position, SpawnPos.rotation);
    }

}
