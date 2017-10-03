using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.PostProcessing;

public class RegularMovementInput : MonoBehaviour {

	PostProcessingProfile pp {
		get { return GetComponentInChildren<PostProcessingBehaviour>().profile; }
	}
	Rigidbody rb {
		get { return GetComponent<Rigidbody>();}
	}
	public GameObject playerCamera;
	public float moveSpeed = 300f;
	public float mouseSens = 10f;
	float verticalRotation = 0;
	private float t = 0;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void FixedUpdate () {
		float moveForward = Input.GetAxis("Vertical");
		float strafe = Input.GetAxis("Horizontal");
		Vector3 movement = new Vector3(strafe, 0, moveForward);
		movement = transform.rotation * movement.normalized*moveSpeed;
		rb.AddForce(movement, ForceMode.Acceleration);
		
		if(rb.velocity.magnitude > 1){
			if(t<1){
				t += 0.01f;
			}	
		} else {
			if(t>0){
				t -= 0.03f;
			}
		}
		VignetteModel.Settings vignettetmp = pp.vignette.settings;
		vignettetmp.intensity = Mathf.Lerp(0.05f, 0.5f, t);
		pp.vignette.settings = vignettetmp;
			
		
		//vignettetmp.intensity = Mathf.Clamp(rb.velocity.magnitude / 20, 0.05f, 0.4f);
		//pp.vignette.settings = vignettetmp;

		

		float horizontalRotation = Input.GetAxis("Mouse X") * mouseSens;
		transform.Rotate(0,horizontalRotation,0);
		verticalRotation -= Input.GetAxis("Mouse Y") * mouseSens;
		verticalRotation = Mathf.Clamp(verticalRotation,-90,90);
		playerCamera.transform.localRotation = Quaternion.Euler(verticalRotation,0,0);
	}
}
