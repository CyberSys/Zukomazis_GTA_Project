using UnityEngine;
using System.Collections;

public class MakeMoney : MonoBehaviour {

	public int Value;
	
	void OnTriggerEnter(Collider collider)
	{
		
		if(collider.gameObject.tag == "Player")
		{
			collider.gameObject.GetComponent<GUISystem> ().Money += Value;
			Destroy(gameObject);
			
		}
	}
}
