using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EventData  {
	// Empty base data class for passing information to listeners
	public EventData(string message){
		this.message = message;
	}

	public string message;
}

public enum EventTypeEnum {
	NEW_LEVEL,
	ENEMY_KILLED,
	LEVEL_CLEARED,
	PLAYER_DEATH,
	PLAYER_HEALTH_CHANGED,
	COIN_CHANGED,
	WARPING,
	SHOT_FIRED,
	ENEMY_HIT,
}

// Below are simple classes for passing relevant data when an event is fired.

public class NewLevelED : EventData{
	public int levelIndex;
	public NewLevelED(string message, int levelIndex):base(message){
		this.levelIndex = levelIndex;
	}
}

public class EnemyKilledED : EventData{
	public Vector3 enemyPosition;
	public EnemyKilledED(string message, Vector3 enemyPosition) : base(message){
		this.enemyPosition = enemyPosition;
	}
}

public class PlayerHealthChangedED : EventData{
	public int health;
	public int maxHealth;
	public PlayerHealthChangedED(string message, int health, int maxHealth):base(message){
		this.health = health;
		this.maxHealth = maxHealth;
	}
}

public class CoinChangedED : EventData{
	public int newAmount;
	public CoinChangedED(string message, int newAmount):base(message){
		this.newAmount = newAmount;
	}
}

public class LevelClearedED : EventData{
	public bool finalLevel;
	public LevelClearedED(string message, bool finalLevel):base(message){
		this.finalLevel = finalLevel;
	}
}





	
