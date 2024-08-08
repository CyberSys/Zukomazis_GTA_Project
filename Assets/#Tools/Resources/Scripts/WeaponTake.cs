using UnityEngine;
using System.Collections;

public class WeaponTake : MonoBehaviour {

	//public Transform WeaponOn;
	public int WeaponIdex;
	public int Price;

	void OnTriggerEnter(Collider collider)
	{

		if(collider.gameObject.tag == "Player")
		{
			transform.position = new Vector3(transform.position.x, transform.position.y + 1, transform.position.z);

		}
	}
	void OnTriggerStay(Collider collider){
		collider.gameObject.GetComponent<GUISystem> ().Cost = Price;
		collider.gameObject.GetComponent<GUISystem> ().CostShow = true;

		if (Input.GetKey(KeyCode.Return) && collider.gameObject.GetComponent<GUISystem> ().Money > Price){
			
			var oldWeapIndex = collider.gameObject.GetComponent<Weapons> ().weaponIndex;
			collider.gameObject.GetComponent<GUISystem> ().Money -= Price;
			
			collider.gameObject.GetComponent<Weapons> ().OldWeapIndex = oldWeapIndex;
			collider.gameObject.GetComponent<Weapons> ().weaponIndex = WeaponIdex;

			collider.gameObject.GetComponent<GUISystem> ().CostShow = false;
			Destroy(gameObject);
		}
	
	}


	public void EquipWeapon()
    {
		var oldWeapIndex = GetComponent<Collider>().gameObject.GetComponent<Weapons>().weaponIndex;
		GetComponent<Collider>().gameObject.GetComponent<GUISystem>().Money -= Price;

		GetComponent<Collider>().gameObject.GetComponent<Weapons>().OldWeapIndex = oldWeapIndex;
		GetComponent<Collider>().gameObject.GetComponent<Weapons>().weaponIndex = WeaponIdex;

		GetComponent<Collider>().gameObject.GetComponent<GUISystem>().CostShow = false;
		Destroy(gameObject);
	}


	void OnTriggerExit(Collider collider)
	{
		collider.gameObject.GetComponent<GUISystem> ().CostShow = false;
		transform.position = new Vector3(transform.position.x, transform.position.y -1, transform.position.z);
	}
	
}