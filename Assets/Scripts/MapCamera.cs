using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapCamera : MonoBehaviour {

	// Positions the camera and sets its size to fit the whole map.

	StarMap starMap;

	void OnEnable () {
		if (starMap == null) {
			starMap = FindObjectOfType<StarMap> ();
		}
		transform.position = starMap.transform.position + new Vector3(starMap.GetMapsize ()/2f,0,0);
		Camera.main.orthographicSize = 1.1f* starMap.GetMapsize ()  / (2* Camera.main.aspect);
	}

}
