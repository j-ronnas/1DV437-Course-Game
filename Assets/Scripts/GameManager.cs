using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour {

	// Handles the logic for switching between the different modules and gamemodes: MAIN_MENU, UPGRADING, STAR_MAP, COMBAT,	WIN, LOSS
	// Does this my activating/deacitvating relevant gameobjects as well as changing mode for camera.
	// For the Menu mode, the old state is kept track of as to be able to return to the previous state when game was paused.
	public GameState PlayState { get; protected set;}

	[SerializeField]
	GameObject cameraObject;
	[SerializeField]
	GameObject combatObjects;
	[SerializeField]
	GameObject mapObjects;
	[SerializeField]
	GameObject upgradeObjects;
	[SerializeField]
	GameObject statsObject;
	[SerializeField]
	GameObject menuObject;


	[SerializeField]
	GameObject textPrompt;
	bool waitingForPrompt = false;
	// Use this for initialization
	void Start () {
		SetState (GameState.MAIN_MENU);
		PlayState = GameState.STAR_MAP;

		EventSystem.Current.RegisterListener (EventTypeEnum.NEW_LEVEL, OnNewLevel);
		EventSystem.Current.RegisterListener (EventTypeEnum.LEVEL_CLEARED, OnLevelCleared);
		EventSystem.Current.RegisterListener (EventTypeEnum.PLAYER_DEATH, OnPlayerDeath);

		textPrompt.SetActive (false);
	}
	
	// Update is called once per frame
	void Update () {
		if (waitingForPrompt && Input.GetKeyDown (KeyCode.T)) {
			SetState (GameState.STAR_MAP);
			waitingForPrompt = false;
			textPrompt.SetActive (false);
		}

		if (Input.GetKeyDown (KeyCode.Escape)) {
			SetState (GameState.MAIN_MENU);
		}
	}

	void OnLevelCleared(EventData ed){
		LevelClearedED data = (LevelClearedED)ed;
		if (data.finalLevel) {
			SetState (GameState.WIN);
			return;
		}
		textPrompt.SetActive (true);
		waitingForPrompt = true;
	}

	void OnNewLevel(EventData ed){
		NewLevelED newLevel = (NewLevelED)ed;
		SetState (GameState.COMBAT);
	}

	void OnPlayerDeath(EventData ed){
		SetState (GameState.LOSS);
	}	

	public void SetState(GameState state){
		PlayState = state == GameState.MAIN_MENU ? PlayState : state;

		switch (state) {
		case GameState.COMBAT:
			mapObjects.SetActive (false);
			combatObjects.SetActive (true);
			upgradeObjects.SetActive (false);

			cameraObject.GetComponent<CombatCamera> ().enabled = true;
			cameraObject.GetComponent<MapCamera> ().enabled = false;	
			break;
		case GameState.UPGRADING:	
			upgradeObjects.SetActive (true);
			combatObjects.SetActive (false);

			break;
		case GameState.STAR_MAP:
			mapObjects.SetActive (true);
			combatObjects.SetActive (false);
			upgradeObjects.SetActive (false);

			cameraObject.GetComponent<MapCamera> ().enabled = true;
			cameraObject.GetComponent<CombatCamera> ().enabled = false;
			break;
		case GameState.MAIN_MENU:
			menuObject.SetActive (true);
			mapObjects.SetActive (false);
			upgradeObjects.SetActive (false);
			combatObjects.SetActive (false);

			break;
		case GameState.WIN:
			combatObjects.SetActive (false);
			statsObject.SetActive (true);
			statsObject.GetComponent<StatsScreen> ().SetTitle ("You win!");
			break;
		case GameState.LOSS:
			combatObjects.SetActive (false);
			statsObject.SetActive (true);
			statsObject.GetComponent<StatsScreen> ().SetTitle ("You died!");
			break;

		default:
			break;
		}
	}
}


public enum GameState{
	MAIN_MENU,
	UPGRADING,
	STAR_MAP,
	COMBAT,
	WIN,
	LOSS,
}
