using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScrollingBackground : MonoBehaviour {

	// Script for creating a parallax effect. By chaning parallaxDist and using several, the layes will move at different speeds.

	Material material;
	Transform playerPos;

	Vector2 randomStart;

	[SerializeField]
	float parallaxDist = 1;

	// Use this for initialization
	void Start () {
		material = GetComponent<MeshRenderer> ().material;
		playerPos = GameObject.FindObjectOfType<PlayerMovement> ().transform;
		randomStart = new Vector2 (Random.Range(0f,1f), Random.Range (0f,1f));
		float scale = Camera.main.aspect * Camera.main.orthographicSize * 2;
		transform.localScale = new Vector3 (1.2f*scale, 1.2f*scale, 1f);
	}
	
	// Update is called once per frame
	void Update () {
		transform.position = playerPos.position;

		Vector2 offset = material.mainTextureOffset;
		offset = randomStart + new Vector2 (transform.position.x/transform.localScale.x, transform.position.y/transform.localScale.y)/parallaxDist;

		material.mainTextureOffset = offset;
	}
}
