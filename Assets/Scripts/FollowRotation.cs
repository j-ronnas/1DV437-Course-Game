using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowRotation : MonoBehaviour {

	[SerializeField]
	Transform target;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		Vector2 direction = target.position - transform.position;

		float angle = Vector2.Angle(Vector2.up, direction);
		angle *= direction.x > 0 ? -1 : 1;

		transform.rotation = Quaternion.Euler(new Vector3(0,0,angle));
	}
}
