using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CombatManager : MonoBehaviour {

	// Handles the logic of spawning enemy ships and stations when entering new level as well as keeping track of remaining ships.

	[SerializeField]
	GameObject smallEnemyShipPrefab;
	[SerializeField]
	GameObject mediumEnemyShipPrefab;
	[SerializeField]
	GameObject largeEnemyShipPrefab;


	[SerializeField]
	Transform enemyShipParent;

	GameObject player;
	GameObject station;

	int numberOfActiveEnemies = 0;

	List<int> starsWithStation;

	StarMap starMap;

	bool isFinalLevel = false;

	// Use this for initialization
	void Awake () {
		player = GameObject.FindGameObjectWithTag("Player");
		starMap = FindObjectOfType<StarMap> ();
		station = FindObjectOfType<SpaceStation> ().gameObject;
		EventSystem.Current.RegisterListener (EventTypeEnum.NEW_LEVEL, OnNewLevel);
		EventSystem.Current.RegisterListener (EventTypeEnum.ENEMY_KILLED, OnEnemyKilled);
	}

	void DetermineStationLocations(){
		starsWithStation = new List<int> ();
		int i = 0;
		while (i < starMap.MapSize) {
			starsWithStation.Add (i);
			i += Random.Range (1, 5);
		}
	}
		

	void OnNewLevel(EventData ed){
		ResetPlayerPos ();
		NewLevelED newLevelED = (NewLevelED)ed;
		isFinalLevel = newLevelED.levelIndex == starMap.MapSize - 1;
		if (starsWithStation == null) {
			DetermineStationLocations ();
		}
		station.SetActive (starsWithStation.Contains (newLevelED.levelIndex));
		SpawnEnemies (newLevelED.levelIndex);
	}

	void OnEnemyKilled(EventData ed){
		numberOfActiveEnemies--;
		if (numberOfActiveEnemies == 0) {
			EventSystem.Current.FireEvent (EventTypeEnum.LEVEL_CLEARED, new LevelClearedED ("LEVEL CLEARED!", isFinalLevel));
		}
	}

	void SpawnEnemies(int levelIndex){
		int smallEnemies = (int)(Random.Range (0f, 2f) * levelIndex + 1);
		int mediemEnemies = (int)(Random.Range (0f, 2f) * levelIndex - 4);
		int largeEnemies = (int)(Random.Range (0f, 2f) * levelIndex - 8);


		Vector2 smallEnemyClusterPos = (Vector2)Random.onUnitSphere*25f;
		for (int i = 0; i < smallEnemies; i++) {
			Vector2 pos = smallEnemyClusterPos + 7f*Random.insideUnitCircle;
			Instantiate (smallEnemyShipPrefab, pos, Quaternion.identity, enemyShipParent);
			numberOfActiveEnemies++;
		}
		Vector2 mediumEnemyClusterPos = (Vector2)Random.onUnitSphere*25f;
		for (int i = 0; i < mediemEnemies; i++) {
			Vector2 pos = mediumEnemyClusterPos + 7f*Random.insideUnitCircle;
			Instantiate (mediumEnemyShipPrefab, pos, Quaternion.identity, enemyShipParent);
			numberOfActiveEnemies++;
		}
		Vector2 largeEnemyClusterPos = (Vector2)Random.onUnitSphere*25f;
		for (int i = 0; i < largeEnemies; i++) {
			Vector2 pos = largeEnemyClusterPos + 7f*Random.insideUnitCircle;
			Instantiate (largeEnemyShipPrefab, pos, Quaternion.identity, enemyShipParent);
			numberOfActiveEnemies++;
		}
	}

	void ResetPlayerPos(){
		player.transform.position = Vector2.zero;
		player.GetComponent<Rigidbody2D> ().velocity = Vector2.zero;
	}
		
}
