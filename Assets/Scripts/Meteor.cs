using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Meteor : MonoBehaviour {
	public float minSpeed = 5.0f;
	public float turnSpeed = 20.0f;
	public float randomFreq = 10.0f;
	public float randomForce = 10.0f;

	public float alignForce = 50.0f;
	public float alignRange = 100.0f;

	public float avoidanceRadius = 20.0f;
	public float avoidanceForce = 20.0f;

	public float gatherVelocity = 5.0f;
	public float gatherRadius = 30.0f;

	private Vector3 velocity;
	private Vector3 randomPush;
	private Vector3 leaderPush;
	private Vector3 avoidPush;
	private Vector3 centerPush;

	private MeteorsController leader;

	// Use this for initialization
	void Start () {
		randomFreq = 1.0f / randomFreq;

		if (transform.parent){
			leader = transform.parent.GetComponent<MeteorsController>();
		}

		if (leader.Flocks != null && leader.FlockCount > 1){
			transform.parent = null;
			StartCoroutine(UpdateRandom());
		}
	}

	private IEnumerator UpdateRandom()
	{
		while (true){
			randomPush = Random.insideUnitSphere * randomForce;
			yield return new WaitForSeconds(randomFreq + Random.Range(-randomFreq / 2.0f, randomFreq / 2.0f));
		}
	}


	// Update is called once per frame
	void Update () {
		if (leader == null || leader.FlockCount < 2){ return; }
		minSpeed = turnSpeed = leader.speed;
		avoidPush = Vector3.zero;
		Vector3 myPosition = transform.position;
		Vector3 avgPosition = Vector3.zero;
		Vector3 direction;
		float distance;
		float f;

		foreach (Meteor flock in leader.Flocks)
		{
			Transform flockTrans = flock.transform;

			if (flockTrans != transform){
				Vector3 otherPosition = flockTrans.position;

				avgPosition += otherPosition;

				direction = myPosition - otherPosition;
				distance = direction.magnitude;

				if (distance < avoidanceRadius)
				{
					f = 1.0f - (distance / avoidanceRadius);

					if (distance > 0)
					{
						avoidPush += (direction / distance) * f * avoidanceForce;
					}
				}
			}
		}
		avoidPush /= (leader.FlockCount - 1);

		direction = leader.transform.position - myPosition;
		distance = direction.magnitude;
		f = distance / alignRange;

		if (distance > 0){
			leaderPush = (direction / distance) * f * alignForce;
		}
			
		Vector3 centerPos = (avgPosition / (leader.FlockCount - 1));
		Vector3 toCenter = centerPos - myPosition;
		distance = toCenter.magnitude;

		if (distance > gatherRadius){
			f = distance / gatherRadius - 1.0f;
			centerPush = (toCenter / distance) * f * gatherVelocity;
		}
		else{
			centerPush = Vector3.zero;
		}

		float speed = velocity.magnitude;
		if (speed < minSpeed && speed > 0){
			velocity = (velocity / speed) * minSpeed;
		}

		Vector3 wantedVel = velocity;

		wantedVel -= wantedVel * Time.deltaTime;
		wantedVel += randomPush * Time.deltaTime;
		wantedVel += leaderPush * Time.deltaTime;
		wantedVel += avoidPush * Time.deltaTime;
		wantedVel += centerPush * Time.deltaTime;

		velocity = Vector3.RotateTowards(velocity, wantedVel, turnSpeed * Time.deltaTime, 100.00f);
		transform.rotation = Quaternion.LookRotation(velocity);

		transform.Translate(velocity * Time.deltaTime, Space.World);
	}
}

