using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoinSpawner : MonoBehaviour {

	// Handles logic of spawning coins on enemy death. Could be further extended by changing if/how many coins are created.
	// Right now it always spawns 1 coin.

	[SerializeField]
	GameObject coinPrefab;

	// Use this for initialization
	void Start () {
		EventSystem.Current.RegisterListener (EventTypeEnum.ENEMY_KILLED, OnEnemyDeath);
		EventSystem.Current.RegisterListener (EventTypeEnum.NEW_LEVEL, OnNewLevel);
	}


	void OnEnemyDeath(EventData ed){
		EnemyKilledED enemyKilledED = (EnemyKilledED)ed;
		Instantiate (coinPrefab, enemyKilledED.enemyPosition, Quaternion.identity, this.transform);
	}

	void OnNewLevel(EventData ed){
		for (int i = 0; i < transform.childCount; i++) {
			Destroy (transform.GetChild (i).gameObject);
		}
	}
}
