using UnityEngine;
using System.Collections;

[RequireComponent(typeof(Animator))]  

/// <summary>
/// Makes player lookahead in the direction pointed by controller.
/// Gives a more "responsive" feel
/// </summary>
public class LookAhead : MonoBehaviour {
	
	public Transform CameraTarget ;   	
	
	Animator m_Animator;
	
	void Start () 
	{
		m_Animator = GetComponent<Animator>();
	}
	
	void OnAnimatorIK(int layerIndex)
	{			
		if(!enabled) return; 
		
		if(layerIndex == 0) // do IK pass on base layer only
		{
			float vertical = m_Animator.GetCurrentAnimatorStateInfo(0).IsName("Locomotion.Idle") ? 1 : 0;
			
			Vector3 lookAheadPosition = CameraTarget.position + (CameraTarget.forward * 5) + 
				(CameraTarget.up * vertical * Input.GetAxis("Vertical")) + (CameraTarget.right * 160 * Input.GetAxis("Horizontal"));							
			m_Animator.SetLookAtPosition(lookAheadPosition);

			if (Input.GetButton ("Fire2")) {

				m_Animator.SetLookAtWeight(1.0f, 1.7f, 0.9f, 1.0f, 0.6f);	
			}else{
			m_Animator.SetLookAtWeight(0.3f, 0.5f, 0.9f, 1.0f, 0.6f);
			}
		}
	}
}