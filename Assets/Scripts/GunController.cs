using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GunController : MonoBehaviour {

	//Handles the logic for the player firing projectiles.

	[SerializeField]
	GameObject laserPrefab;
	[SerializeField]
	Transform projectilesParent;
	[SerializeField]
	Transform gunLocation;

	GameObject playerObject;
	PlayerProperties pp;

	float timer;
	// Use this for initialization
	void Start () {
		playerObject = FindObjectOfType<PlayerMovement> ().gameObject;
		pp = playerObject.GetComponent<PlayerProperties> ();
	}
	
	// Update is called once per frame
	void Update () {
		if (timer < 1 / pp.FireRate) {
			timer += Time.deltaTime;
		} else if (Input.GetMouseButton (0)) {
			timer -= 1 / pp.FireRate;

			GameObject go = Instantiate (laserPrefab, gunLocation.position, Quaternion.identity, projectilesParent);
			go.GetComponent<LaserProjectile> ().Setup (
				Camera.main.ScreenToWorldPoint (Input.mousePosition) - transform.position, 
				(Vector3)playerObject.GetComponent<Rigidbody2D> ().velocity,
				pp.Damage, 
				true
			);

			EventSystem.Current.FireEvent (EventTypeEnum.SHOT_FIRED, new EventData ("FriendlyShotFired"));
		}

	}
}
