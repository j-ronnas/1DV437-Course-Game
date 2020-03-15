using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MainMenu : MonoBehaviour {

	//Class that assigns functions for main-menubuttons, instead of manually assigning in editor.

	GameManager gm;

	[SerializeField]
	Button[] buttons;

	[SerializeField]
	GameObject settingsGO;
	[SerializeField]
	GameObject mainGO;
	[SerializeField]
	GameObject guideGO;

	// Use this for initialization
	void Start () {
		gm = FindObjectOfType<GameManager>();
		SetupButtons ();
	}

	void SetupButtons(){
		buttons[0].onClick.AddListener(() => {
			gm.SetState(gm.PlayState); 
			gameObject.SetActive(false);
		}); 
		buttons[1].onClick.AddListener(() => {
			settingsGO.SetActive(true); 
			mainGO.SetActive(false);
		}); 
		buttons[2].onClick.AddListener(() => {
			guideGO.SetActive(true); 
			mainGO.SetActive(false);
		}); 
		buttons[3].onClick.AddListener(() => {
			Application.Quit();
		}); 
	}	
}
