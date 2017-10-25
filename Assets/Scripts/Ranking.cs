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

	Text text;
	public GameObject rank;
	public InputField username;


	void Awake () {
		text = GetComponent<Text>();
		text.text = "Ending";
	}
		
	// Use this for initialization
	void Start () {
//		rank.SetActive (false);
//		rank = this;
	}
	
	// Update is called once per frame
	void Update () {
		if (TimeScore.instance.isEnded) {
			rank.SetActive (false);
			text.text = "Ending";
		}
	}

	public void showRanking(){
		rank.SetActive (false);
	}


	public string resultToJson(string name, int time)
	{
		Record record = new Record (name, time);
		return JsonUtility.ToJson(record);
	}

	void saveRank(string records)
	{
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
		writer.WriteLine(records);
		writer.Close();
	}

	void readRank(){
		FileStream f = new FileStream(Application.dataPath + "/Resources/rank.txt", FileMode.Open);
		Record temp = new Record ();
		List<Record> records = new List<Record>();	
		if (f.CanRead){
			StreamReader reader = new StreamReader(f);
			string str;
			while ((str = reader.ReadLine ()) != null) {
				temp = JsonUtility.FromJson<Record> (str);
				records.Add (temp);
			}
		}
		List<Record> SortedList = records.OrderBy(r=>r.time).ToList();
	}
}

class Record{
	public string name;
	public int time;
	public Record(){
	}
	public Record(string n, int t){
		name = n;
		time = t;
	}
}
