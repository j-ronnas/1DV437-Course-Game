using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IndicatorSpawner : MonoBehaviour {

	//COmponetn that attaches to anything using an indicator (enemy ships and stations)

	[SerializeField]
	GameObject indicatorPrefab;

	[SerializeField]
	Transform indicatorParent;

	// Use this for initialization
	void Start () {
		GameObject go = Instantiate (indicatorPrefab, indicatorParent);
		go.GetComponent<ShipIndicator> ().Setup (this.transform);
	}


}
