using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StarComponent : MonoBehaviour {

	//Component for the star indicator on the starmap which keeps track of the levels state to be used by the MouseController

	public int id;
	public Vector2 location;
	public bool isNeighbour;

	[SerializeField]
	Color defaultColor;
	[SerializeField]
	Color visitedColor;

	[SerializeField]
	GameObject neighborIndicator;

	[SerializeField]
	GameObject playerLocationIndicator;

	public void Setup(StarInfo starInfo){
		id = starInfo.id;
		location = starInfo.location;
		isNeighbour = false;
		GetComponentInChildren<SpriteRenderer>().color = defaultColor;
		SetState (StarState.DEFAULT);
	}

	public void SetState(StarState state){

		switch (state) {
		case StarState.DEFAULT:
			neighborIndicator.SetActive (false);
			isNeighbour = false;
			playerLocationIndicator.SetActive (false);
			break;
		case StarState.VISITED:
			GetComponentInChildren<SpriteRenderer>().color = visitedColor;
			break;
		case StarState.PLAYER_POSITION:
			playerLocationIndicator.SetActive (true);
			break;
		case StarState.NEIGHBOUR:
			neighborIndicator.SetActive (true);
			isNeighbour = true;
			break;
		default:
			break;
		}
	}

}

public enum StarState{
	DEFAULT,
	VISITED,
	PLAYER_POSITION,
	NEIGHBOUR
}
