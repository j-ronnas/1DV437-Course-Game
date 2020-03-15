using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour {

	// Player movement is done through the Rigidbody2D system, here using hardcoded inputkeys. 
	// Perhaps should be changed to use Unity's inputsystem, or a self-made one.

	Rigidbody2D rb;
	float rotationRate = 90f;

	PlayerProperties pp;

	[SerializeField]
	GameObject fireEffect;
	// Use this for initialization
	void Start () {
		rb = GetComponent<Rigidbody2D> ();
		pp = GetComponent<PlayerProperties> ();
	}
	
	// Update is called once per frame
	void Update () {
		float y = 0;
		if (Input.GetKey (KeyCode.W)) {
			y = pp.AccelerationRate;
			fireEffect.SetActive (true);
		} else {
			fireEffect.SetActive (false);
		}
		if (Input.GetKey(KeyCode.S)){
			y = -pp.AccelerationRate;
		}
		if (Input.GetKey(KeyCode.D)){
			rb.rotation +=  -(Time.deltaTime * rotationRate);
		}
		if (Input.GetKey(KeyCode.A)){
			rb.rotation +=  (Time.deltaTime * rotationRate);
		}
		rb.velocity += (Vector2)transform.up * y *Time.deltaTime;
		rb.angularVelocity = 0;

	}
}
