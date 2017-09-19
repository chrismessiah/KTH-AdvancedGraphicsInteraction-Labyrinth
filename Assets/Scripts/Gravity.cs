using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gravity : MonoBehaviour {
    private Rigidbody rb
    {
        get { return GetComponent<Rigidbody>(); }
    }
    private Vector3 gravityCenter = new Vector3(0, 0, 0);
    private float gravityPower = -1;
    Vector3 myNormal;
    float lerpSpeed = 10f;
    Vector3 gravityDirection;


    // Use this for initialization
    void Start () {
        myNormal = transform.up;
	}
	
	// Update is called once per frame
	void Update () {
        myNormal = Vector3.Lerp(myNormal, gravityDirection, lerpSpeed);
        Vector3 myForward = Vector3.Cross(transform.right, myNormal);
        Quaternion targetRot = Quaternion.LookRotation(myForward, myNormal);
        transform.rotation = Quaternion.Lerp(transform.rotation, targetRot, lerpSpeed);
	}

    private void FixedUpdate()
    {
        gravityDirection = gravityCenter - transform.position;
        float length = Vector3.Magnitude(gravityDirection);
        gravityDirection = new Vector3(0, gravityDirection.y, gravityDirection.z);
        rb.AddForce(gravityDirection*gravityPower);
    }
}
