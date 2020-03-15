using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EventSystem : MonoBehaviour {

	// Inspired by tutorial by Quill18: https://www.youtube.com/watch?v=04wXkgfd9V8&
	// EventManager that manages the logic for recieving and sending messages between scripts. 
	// This is done using delegates and an eventdata module which holds enums for different types of events as well as data-classes

	//Singleton pattern

	static private EventSystem __Current;
	static public EventSystem Current
	{
		get
		{
			if(__Current == null)
			{
				__Current = GameObject.FindObjectOfType<EventSystem>();
			}

			return __Current;
		}
	}


	public delegate void EventListener(EventData eventData);
	Dictionary<EventTypeEnum, List<EventListener>> eventListeners;

	public void RegisterListener(EventTypeEnum eventType, EventListener listener){

		if (eventListeners == null) {
			eventListeners = new Dictionary<EventTypeEnum, List<EventListener>> ();
		}

		if (eventListeners.ContainsKey(eventType) == false || eventListeners [eventType] == null) {
			eventListeners [eventType] = new List<EventListener> ();
		}

		eventListeners [eventType].Add (listener);
	}

	public void UnRegisterListener(EventTypeEnum eventType, EventListener listener){
		if (eventListeners == null || eventListeners.ContainsKey(eventType) == false || eventListeners [eventType] == null) {
			//There are no listeners for this event
			return;
		}
		eventListeners [eventType].Remove (listener);
	}


	public void FireEvent(EventTypeEnum eventType, EventData eventData){
		if (eventListeners == null || eventListeners.ContainsKey(eventType) == false ||eventListeners [eventType] == null) {
			//There are no listeners for this event
			return;
		}
		foreach (EventListener el in eventListeners[eventType]) {
			el (eventData);
		}
	}
}


