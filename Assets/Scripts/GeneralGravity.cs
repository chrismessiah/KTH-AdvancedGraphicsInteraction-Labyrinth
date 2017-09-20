using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GeneralGravity : MonoBehaviour {
    private Rigidbody rb
    {
        get { return GetComponent<Rigidbody>(); }
    }
    private Vector3 gravityCenter = new Vector3(0, 0, 0);
    private float gravityPower = -10;
    Vector3 gravityDirection;


    // Use this for initialization
    void Start () {
        
	}
	
	// Update is called once per frame
	void Update () {

	}

    private void FixedUpdate()
    {
        gravityDirection = gravityCenter - transform.position;
        gravityDirection = new Vector3(0, gravityDirection.y, gravityDirection.z);
        rb.AddForce(gravityDirection.normalized*gravityPower, ForceMode.Acceleration);
    }
}
