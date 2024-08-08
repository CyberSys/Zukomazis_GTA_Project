using UnityEngine;
using System.Collections;


public class OrbitCamera : MonoBehaviour
{
	public Texture2D crosshairTexture;
	public float crosshairScale = 0.47f;

	//The target of the camera. The camera will always point to this object.
	public Transform _target;
	public Transform targetShoot;
	public Transform targetOrbit;
	
	//The default distance of the camera from the target.
	public float _distance = 3.0f;
	
	//Control the speed of zooming and dezooming.
	//public float _zoomStep = 1.0f;
	
	//The speed of the camera. Control how fast the camera will rotate.
	public float _xSpeed = 3f;
	public float _ySpeed = 3f;
	public float _MinY = 90f;
	public float _MaxY = 90f;
	
	//The position of the cursor on the screen. Used to rotate the camera.
	private float _x = 0.0f;
	private float _y = 0.0f;
	
	//Distance vector. 
	private Vector3 _distanceVector;

//	public Transform turret;
	
	/**
  * Move the camera to its initial position.
  */
	void Start ()
	{
		_distanceVector = new Vector3(0.0f,0.0f,-_distance);
		
		Vector2 angles = this.transform.localEulerAngles;
		_x = angles.x;
		_y = angles.y;
		
		this.Rotate(_x, _y);
		
	}
	void Update (){
/*
		RaycastHit hit;
		Vector3 fwd = transform.TransformDirection(Vector3.forward);
		if (Physics.Raycast(transform.position, fwd, out hit)){
			Vector3 hitPosition = hit.point;
			turret.LookAt(hitPosition);
		}
*/
		bool ShCam = Input.GetButton ("Fire2");

		if (ShCam) {
			_target = targetShoot;
			GetComponent<Camera>().fieldOfView = 20;
		
		} else {
			_target = targetOrbit;
			GetComponent<Camera>().fieldOfView = 60;
		}
	}
	
	/**
  * Rotate the camera or zoom depending on the input of the player.
  */
	void LateUpdate()
	{
		if ( _target )
		{
			this.RotateControls();
			//this.Zoom();
		}
	}

	void OnGUI()
	{
		//if not paused
		if(Time.timeScale != 0 && _target == targetShoot)
		{
			if(crosshairTexture!=null )
				GUI.DrawTexture(new Rect((Screen.width-crosshairTexture.width*crosshairScale)/2 ,(Screen.height-crosshairTexture.height*crosshairScale)/2, crosshairTexture.width*crosshairScale, crosshairTexture.height*crosshairScale),crosshairTexture);
		}
	}
	
	/**
  * Rotate the camera when the first button of the mouse is pressed.
  * 
  */
	void RotateControls()
	{
		//	if ( Input.GetButtonUp("Fire1") )
		//	{
		_x += Input.GetAxis("Mouse X") * _xSpeed;
		_y += -Input.GetAxis("Mouse Y")* _ySpeed;
		
		this.Rotate(_x,_y);

		if (_y > _MaxY) {
			_y = _MaxY;		
		}
		if (_y < _MinY) {
			_y = _MinY;		
		}
				
		//	}
		
	}
	
	/**
  * Transform the cursor mouvement in rotation and in a new position
  * for the camera.
  */
	void Rotate( float x, float y )
	{
		//Transform angle in degree in quaternion form used by Unity for rotation.
		Quaternion rotation = Quaternion.Euler(y,x,0.0f);
		
		//The new position is the target position + the distance vector of the camera
		//rotated at the specified angle.
		Vector3 position = rotation * _distanceVector + _target.position;
		
		//Update the rotation and position of the camera.
		transform.rotation = rotation;
		transform.position = position;
	}
	
	/**
  * Zoom or dezoom depending on the input of the mouse wheel.

	void Zoom()
	{
		if ( Input.GetAxis("Mouse ScrollWheel") < 0.0f )
		{
			this.ZoomOut();
		}
		else if ( Input.GetAxis("Mouse ScrollWheel") > 0.0f )
		{
			this.ZoomIn();
		}
		
	}
	
	/**
  * Reduce the distance from the camera to the target and
  * update the position of the camera (with the Rotate function).

	void ZoomIn()
	{
		_distance -= _zoomStep;
		_distanceVector = new Vector3(0.0f,0.0f,-_distance);
		this.Rotate(_x,_y);
	}
	
	/**
  * Increase the distance from the camera to the target and
  * update the position of the camera (with the Rotate function).

	void ZoomOut()
	{
		_distance += _zoomStep;
		_distanceVector = new Vector3(0.0f,0.0f,-_distance);
		this.Rotate(_x,_y);
	}
	*/
	
} //End class

