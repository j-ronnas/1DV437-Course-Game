using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealth : MonoBehaviour {

	public int Health { get; protected set;}
	PlayerProperties pp;

	// Use this for initialization
	void Start () {
		pp = GetComponent<PlayerProperties> ();
		RestoreHealth ();
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	public void RestoreHealth(){
		Health = pp.MaxHealth;
		EventSystem.Current.FireEvent (EventTypeEnum.PLAYER_HEALTH_CHANGED, new PlayerHealthChangedED ("Health restored", Health, pp.MaxHealth));
	}

	public void TakeDamage(int amount = 1){
		Health -= amount;
		EventSystem.Current.FireEvent (EventTypeEnum.PLAYER_HEALTH_CHANGED, new PlayerHealthChangedED ("DamageTaken", Health, pp.MaxHealth));
		if (Health <= 0) {
			EventSystem.Current.FireEvent (EventTypeEnum.PLAYER_DEATH, new EventData("Player died!"));
		}
	}
}
