using UnityEngine;
using System.Collections;


[RequireComponent(typeof(Animator))]


public class Weapons : MonoBehaviour {
	
	public AudioClip WeaponEmpty;

	[System.Serializable]
	public class WeaponsSetup{


		public Transform WeapObj;
		public Transform leftHandle;
		public Transform rightHandle;
		public Texture2D crossTexture;
		public GameObject MuzzleFlash;
		public AudioClip WeaponSound;
		public string WAnimation = "None";
		public string WIk = "Bazooka.Bazooka";
		public string PreFireAnim;
		public string FireAnim;
		public int RaysPerShoot;
		public int Bullets;
		public int Magazine;
		public int MaxBulletInMagazine;
		public AudioSource ReloadSound;
		public float FireRate;
		public float FireAnimatorSpeed;
		public float Power = 0.0f;
		public float Imprecision;
		public float DamageValue;

		//RigidBodyBullete (grenade&Rifle)
		public Rigidbody RigidBodyPrefab;
		public Transform RigidSpawnTarget;

	}


	public enum BaseState
	{
		Base,
		Combat,
		Climb,
		Swim,
		Pracute,
		JetPack

	}

	public BaseState AnimState = BaseState.Base;

	public AnimControllers animCtrl;
	[System.Serializable]
	public class AnimControllers
	{
		public RuntimeAnimatorController Base;
		//public RuntimeAnimatorController Combat;
		//public RuntimeAnimatorController Climb;
		//public RuntimeAnimatorController Swim;
		//public RuntimeAnimatorController Parachute;
		//public RuntimeAnimatorController Jetpack;
	}
	public WeaponsSetup[] weaponsSetup;
	private Animator m_Animator;
	private bool Reloading;
	
	public GameObject[] _hitParticles;
	public float HitParticlesLifeTime = 10.1f;

	public int weaponIndex = 0;
	public int OldWeapIndex;

	

	public float ReloadTime = 1.0f;
	private float LastReload = 0.0f;
	private float nextFire = 0.0f;
	//Other Weapons
	private RaycastHit hit;
	private Ray ray;
	//Grenade
	private int PowerThrow;
	public GameObject HitMarker;
	public AudioSource ReloadSound;

	void Start () 
	{
		m_Animator = GetComponent<Animator>();        
	}

	void Update()
	{
		var fwd = transform.TransformDirection(Vector3.forward);

		//Setting Key
		bool Shoot = Input.GetButton ("Fire1");
		bool Aim = Input.GetButton ("Fire2");
		bool Reload = Input.GetKey (KeyCode.R);
		bool ThrowRelease = Input.GetButtonUp ("Fire1");


		if (weaponsSetup[weaponIndex].FireAnim != "") {
			m_Animator.SetBool (weaponsSetup[weaponIndex].FireAnim, false);
				}

		if (Reload && weaponsSetup[weaponIndex].Magazine > 0 && weaponsSetup[weaponIndex].Bullets >= 0 && !Aim) {
						Reloading = true;
						m_Animator.SetBool ("Reload", true);
			ReloadSound.Play();

			weaponsSetup[weaponIndex].Bullets = weaponsSetup[weaponIndex].MaxBulletInMagazine;
			weaponsSetup[weaponIndex].Magazine = weaponsSetup[weaponIndex].Magazine - 1;


			LastReload = Time.time + ReloadTime;

			} else {

			if (Time.time > LastReload){
			Reloading = false;
			m_Animator.SetBool ("Reload", false);	
			}
		}


		if (Shoot && Aim && weaponsSetup[weaponIndex].WAnimation == "Equip" && Time.time > nextFire && weaponsSetup[weaponIndex].Bullets > 0 && !Reloading) {

			m_Animator.SetBool (weaponsSetup[weaponIndex].FireAnim, true);
			m_Animator.speed = weaponsSetup[weaponIndex].FireAnimatorSpeed;

			weaponsSetup[weaponIndex].MuzzleFlash.SetActive (true);
			GetComponent<AudioSource>().PlayOneShot(weaponsSetup[weaponIndex].WeaponSound);
			weaponsSetup[weaponIndex].Bullets = weaponsSetup[weaponIndex].Bullets -1;

			nextFire = Time.time + weaponsSetup[weaponIndex].FireRate;

			for(int i =0; i<weaponsSetup[weaponIndex].RaysPerShoot; i++){
				// Regular raycast using vec and transform.position

				Vector2 screenCenterPoint = new Vector2 (Screen.width / 2 + Random.Range(weaponsSetup[weaponIndex].Imprecision, - weaponsSetup[weaponIndex].Imprecision), Screen.height / 2 + Random.Range(weaponsSetup[weaponIndex].Imprecision, - weaponsSetup[weaponIndex].Imprecision));
				
				ray = Camera.main.ScreenPointToRay (screenCenterPoint);

				if (Physics.Raycast (ray, out hit, Camera.main.farClipPlane)) {

					Quaternion rot = Quaternion.FromToRotation (Vector3.forward, hit.normal);
		

					if (hit.collider.tag == "Dirt") {
																			
						var DirtHole = Instantiate (_hitParticles [0], hit.point, rot) as GameObject;
						DirtHole.transform.parent = hit.transform;
						Destroy(DirtHole, HitParticlesLifeTime);
					}
					if (hit.collider.tag == "Metal") {
						
						var MetalHole = Instantiate (_hitParticles [1], hit.point, rot) as GameObject;
						MetalHole.transform.parent = hit.transform;
						Destroy(MetalHole, HitParticlesLifeTime);
					}
					if (hit.collider.tag == "Wood") {
						
						var WoodHole = Instantiate (_hitParticles [2], hit.point, rot) as GameObject;
						WoodHole.transform.parent = hit.transform;
						Destroy(WoodHole, HitParticlesLifeTime);

					}
					if (hit.collider.tag == "Glass") {
						
						var GlassHole = Instantiate (_hitParticles [3], hit.point, rot) as GameObject;
						GlassHole.transform.parent = hit.transform;
						Destroy(GlassHole, HitParticlesLifeTime);
					}
					if (hit.collider.tag == "Water") {
						
						var WaterHole = Instantiate (_hitParticles [4], hit.point, rot) as GameObject;
						WaterHole.transform.parent = hit.transform;
						Destroy(WaterHole, HitParticlesLifeTime);
					}
					if (hit.collider.tag == "Blood") {
						
						var BloodHole = Instantiate (_hitParticles [5], hit.point, rot) as GameObject;
						BloodHole.transform.parent = hit.transform;
						Destroy(BloodHole, HitParticlesLifeTime);
					}
					if (hit.collider.tag == "Ground") {
						
						var GroundHole = Instantiate (_hitParticles [6], hit.point, rot) as GameObject;
						GroundHole.transform.parent = hit.transform;
						Destroy(GroundHole, HitParticlesLifeTime);
					}


					if (hit.collider.tag == "Enemy")
					{
						var BloodHole = Instantiate(_hitParticles[5], hit.point, rot) as GameObject;
						BloodHole.transform.parent = hit.transform;
						Destroy(BloodHole, HitParticlesLifeTime);
						hit.collider.GetComponent<Health>().CurrentHealth -= weaponsSetup[weaponIndex].DamageValue;
						Instantiate(HitMarker);
					
					}
					if (hit.rigidbody)
						
						hit.rigidbody.AddForceAtPosition (fwd * weaponsSetup[weaponIndex].Power, hit.point);  //applies a force to a rigidbody
						}
			}	

				
		} else {


			if (weaponsSetup[weaponIndex].MuzzleFlash && Time.time > nextFire) {

				weaponsSetup[weaponIndex].MuzzleFlash.SetActive (false);
				m_Animator.speed = 0.91f;


						}
				}
		//End 

		//grenade & bomb Launch parameters

		if (Shoot && weaponsSetup[weaponIndex].WAnimation == "Throw") {

			m_Animator.SetBool ("PreThrow", true);	
			PowerThrow = PowerThrow + 20; // Power Launch increment
			if (PowerThrow > 800)
				PowerThrow = 800;
		}

		if (ThrowRelease && weaponsSetup[weaponIndex].WAnimation == "Throw" && Time.time > nextFire && weaponsSetup[weaponIndex].Bullets > 0) {
			m_Animator.SetBool ("Throw", true);
			m_Animator.SetBool ("PreThrow", false);	
			Rigidbody GranadeInstance;
			GranadeInstance = Instantiate(weaponsSetup[weaponIndex].RigidBodyPrefab, weaponsSetup[weaponIndex].RigidSpawnTarget.position, weaponsSetup[weaponIndex].RigidSpawnTarget.rotation) as Rigidbody;
			GranadeInstance.AddForce(weaponsSetup[weaponIndex].RigidSpawnTarget.forward * PowerThrow);
			weaponsSetup[weaponIndex].Bullets = weaponsSetup[weaponIndex].Bullets - 1;
			PowerThrow = 0;
			if(weaponsSetup[weaponIndex].Bullets == 0)
				weaponIndex = 0;
	
		}
		else
		{
			
			m_Animator.SetBool("Throw" , false);                
		}
		//End

		//Bazooka Parameters
		if(Shoot && Aim && weaponsSetup[weaponIndex].WAnimation == "Bazooka" && Time.time > nextFire && weaponsSetup[weaponIndex].Bullets > 0)
		{
			nextFire = Time.time + weaponsSetup[weaponIndex].FireRate;
			weaponsSetup[weaponIndex].Bullets = weaponsSetup[weaponIndex].Bullets -1;

			Rigidbody rocketInstance;
			rocketInstance = Instantiate(weaponsSetup[weaponIndex].RigidBodyPrefab, weaponsSetup[weaponIndex].RigidSpawnTarget.position, weaponsSetup[weaponIndex].RigidSpawnTarget.rotation) as Rigidbody;
			rocketInstance.AddForce(weaponsSetup[weaponIndex].RigidSpawnTarget.forward * 5000);

		}
		if ( Input.GetButtonDown("Fire1") && weaponsSetup[weaponIndex].Bullets == 0 && weaponsSetup[weaponIndex].WAnimation == "Equip"){
			GetComponent<AudioSource>().PlayOneShot(WeaponEmpty);
		}
	}
	//End


	void LateUpdate () 
	{
		GameObject theCamera = GameObject.Find("PlayerCamera");
		OrbitCamera CamScript = theCamera.GetComponent<OrbitCamera>();

		CamScript.crosshairTexture = weaponsSetup[weaponIndex].crossTexture;

	//	if (weaponIndex != 0) {

			weaponsSetup [weaponIndex].WeapObj.tag = "on";
			weaponsSetup [weaponIndex].WeapObj.GetComponent<Renderer>().enabled = true;

	//		if (OldWeapIndex!=0){
			weaponsSetup [OldWeapIndex].WeapObj.GetComponent<Renderer>().enabled = false;
			weaponsSetup [OldWeapIndex].WeapObj.tag = "off";
//			}
	//			}

		if (weaponsSetup[weaponIndex].WeapObj) {

			if (weaponsSetup[weaponIndex].WeapObj.tag == "on") {

				if (Input.GetButton ("Fire2") && weaponsSetup[weaponIndex].WAnimation != "Bazooka" && !Reloading) {

					m_Animator.SetBool (weaponsSetup[weaponIndex].PreFireAnim, true);


										//WIk = "Armed.PreFire";
				
				} else {
					if (weaponsSetup[weaponIndex].WAnimation != "Bazooka")
					m_Animator.SetBool (weaponsSetup[weaponIndex].PreFireAnim, false);
					m_Animator.SetBool (weaponsSetup[weaponIndex].WAnimation, true);

								}

						}


			if (weaponsSetup[OldWeapIndex].WeapObj.tag == "off") {

				if (OldWeapIndex != 0){
				weaponsSetup[OldWeapIndex].leftHandle = null;
				weaponsSetup[OldWeapIndex].rightHandle = null;
				}
				if (weaponsSetup[OldWeapIndex].WAnimation != weaponsSetup[weaponIndex].WAnimation) //Only if animation is different
				m_Animator.SetBool (weaponsSetup[OldWeapIndex].WAnimation, false);

						}

						m_Animator.SetLayerWeight (2, 1);


			}
		}


	void OnAnimatorIK(int layerIndex)
	{
		if(!enabled) return;
		
		if (layerIndex == 2) // do the log holding on the last layer, since LookAt is done in previous layer
		{
			float ikWeight =  m_Animator.GetCurrentAnimatorStateInfo(2).IsName(weaponsSetup[weaponIndex].WIk) ? 1 : 0;
			
			if (weaponsSetup[weaponIndex].leftHandle != null)
			{
				m_Animator.SetIKPosition(AvatarIKGoal.LeftHand, weaponsSetup[weaponIndex].leftHandle.transform.position);
				m_Animator.SetIKRotation(AvatarIKGoal.LeftHand, weaponsSetup[weaponIndex].leftHandle.transform.rotation);
				m_Animator.SetIKPositionWeight(AvatarIKGoal.LeftHand, ikWeight);
				m_Animator.SetIKRotationWeight(AvatarIKGoal.LeftHand, ikWeight);
			}
			
			if (weaponsSetup[weaponIndex].rightHandle != null)
			{
				m_Animator.SetIKPosition(AvatarIKGoal.RightHand, weaponsSetup[weaponIndex].rightHandle.transform.position);
				m_Animator.SetIKRotation(AvatarIKGoal.RightHand, weaponsSetup[weaponIndex].rightHandle.transform.rotation);
				m_Animator.SetIKPositionWeight(AvatarIKGoal.RightHand, ikWeight);
				m_Animator.SetIKRotationWeight(AvatarIKGoal.RightHand, ikWeight);
			}
		}
	}
}
