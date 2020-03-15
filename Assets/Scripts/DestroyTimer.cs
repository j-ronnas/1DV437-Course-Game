using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyTimer : MonoBehaviour {

	//Destroys game object after set time.

	[SerializeField]
	float time;

	// Update is called once per frame
	void Update () {
		time -= Time.deltaTime;
		if (time < 0) {
			Destroy (gameObject);
		}
	}
}
