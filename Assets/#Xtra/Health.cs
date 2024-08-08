using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.Characters.ThirdPerson;
using UnityEngine.AI;
public class Health : MonoBehaviour
{
    public float CurrentHealth;
    public GameObject DeathPrefab;
    public Transform Pos;
    


    void Start()
    {
        
    }

    
    void Update()
    {
        if(CurrentHealth < 0)
        {
            // Use this if you want to instatiate prefab at death
            //Instantiate(DeathPrefab, Pos.position, Pos.rotation);
            gameObject.GetComponent<Animator>().Play("Death");
            Destroy(gameObject.GetComponent<AICharacterControl>());
            Destroy(gameObject.GetComponent<ThirdPersonCharacter>());
            Destroy(gameObject.GetComponent<Rigidbody>());
            Destroy(gameObject.GetComponent<CapsuleCollider>());
            Destroy(gameObject.GetComponent<NavMeshAgent>());
            gameObject.GetComponent<AutoDestroy>().enabled = true;
        }
    }
}
