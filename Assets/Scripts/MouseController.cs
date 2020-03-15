using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseController : MonoBehaviour {

	// Controller-class in the starmap-module. Handles logic for interacting with the starmap.

	int currentID;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		if (Input.GetMouseButtonDown (0)) {
			RaycastHit2D hitInfo = Physics2D.Raycast ((Vector2)Camera.main.ScreenToWorldPoint (Input.mousePosition), Vector2.zero);
			if (hitInfo.collider != null && hitInfo.collider.GetComponent<StarComponent>() != null) {
				StarComponent sc = hitInfo.collider.GetComponent<StarComponent> ();
				if (sc.isNeighbour) {
					currentID = sc.id;
					EventSystem.Current.FireEvent (EventTypeEnum.WARPING, new EventData ("Warping"));
					FindObjectOfType<CameraShake> ().StartShake (2f, SwitchLevel);
				}
			}
		}
	}

	public void SwitchLevel(){
		EventData ed = new NewLevelED ("New level!", currentID);
		EventSystem.Current.FireEvent (EventTypeEnum.NEW_LEVEL, ed);
	}
}
