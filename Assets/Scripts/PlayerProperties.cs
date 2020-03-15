using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerProperties : MonoBehaviour {

	// Component to hold all player properties that can change during the game through upgrades
	// Other player-components use the data in this class

	public int MaxHealth;
	public float AccelerationRate;
	public int Damage;
	public float FireRate;

	public int Coins { get; protected set;}

	// Use this for initialization
	void Start () {
		InitValues ();
	}
		
	void InitValues(){
		MaxHealth = 5;
		AccelerationRate = 2f;
		Damage = 1;
		FireRate = 1f;
	}

	public void PickupCoin(){
		Coins++;
		EventSystem.Current.FireEvent (EventTypeEnum.COIN_CHANGED, new CoinChangedED ("Pickup", Coins));
	}

	public void Pay(int amount){
		Coins -= amount;
		EventSystem.Current.FireEvent (EventTypeEnum.COIN_CHANGED, new CoinChangedED ("Pay", Coins));
	}

}
