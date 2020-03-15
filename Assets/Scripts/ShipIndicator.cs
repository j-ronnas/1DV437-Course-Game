using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShipIndicator : MonoBehaviour {

	// Indicator class to show the user in which direction an enemy ship is. 
	// Disapears when the target ship is too close to the player ship.

	Transform targetShip;

	Transform playerShip;

	float radius = 3f;
	float minDis = 5f;

	SpriteRenderer spriteRenderer;

	[SerializeField]
	Color spriteColor;
	// Use this for initialization
	public void Setup (Transform targetShip) {
		this.targetShip = targetShip;
		playerShip = FindObjectOfType<PlayerMovement> ().transform;
		spriteRenderer = GetComponentInChildren<SpriteRenderer> ();
	}
	
	// Update is called once per frame
	void Update () {
		if (targetShip == null) {
			Destroy (gameObject);
			return;
		}
		Vector2 direction = targetShip.position - playerShip.position;
		float distance = direction.magnitude;

		spriteRenderer.color = distance < minDis ? Color.clear : spriteColor;

		transform.position = playerShip.position + (Vector3)direction.normalized * radius;

		float angle = Vector2.Angle(Vector2.up, direction);
		angle *= direction.x > 0 ? -1 : 1;

		transform.rotation = Quaternion.Euler(new Vector3(0,0,angle));
	}
}
