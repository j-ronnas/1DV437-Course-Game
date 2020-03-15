using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CoinCounter : MonoBehaviour {

	//Class for the coin counter in the UI. Listens to event and updates text.

	Text text;

	// Use this for initialization
	void Start () {
		text = GetComponent<Text> ();
		EventSystem.Current.RegisterListener (EventTypeEnum.COIN_CHANGED, OnCoinCountChanged);
	}
	

	void OnCoinCountChanged(EventData ed){
		CoinChangedED data = (CoinChangedED)ed;
		text.text = data.newAmount.ToString();
	}
}
