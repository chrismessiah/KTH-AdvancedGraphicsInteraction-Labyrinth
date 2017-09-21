using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RegularMovementInput : MonoBehaviour {

	Rigidbody rb {
		get { return GetComponent<Rigidbody>();}
	}
	public GameObject playerCamera;
	public float moveSpeed = 300f;
	public float mouseSens = 10f;
	float verticalRotation = 0;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		float moveForward = Input.GetAxis("Vertical")*moveSpeed;
		float strafe = Input.GetAxis("Horizontal")*moveSpeed;
		Vector3 movement = new Vector3(strafe, 0, moveForward);
		movement = transform.rotation * movement;
		rb.AddForce(movement, ForceMode.Acceleration);

		float horizontalRotation = Input.GetAxis("Mouse X") * mouseSens;
		transform.Rotate(0,horizontalRotation,0);
		verticalRotation -= Input.GetAxis("Mouse Y") * mouseSens;
		verticalRotation = Mathf.Clamp(verticalRotation,-90,90);
		playerCamera.transform.localRotation = Quaternion.Euler(verticalRotation,0,0);
	}
}
