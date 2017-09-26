using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeteorsController : MonoBehaviour {
	public Vector3 bound;
	public float speed = 10.0f;

	public Meteor[] Flocks;
	public int FlockCount;

	private Vector3 initialPosition;
	private Vector3 nextMovementPoint;

	void Awake()
	{
		Flocks = transform.GetComponentsInChildren<Meteor>();
		FlockCount = Flocks.Length;
	}

	// Use this for initialization
	void Start () {
		initialPosition = transform.position;
		CalculateNextMovementPoint();
	}

	// Update is called once per frame
	void Update () {
		transform.Translate(Vector3.forward * speed * Time.deltaTime);
		transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(nextMovementPoint - transform.position), 0.2f * Time.deltaTime);

		if (Vector3.Distance(nextMovementPoint, transform.position) <= 10.0f)
			CalculateNextMovementPoint();
	}

	private void CalculateNextMovementPoint()
	{
		float posX = Random.Range(-15, -15);
		float posY = Random.Range(initialPosition.y - bound.y, initialPosition.y + bound.y);
		float posZ = Random.Range(initialPosition.z - bound.z, initialPosition.z + bound.z);

		nextMovementPoint = initialPosition + new Vector3(posX, posY, posZ);
	}

	//    private void OnDrawGizmos()
	//    {
	//        Gizmos.color = Color.green;
	//
	//        Gizmos.DrawCube(transform.position, Vector3.one);
	//    }
}

