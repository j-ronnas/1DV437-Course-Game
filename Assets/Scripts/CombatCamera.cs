using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CombatCamera : MonoBehaviour {

	// When in combat mode the camera follows the player as well as the cursor.
	// This is done by positinin the camera between cursor and player using a vector 3 lerp.

	Transform playerPos;
	Transform cursorPos;

	float maxCamDist = 2f;
	float maxCursorDist = 10f;

	// Use this for initialization
	void Start () {
		playerPos = FindObjectOfType<PlayerMovement> ().transform;
		cursorPos = FindObjectOfType<Crosshair> ().transform;
	}
	
	// Update is called once per frame
	void Update () {
		Vector3 aimDir = (cursorPos.position - playerPos.position);
		float cursorDist = aimDir.magnitude;
		aimDir = aimDir / cursorDist;

		float t = Mathf.Clamp (cursorDist / maxCursorDist, 0, 1);

		Vector3 cameraPos = Vector3.Lerp (playerPos.position, playerPos.position + maxCamDist*aimDir, t);

		transform.position = new Vector3 (cameraPos.x, cameraPos.y, -10);
	}
}
