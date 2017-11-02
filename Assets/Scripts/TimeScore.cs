using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TimeScore : MonoBehaviour {
	Text text;
	public float timepassing = 0.0f;
	public static TimeScore instance;
	public bool isEnded = false;

	GameController gameController {
		get { return GameController.instance; }
	}

	void Awake () {
		text = GetComponent<Text>();
		text.text = "  Time Passing: 0 sec";
	}

	// Use this for initialization
	void Start () {
		instance = this;		
	}
	
	// Update is called once per frame
	void Update () {
		if (!isEnded) {
			timepassing += Time.deltaTime;
			text.text = "  Time Passing: " + Mathf.Round (timepassing) + " sec";
		}
	}

	public void StopTimer(){
		isEnded = true;
//		Ranking.instance.showRanking ();
	}
}
