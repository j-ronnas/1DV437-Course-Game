using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpaceStation : MonoBehaviour {

	//Class to handle logic for SpaceStations within the Combat module.

	[SerializeField]
	GameObject textPrompt;

	bool withinTrigger = false;

	GameManager gm;

	// Use this for initialization
	void Start () {
		textPrompt.SetActive (false);
		gm = FindObjectOfType<GameManager> ();
	}

	void OnDisable(){
		textPrompt.SetActive (false);
	}
	
	// Update is called once per frame
	void Update () {
		if (withinTrigger && Input.GetKeyDown (KeyCode.E)) {
			gm.SetState (GameState.UPGRADING);
			textPrompt.SetActive (false);
			withinTrigger = false;
		}
	}


	void OnTriggerEnter2D(Collider2D coll){
		if (coll.tag == "Player") {
			textPrompt.SetActive (true);
			withinTrigger = true;
		}
	}


	void OnTriggerExit2D(Collider2D coll){
		if (coll.tag == "Player") {
			textPrompt.SetActive (false);
			withinTrigger = false;
		}
	}
}
