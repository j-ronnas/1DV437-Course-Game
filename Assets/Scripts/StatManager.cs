using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StatManager : MonoBehaviour {

	//This class listens to different events and increments counters for these events.

	int levelsCleared;
	int enemiesKilled;
	int shotsHit;
	int shotsFired;
	int coinsCollected;

	float accuracy;
	float score;
	float highscore;

	void Start(){
		EventSystem.Current.RegisterListener (EventTypeEnum.ENEMY_KILLED, OnEnemyKilled);
		EventSystem.Current.RegisterListener (EventTypeEnum.LEVEL_CLEARED, OnLevelCleared);
		EventSystem.Current.RegisterListener (EventTypeEnum.ENEMY_HIT, OnEnemyHit);
		EventSystem.Current.RegisterListener (EventTypeEnum.COIN_CHANGED, OnCoinPickup);
		EventSystem.Current.RegisterListener (EventTypeEnum.SHOT_FIRED, OnShotFired);

	}



	void OnEnemyKilled(EventData ed){
		enemiesKilled++;
	}

	void OnLevelCleared(EventData ed){
		levelsCleared++;
	}

	void OnEnemyHit(EventData ed){
		shotsHit++;
	}

	void OnCoinPickup(EventData ed){
		if (ed.message == "Pickup") {
			coinsCollected++;
		}
	}

	void OnShotFired(EventData ed){
		shotsFired++;
	}

	void UpdateScores(){
		shotsFired = shotsFired == 0 ? 1 : shotsFired;
		accuracy = (float)shotsHit / shotsFired;
		score = 100 * levelsCleared + 20 * enemiesKilled + 10 * coinsCollected;

		highscore = PlayerPrefs.HasKey("Highscore") ? PlayerPrefs.GetFloat ("Highscore") : 0;
		highscore = score > highscore ? score : highscore;

		PlayerPrefs.SetFloat ("Highscore", highscore);

		PlayerPrefs.Save ();

	}

	// Method to get all stats as strings to display to the user
	public string[] GetStatistics(){
		UpdateScores ();
		string[] strings = new string[8];

		strings [0] = "Levels cleared: " + levelsCleared.ToString ();
		strings [1] = "Enemies destroyed: " + enemiesKilled.ToString ();
		strings [2] = "Shots fired: " + shotsFired.ToString ();
		strings [3] = "Shots hit: " + shotsHit.ToString ();
		strings [4] = "Accuracy: " + accuracy.ToString ("0.00");
		strings [5] = "Coins collected: " + coinsCollected.ToString ();
		strings [6] = "Total score: " + score.ToString ();
		strings [7] = "Highscore: " + highscore.ToString ();
		return strings;

	}

}

