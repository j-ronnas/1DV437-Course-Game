using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Crosshair : MonoBehaviour {

	void OnEnable(){
		Cursor.visible = false;
	}

	void OnDisable(){
		Cursor.visible = true;
	}
	// Update is called once per frame
	void Update () {
		Vector3 cursorPos = Camera.main.ScreenToWorldPoint (Input.mousePosition);
		transform.position = new Vector3 (cursorPos.x, cursorPos.y, 0);
	}
}
