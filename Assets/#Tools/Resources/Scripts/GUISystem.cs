using UnityEngine;
using System.Collections;

public class GUISystem : MonoBehaviour {
	
	public GUISkin CustomSkin;
	private bool GuiIsActive = false;
	public bool MoneyShow = false;
	public bool CostShow = false;
	public int Money;
	public int Cost;

	void OnTriggerEnter(Collider collider)
	{
		
				if (collider.gameObject.name == "MoneyShow") {
						MoneyShow = true;
			
				}
		}
	void OnTriggerExit(Collider collider)
	{
				if (collider.gameObject.name == "MoneyShow") {
						MoneyShow = false;

				}
		}
	
	void OnGUI(){
		
		GameObject thePlayer = GameObject.Find("Player");
		Weapons playerScript = thePlayer.GetComponent<Weapons>();
		
		
		if (playerScript.weaponIndex != 0) {
			
			GuiIsActive = true;		
			
		} 
		else {
			GuiIsActive = false;
		}
		
		
		GUI.skin = CustomSkin;
		GUILayout.BeginArea(new Rect(Screen.width - 100,5,100,100));
		GUILayout.BeginVertical();

		if (MoneyShow)
			GUILayout.Label("$ " + Money,"MoneyStyle");

		if (CostShow)
			GUILayout.Label("-$ " + Cost,"CostStyle");
		
		if (GuiIsActive)
		GUILayout.Label(playerScript.weaponsSetup[playerScript.weaponIndex].Bullets + " / " + playerScript.weaponsSetup[playerScript.weaponIndex].Magazine,"WeaponInfo");

		GUI.EndGroup ();
	}
}
