using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StatsScreen : MonoBehaviour {

	StatManager sm;
	[SerializeField]
	Text[] texts;

	[SerializeField]
	Text title;

	public void SetTitle(string s){
		title.text = s;
	}

	void OnEnable(){
		DisplayScreen ();
	}

	//Gets the relevant information from StatManager and displays it in a list.
	void DisplayScreen(){
		if (sm == null) {
			sm = FindObjectOfType<StatManager> ();
		}
		string[] strings = sm.GetStatistics ();
		for (int i = 0; i < Mathf.Min(texts.Length, strings.Length); i++) {
			texts [i].text = strings [i];
		}
	}
}
