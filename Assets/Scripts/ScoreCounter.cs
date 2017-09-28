using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScoreCounter : MonoBehaviour {

	Text text;
	Vector3 startPoint;
	int maxScore = 0;

	void Awake () {
		text = GetComponent<Text>();
		text.text = "  Score: 0";
	}

	void Start() {
		startPoint = GameObject.Find("MouseKeyboardPlayer").transform.position;
	}

	void Update () {
		Vector3 currentPoint = GameObject.Find("MouseKeyboardPlayer").transform.position;
		Vector3 distance = currentPoint - startPoint;
		int currentScore = (int)Mathf.Round(distance.magnitude);
		if (maxScore < currentScore) {
			maxScore = currentScore;
			text.text = "  Score: " + maxScore;
		}
	}
}
