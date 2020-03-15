using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class UpgradeManager : MonoBehaviour {
	// Contains functions for upgrading the ship as well as manages the UI-buttons. 


	UpgradeButton[] upgradeButtons;


	[SerializeField]
	GameObject healthButton;
	[SerializeField]
	GameObject speedButton;
	[SerializeField]
	GameObject damageButton;
	[SerializeField]
	GameObject fireRateButton;
	[SerializeField]
	GameObject restoreButton;

	PlayerProperties playerProps;
	PlayerHealth playerHealth;


	void OnEnable () {
		if (playerProps == null) {
			playerProps = FindObjectOfType<PlayerProperties> ();
		}
		if (playerHealth == null) {
			playerHealth = FindObjectOfType<PlayerHealth> ();
		}

		UpdateButtons ();
	}

	void InitButtons(){
		upgradeButtons = new UpgradeButton[5];
		upgradeButtons [0] = new UpgradeButton ("Increase maximum health", healthButton, 10);
		upgradeButtons [0].button.GetComponent<Button> ().onClick.AddListener( UpgradeHealth);

		upgradeButtons [1] = new UpgradeButton ("Increase ship speed", speedButton, 5);
		upgradeButtons [1].button.GetComponent<Button> ().onClick.AddListener( UpgradeSpeed);

		upgradeButtons [2] = new UpgradeButton ("Increase gun power", damageButton, 20);
		upgradeButtons [2].button.GetComponent<Button> ().onClick.AddListener(UpgradeDamage);

		upgradeButtons [3] = new UpgradeButton ("Increase gun fire rate", fireRateButton, 5);
		upgradeButtons [3].button.GetComponent<Button> ().onClick.AddListener(UpgradeFireRate);

		upgradeButtons [4] = new UpgradeButton ("Restore health", restoreButton, 1);
		upgradeButtons [4].button.GetComponent<Button> ().onClick.AddListener(RestoreHealth);

	}

	// Update is called once per frame
	void Update () {
		if (Input.GetKeyDown(KeyCode.E)) {
			ExitStation ();
		}
	}

	void UpdateButtons(){
		if (upgradeButtons == null) {
			InitButtons ();
		}
		foreach (UpgradeButton item in upgradeButtons) {
			string s = item.text + " | $" + item.cost.ToString();
			item.button.GetComponentInChildren<Text> ().text = s;
			if (item.cost > playerProps.Coins) {
				item.button.GetComponent<Button> ().interactable = false;
			} else {
				item.button.GetComponent<Button> ().interactable = true;
			}
		}
	}



	public void UpgradeHealth(){
		playerProps.MaxHealth += 1;
		EventSystem.Current.FireEvent (EventTypeEnum.PLAYER_HEALTH_CHANGED, new PlayerHealthChangedED ("Upgrading health", playerHealth.Health,playerProps.MaxHealth));
		playerProps.Pay(upgradeButtons [0].cost);
		upgradeButtons [0].cost += 10;
		UpdateButtons ();
	}

	public void UpgradeSpeed(){
		playerProps.AccelerationRate *= 1.2f;
		playerProps.Pay (upgradeButtons [1].cost);
		upgradeButtons [1].cost += 5;
		UpdateButtons ();
	}

	public void UpgradeDamage(){
		playerProps.Damage = 2;
		playerProps.Pay (upgradeButtons [2].cost);
		upgradeButtons [2].button.SetActive (false);
		UpdateButtons ();
	}

	public void UpgradeFireRate(){
		playerProps.FireRate *= 1.5f;
		playerProps.Pay (upgradeButtons [3].cost);
		upgradeButtons [3].cost += 5; 
		UpdateButtons ();
	}

	public void RestoreHealth(){
		playerHealth.RestoreHealth ();
		playerProps.Pay (upgradeButtons [4].cost);
		UpdateButtons ();
	}

	public void ExitStation(){
		FindObjectOfType<GameManager> ().SetState (GameState.COMBAT);
	}
}


class UpgradeButton{

	//Simple class for holding the information needed for each type of upgrade
	public string text;
	public GameObject button;
	public int cost;

	public UpgradeButton(string text, GameObject button, int cost){
		this.text = text;
		this.button = button;
		this.cost = cost;
	}
}
