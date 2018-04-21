using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovementController : MonoBehaviour {

	[SerializeField]
	private float _walkSpeed, _sideWalkSpeed;
	
	// Update is called once per frame
	private void FixedUpdate () 
	{
		Vector3 cameraForwardVector = Camera.main.transform.forward;
		Vector3 cameraRightVector = Camera.main.transform.right;

		if(Input.GetButton("MoveForward"))
			transform.position += new Vector3(cameraForwardVector.x *_walkSpeed,0, cameraForwardVector.z * _walkSpeed);
		else if(Input.GetButton("MoveBackward"))
			transform.position -= new Vector3(cameraForwardVector.x *_walkSpeed,0, cameraForwardVector.z * _walkSpeed);

		if(Input.GetButton("MoveRight"))
			transform.position += new Vector3(cameraRightVector.x * _sideWalkSpeed, 0, cameraRightVector.z * _sideWalkSpeed);
		else if(Input.GetButton("MoveLeft"))
			transform.position -= new Vector3(cameraRightVector.x * _sideWalkSpeed, 0, cameraRightVector.z * _sideWalkSpeed);
	}
}
