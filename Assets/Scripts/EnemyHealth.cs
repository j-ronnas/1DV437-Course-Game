using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHealth : MonoBehaviour {

	[SerializeField]
	int maxHealth;
	int health;

	[SerializeField]
	GameObject deathAnim;

	// Use this for initialization
	void Start () {
		health = maxHealth;
	}
	
	// Update is called once per frame
	float timer = 1;
	void Update () {
		if (timer < 1) {
			timer += Time.deltaTime;
		} else {
			timer = 1;
		}

		GetComponentInChildren<SpriteRenderer> ().color = Color.Lerp (Color.red, Color.white, timer);
	}

	public void TakeDamage(int amount = 1){
		health -= amount;
		timer = 0;
		EventSystem.Current.FireEvent (EventTypeEnum.ENEMY_HIT, new EventData ("EnemyHit"));
		if (health <= 0) {
			Die ();
		}
	}

	void Die(){
		EventSystem.Current.FireEvent (EventTypeEnum.ENEMY_KILLED, new EnemyKilledED ("Enemy died!", transform.position));
		Instantiate (deathAnim, transform.position, Quaternion.identity);
		Destroy (gameObject);
	}

}
