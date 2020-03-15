using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StarMap : MonoBehaviour {

	// Class that is the "view" part of the starmap module. 

	[SerializeField]
	GameObject starPrefab;

	[SerializeField]
	GameObject linePrefab;

	MapGenerator mapGenerator;
	StarComponent[] starComponents;

	int currentLocation = 0;
	public int MapSize { get; protected set;}


	void Awake () {
		MapSize = Random.Range (12, 18);
		mapGenerator = new MapGenerator (MapSize);
		CreateMap ();
		UpdateMap ();

		EventSystem.Current.RegisterListener (EventTypeEnum.NEW_LEVEL, OnNewLevel);
	}

	//Creates gameobjects to represent data created by the MapGenerator class.
	void CreateMap(){
		starComponents = new StarComponent[mapGenerator.numberOfStars];
		int index = 0;
		foreach (StarInfo item in mapGenerator.starInfos) {
			GameObject go = Instantiate (starPrefab, item.location, Quaternion.identity, transform);
			StarComponent sc = go.GetComponent<StarComponent> ();
			sc.Setup (item);
			starComponents [index] = sc;
			index++;
		}

		for (int i = 0; i < mapGenerator.numberOfStars; i++) {
			for (int j = i; j < mapGenerator.numberOfStars; j++) {
				if (mapGenerator.graph [i, j] == 1) {
					GameObject go = Instantiate (linePrefab, transform);
					go.GetComponent<LineRenderer> ().SetPositions (new Vector3[2]{
						(Vector3)mapGenerator.starInfos [i].location,
						(Vector3)mapGenerator.starInfos [j].location
					});
				}
			}
		}
	}

	void OnNewLevel(EventData ed){
		NewLevelED newLevel = (NewLevelED)ed;
		currentLocation = newLevel.levelIndex;
		UpdateMap ();
	}


	void UpdateMap(){
		starComponents [currentLocation].SetState(StarState.VISITED);
		foreach (StarComponent item in starComponents) {
			if (mapGenerator.graph [currentLocation, item.id] == 1) {
				starComponents [item.id].SetState (StarState.DEFAULT);
				starComponents [item.id].SetState (StarState.NEIGHBOUR);
			} else if (item.id == currentLocation) {
				starComponents [currentLocation].SetState(StarState.PLAYER_POSITION);
			} else {
				starComponents [item.id].SetState (StarState.DEFAULT);
			}
		}
	}

	public float GetMapsize(){
		return  mapGenerator.starInfos[mapGenerator.numberOfStars-1].location.x + 2.4f;
	}
}