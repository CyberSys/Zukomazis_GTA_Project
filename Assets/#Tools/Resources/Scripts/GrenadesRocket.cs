using UnityEngine;
using System.Collections;

public class GrenadesRocket : MonoBehaviour {

	public bool OnCollision;
	public float timeLeft = 2.5f;
	public ParticleSystem[] trailEmitters;


	public GameObject explosionPrefab;

	void OnCollisionEnter(Collision collision) {
		if (OnCollision)
		//if(collision.gameObject.tag=="default")
		Destroy();    
	}

	void Update(){

		timeLeft -= Time.deltaTime;
		if(timeLeft < 0)
		{
			Destroy();
		}
	}

	
	void Destroy(){

		
		GameObject explosionGo = Instantiate(explosionPrefab, transform.position, Quaternion.identity) as GameObject;
		
		AudioLowPassFilter low_pass_filter_acces = explosionGo.GetComponent<AudioLowPassFilter>();
		AudioListener audioListener = FindObjectOfType(typeof(AudioListener)) as AudioListener;
		low_pass_filter_acces.cutoffFrequency = Mathf.Clamp(low_pass_filter_acces.cutoffFrequency, 100.0f,  10000.0f - Vector3.Distance(explosionGo.transform.position, audioListener.transform.position) * 35.0f);
		explosionGo.GetComponent<AudioSource>().Play();
		
		Destroy(explosionGo, 7.0f);
		Destroy(gameObject);
	}
}

