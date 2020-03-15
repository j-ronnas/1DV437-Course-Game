using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraShake : MonoBehaviour {
	// A simple shake effect by randomly offsetting the camera a small amount each frame.

	public delegate void Callback();
	Callback OnFinished;
	float timer;
	Vector3 startPosition;
	bool active = false;
	float jumpAmount = 0.7f;

	// Update is called once per frame
	void Update () {
		if (active == false) {
			return;
		}
		if (timer > 0) {
			timer -= Time.deltaTime;
			transform.position  = startPosition + jumpAmount * (Vector3)Random.insideUnitCircle;
			return;
		}
		transform.position = startPosition;
		OnFinished ();
		active = false;
		GetComponent<CombatCamera> ().enabled = true;
	}

	//OnFinished is called to let the caller know that the shake effect is done.
	public void StartShake(float duration, Callback OnFinished){
		startPosition = transform.position;
		timer = duration;
		active = true;
		GetComponent<CombatCamera> ().enabled = false;
		this.OnFinished = OnFinished;
	}
}
