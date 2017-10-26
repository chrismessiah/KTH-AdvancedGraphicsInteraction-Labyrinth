using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

using System;
using System.Reflection;
using System.Text;
using System.IO;
using System.Linq;

public class Ranking : MonoBehaviour {

	Text text {
		get {return GetComponent<Text>();}
	}
	public List<float> records;


	void Awake () {

	}
		
	// Use this for initialization
	void Start () {

	}
	
	// Update is called once per frame
	void Update () {

	}

	/*
	public void showRanking(){
		rank.SetActive (false);
	}

	
	public string resultToJson(string name, int time)
	{
		Record record = new Record (name, time);
		return JsonUtility.ToJson(record);
	}
	*/

	public void saveRank(float record)
	{
		records.Add(record);
		FileInfo f = new FileInfo(Application.dataPath + "/Resources/rank.txt");
		StreamWriter writer= null;
		if (f.Exists)
		{
			writer = f.AppendText();
		}
		else
		{
			writer = f.CreateText();
		}
		writer.WriteLine(record.ToString());
		writer.Close();
	}

	public void showHighscore(){
		List<float> SortedList = records.OrderBy(r => r).ToList();
		text.text = "Highscores\n\n";
		for(int i=0; i<10; ++i){
			print("record");
			text.text += (Mathf.Round(SortedList[i]).ToString()+" seconds\n");
		}
	}

	public void readRank(){
		FileStream f = new FileStream(Application.dataPath + "/Resources/rank.txt", FileMode.Open);
		List<float> recordsTmp = new List<float>();	
		if (f.CanRead){
			StreamReader reader = new StreamReader(f);
			string str;
			while ((str = reader.ReadLine ()) != null) {
				recordsTmp.Add(float.Parse(str));
			}
		}
		records = recordsTmp;
		
	}
}

/*
class Record{
	public string name;
	public int time;
	public Record(){
	}
	public Record(string n, int t){
		name = n;
		time = t;
	}
}*/
