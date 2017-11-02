using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TImerScript : MonoBehaviour {

	Text text;
	float timeLeft = 3f;

	void Awake () {
		text = GetComponent<Text>();
	}


	void Update () {
		timeLeft -= Time.deltaTime;
		if (timeLeft > 0) {

			text.text = Mathf.Round (timeLeft).ToString ("f0");
		}
		if (timeLeft < 0) {
			text.text = "Start";
		}

		if (timeLeft < -2) {
			Debug.Log (timeLeft);
			gameObject.SetActive (false);
		}
	}

}

