using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoinComponent : MonoBehaviour {

	// Handles logic of coin colliding with player

	GameObject player;

	// Use this for initialization
	void Start () {
		player = GameObject.FindGameObjectWithTag ("Player");
	}
	
	// Update is called once per frame
	void Update () {
		Vector2 dir = (player.transform.position - transform.position).normalized;
		transform.position += (Vector3)dir * Time.deltaTime * 2f;
	}

	void OnTriggerEnter2D(Collider2D coll){
		if (coll.tag == "Player") {
			coll.GetComponent<PlayerProperties> ().PickupCoin ();
			Destroy (gameObject);
		}
	}
}
