using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyShooter : MonoBehaviour {

	// Some enmies have this component attached which will fire at the player at random intervals when inside a specified range

	GameObject player;
	float timer;

	[SerializeField]
	float minShootRange = 5f;
	[SerializeField]
	float maxShootRange = 15f;

	[SerializeField]
	GameObject projectilePrefab;

	[SerializeField]
	int damage;
	// Use this for initialization
	void Start () {
		player = GameObject.FindGameObjectWithTag ("Player");
		timer = Random.Range (1f, 3f);
	}
	
	// Update is called once per frame
	void Update () {
		timer -= Time.deltaTime;
		if (timer < 0) {
			Vector2 dir = player.transform.position - transform.position;
			if (dir.sqrMagnitude < maxShootRange && dir.sqrMagnitude > minShootRange) {
				GameObject go = Instantiate (projectilePrefab, transform.position, Quaternion.identity);
				go.GetComponent<LaserProjectile> ().Setup (dir, GetComponent<Rigidbody2D> ().velocity, damage, false);
				EventSystem.Current.FireEvent (EventTypeEnum.SHOT_FIRED, new EventData ("EnemyShotFired"));
				timer = Random.Range (1f, 3f);
			}
		}
	}
}
