using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScoreCounter : MonoBehaviour {

	GameController gameController {
		get { return GameController.instance; }
	}

	Text text;
	Vector3 startPoint;
	int maxScore = 0;

	void Awake () {
		text = GetComponent<Text>();
		text.text = "  SCORE: 0";
	}

	void Start() {
		startPoint = gameController.player.transform.position;
	}

	void Update () {
		Vector3 currentPoint = gameController.player.transform.position;
		Vector3 distance = currentPoint - startPoint;
		int currentScore = (int)Mathf.Round(distance.magnitude);
		if (maxScore < currentScore) {
			maxScore = currentScore;
			text.text = "  SCORE: " + maxScore;
		}
	}
}
