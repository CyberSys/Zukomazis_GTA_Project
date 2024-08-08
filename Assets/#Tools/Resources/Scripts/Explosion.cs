using UnityEngine;
using System.Collections;

public class Explosion : MonoBehaviour {
	public float radius = 5.0F;
	public float power = 10.0F;
	void Start() {
		Vector3 explosionPos = transform.position;
		Collider[] colliders = Physics.OverlapSphere(explosionPos, radius);
		foreach (Collider hit in colliders) {
			if (hit && hit.GetComponent<Rigidbody>())
				hit.GetComponent<Rigidbody>().AddExplosionForce(power, explosionPos, radius, 3.0F);
			
		}
	}
}