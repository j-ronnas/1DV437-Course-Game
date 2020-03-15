using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.UI;

public class SoundManager : MonoBehaviour {

	// Class that uses listiners and plays sounds on events.

	[SerializeField]
	NamedSound[] soundArray;
	Dictionary<string, AudioClip> sounds;
	Dictionary<string, AudioSource> sources;


	AudioClip a;
	[SerializeField]
	Slider volumeSlider;

	// Use this for initialization
	void Start () {
		InitDictionary ();
		EventSystem.Current.RegisterListener (EventTypeEnum.ENEMY_KILLED, OnEnemyKilled);
		EventSystem.Current.RegisterListener (EventTypeEnum.COIN_CHANGED, OnCoinPickup);
		EventSystem.Current.RegisterListener (EventTypeEnum.WARPING, OnWarping);
		EventSystem.Current.RegisterListener (EventTypeEnum.PLAYER_HEALTH_CHANGED, OnTakeDamage);
		EventSystem.Current.RegisterListener (EventTypeEnum.SHOT_FIRED, OnShotFired);
		EventSystem.Current.RegisterListener (EventTypeEnum.ENEMY_HIT, OnEnemyHit);

		if (PlayerPrefs.HasKey ("Volume")) {
			SetVolume (PlayerPrefs.GetFloat ("Volume"));
			volumeSlider.value = PlayerPrefs.GetFloat ("Volume");
		} else {
			volumeSlider.value = 1f;
			SetVolume (1f);
		}
	}

	public void SetVolume(float amount){
		amount = Mathf.Clamp01 (amount);
		PlayerPrefs.SetFloat ("Volume", amount);
		PlayerPrefs.Save ();
		foreach (var item in sources) {
			item.Value.volume = amount;
		}
	}


	void InitDictionary(){
		sounds = new Dictionary<string, AudioClip> ();
		sources = new Dictionary<string, AudioSource> ();
		foreach (NamedSound item in soundArray) {
			GameObject go = new GameObject (item.id + "GO");
			go.transform.parent = transform;
			AudioSource source = go.AddComponent<AudioSource> ();
			source.clip = item.audioClip;
			source.playOnAwake = false;
			sources.Add (item.id, source);
		}
	}

	void OnEnemyKilled(EventData ed){
		PlaySound ("EnemyDeath");
	}

	void OnCoinPickup(EventData ed){
		if (ed.message == "Pickup") {
			PlaySound ("CoinPickup");
		}
	}

	void OnWarping(EventData ed){
		PlaySound ("Warping");
	}

	void OnTakeDamage(EventData ed){
		if (ed.message == "DamageTaken") {
			PlaySound ("PlayerHit"); 
		}
	}

	void OnShotFired(EventData ed){
		if (ed.message == "FriendlyShotFired") {
			PlaySound ("FriendlyShotFired");
		} else if (ed.message == "EnemyShotFired") {
			PlaySound ("EnemyShotFired");
		}	
	}

	void OnEnemyHit(EventData ed){
		PlaySound ("EnemyHit");
	}



//	void PlaySound(string key){
//		if (sounds.ContainsKey (key) == false) {
//			return;
//		}
//
//		source.Stop ();
//		source.clip = sounds[key];
//		source.Play ();
//	}

	void PlaySound(string key){
		if (sources.ContainsKey (key) == false) {
			return;
		}
		//sources [key].Stop ();

		sources [key].Play ();

	}
}

[Serializable]
struct NamedSound{
	public string id;
	public AudioClip audioClip;
}
