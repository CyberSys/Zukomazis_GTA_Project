 /// <summary>
/// 
/// </summary>

using UnityEngine;
using System;
using System.Collections;
  
[RequireComponent(typeof(Animator))]  

//Name of class must be name of file as well

public class PlayerMotor : MonoBehaviour {

	public Transform target;
    protected Animator animator;
	private float val = 2;
    private float speed = 0;
    private float direction = 0;
    private Locomotion locomotion = null;

	// Use this for initialization
	void Start () 
	{
        animator = GetComponent<Animator>();
        locomotion = new Locomotion(animator);
	}
    
	void Update () 
	{

		if (Input.GetButton ("Jump")) {
			animator.SetBool ("Jump", true);
		}
		else
		{
			animator.SetBool("Jump", false);                
		}

		if (Input.GetButton ("Fire2")) {

			//Look in inverse position of Camera
			transform.rotation = Quaternion.LookRotation(transform.position - new Vector3(target.position.x, 0, target.position.z) );

		}
		bool isRun = Input.GetKey(KeyCode.LeftShift);

		if (isRun) {
			val = 6;

		} else {
			val = 2;	
			
		}

        if (animator && Camera.main)
		{
            JoystickToEvents.Do(transform,Camera.main.transform, ref speed, ref direction);
			locomotion.Do(speed * val, direction * 180);
		}
	}
}
