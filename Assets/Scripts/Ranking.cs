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
	const int MaxNum = 10;
	Text text;
	public Ranking rank;
	public InputField enterUsername;
	int gametime;
	string username;
	bool isNameEntered =false;

	void Awake () {
		text = GetComponent<Text>();
		text.text = "";
	}
		
	// Use this for initialization
	void Start () {
		rank = this;
//		rank.gameObject.SetActive (false);
		enterUsername.gameObject.SetActive(false);
		enterUsername.onEndEdit.AddListener (delegate {getUsername(); });
	}
	
	// Update is called once per frame
	void Update () {
		if (TimeScore.instance.isEnded) {
//			rank.gameObject.SetActive (true);
			text.text = "End of Game";
			enterUsername.gameObject.SetActive(true);
			if (isNameEntered) {
				showRank ();
			} else {
				enterUsername.text = TimeScore.instance.timepassing.ToString();//"Plz enter your name here";
			}
		}
	}

	void getUsername(){
		username = enterUsername.text.ToString ();
		enterUsername.gameObject.SetActive(false);
		isNameEntered =true;
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

	List<Record> readRank(){
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
		return records.OrderBy(r=>r.time).ToList();
//		List<Record> SortedList = records.OrderBy(r=>r.time).ToList();
	}

	public void showRank(){
		rank.gameObject.SetActive (true);
		//get user name

		//get game time
		gametime =  (int)Mathf.Round (TimeScore.instance.timepassing);
		//save to file
		saveRank(resultToJson(username, gametime));


		GameObject go = Instantiate(rank.gameObject);

		//read rank list from file
		List<Record> SortedList = readRank ();
		int showNum = Math.Min (MaxNum,SortedList.Count);

		//show
		for (int i = 0; i < showNum; i++) {
			go.transform.GetComponent<Text>().text += (i.ToString()+SortedList[i].toString()) ;
		}


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
	public string toString(){
		return String.Format ("\t{0, -10}\t{0, 5} sec\n", name, time);
	}
}
