using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour {

	// AI-logic for enemyships.
	// Ships chooses at random intervals a target nearby the player.
	// It then moves towards this point by rotating until in the correct direction and moving forward.
	// Interactions and potential collisions with other ships is handled by the built in rigidbody and physics system in Unity

	Rigidbody2D rb;
	Vector2 currentTarget;
	Transform playerPos;


	// Use this for initialization
	void Start () {
		rb = GetComponent<Rigidbody2D> ();
		playerPos = FindObjectOfType<PlayerMovement> ().transform;
	}
	
	// Update is called once per frame
	void Update () {
		ChooseNewTarget ();
		MoveTowards ();
	}

	float timer = 0;
	void ChooseNewTarget(){
		timer -= Time.deltaTime;
		if (timer > 0) {
			return;
		}

		timer = Random.Range (1f, 3f);

		currentTarget = playerPos.position + 3f*(Vector3)Random.insideUnitCircle;

	}

	void MoveTowards(){
		float angle = Vector2.Angle (transform.up, (Vector3)currentTarget - transform.position);
		float dot = Vector2.Dot (transform.right, (Vector3)currentTarget - transform.position);

		float rotationSpeed = dot < 0 ? 60 : -60;

		rb.rotation += rotationSpeed * Time.deltaTime;

		float t = Mathf.Clamp (angle / 90, 0, 1);

		float directionFactor = Mathf.Lerp (1, 0, t);

		rb.velocity += (Vector2) transform.up * directionFactor * Time.deltaTime;
	}
		
}
