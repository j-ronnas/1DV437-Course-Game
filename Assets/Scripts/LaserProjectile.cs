using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LaserProjectile : MonoBehaviour {

	// Class that handles moving of projectiles. 

	[SerializeField]
	float speed;
	Vector2 velocity;


	//Setup-method that passes on damage and if its friendly to a HurtOnTouch component. 
	// Should check if such a component exists but doesn't because a projectiles must always have a HurtOnTouch (as of now).
	public void Setup (Vector2 direction, Vector2 initVel, int damage, bool friendly) {
		GetComponent<HurtOnTouch> ().Setup (damage, friendly);

		velocity = direction.normalized*speed + initVel;
		float angle = Vector2.Angle(Vector2.up, velocity);
		angle *= velocity.x > 0 ? -1 : 1;

		transform.rotation = Quaternion.Euler(new Vector3(0,0,angle));
	}

	void Update(){
		transform.position += (Vector3)velocity * Time.deltaTime;
	}



}
