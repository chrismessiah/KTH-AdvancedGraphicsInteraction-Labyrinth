using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Timer : MonoBehaviour {

	Text text;
	float timeLeft = 2f * 60f;

	void Awake () {
		text = GetComponent<Text>();
	}
	

	void Update () {
		timeLeft -= Time.deltaTime;
		text.text = "  Time left: " + Mathf.Round(timeLeft);
	}
}
