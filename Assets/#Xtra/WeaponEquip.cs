using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponEquip : MonoBehaviour
{
    public int Index;
    public GameObject Weapon;

    public void EquipWeapon()
    {
        Transform RightHand = GameObject.FindGameObjectWithTag("WeaponHand").transform;

        foreach (Transform child in RightHand)
            child.gameObject.SetActive(false);


        GameObject.FindGameObjectWithTag("Player").GetComponent<Weapons>().weaponIndex = Index;
        Weapon.SetActive(true);
    }

}


