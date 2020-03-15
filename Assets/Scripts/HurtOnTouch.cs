using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HurtOnTouch : MonoBehaviour {

	//Component that hurts on collision if the other object has a health component. 
	// Can be set as friendly or not depending if it interacts with players or enemies.

	[SerializeField]
	bool friendly;
	[SerializeField]
	int damage;

	// Use this for initialization
	public void Setup (int damage, bool friendly) {
		this.damage = damage;
		this.friendly = friendly;
	}



	void OnTriggerEnter2D(Collider2D coll){
		if (friendly) {
			if (coll.tag == "Player") {
				return;
			}
			EnemyHealth health = coll.GetComponent<EnemyHealth> ();

			if (health != null) {
				health.TakeDamage (damage);
				Destroy (gameObject);
			}

		} else {
			if (coll.tag != "Player") {
				return;
			}
			PlayerHealth health = coll.GetComponent<PlayerHealth> ();

			if (health != null) {
				health.TakeDamage (damage);
			}

			Destroy (gameObject);
		}
	}
}
