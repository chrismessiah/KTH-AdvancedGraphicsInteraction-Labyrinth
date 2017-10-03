using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GeneralGravity : MonoBehaviour {

    private Vector3 gravityCenter = new Vector3(0, 0, 0);
    public float gravityPower = -10;
    Vector3 gravityDirection;


    // Use this for initialization
    void Start () {
        
	}
	
	// Update is called once per frame
	void Update () {

	}

    private void FixedUpdate()
    {
        Component[] rigidBodies = transform.GetComponentsInChildren<Rigidbody>();
        foreach (Rigidbody rb in rigidBodies)
        {
            gravityDirection = gravityCenter - rb.transform.position;
            gravityDirection = new Vector3(0, gravityDirection.y, gravityDirection.z);
            rb.AddForce(gravityDirection.normalized * gravityPower, ForceMode.Acceleration);
        }

    }
}
