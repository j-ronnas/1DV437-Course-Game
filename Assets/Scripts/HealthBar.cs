using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class HealthBar : MonoBehaviour {

	// UI-component for updating the healthbar when player health changes.

	[SerializeField]
	GameObject healthSquarePrefab;

	// Use this for initialization
	void Start () {
		EventSystem.Current.RegisterListener (EventTypeEnum.PLAYER_HEALTH_CHANGED, OnPlayerHealthChanged);
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	void OnPlayerHealthChanged(EventData ed){
		PlayerHealthChangedED data = (PlayerHealthChangedED)ed;
		int childCount = transform.childCount;
		for (int i = 0; i < data.maxHealth - childCount; i++) {
			Instantiate (healthSquarePrefab, this.transform);
		}

		for (int i = 0; i < transform.childCount; i++) {
			transform.GetChild (i).GetComponent<Image> ().enabled = i < data.health;
		}
	}
}
