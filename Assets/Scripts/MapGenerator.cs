using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapGenerator{

	// Data-class for the starmap-module. GEnerates a starmap by building a simple graph.
	// Graph is stored in a 2D-array with values 1 for edge exisiting, and 0 for no edge

	public int[,] graph;

	public int numberOfStars;
	// Using structs for pairing node-ids and physical location.
	public StarInfo[] starInfos;

	public MapGenerator(int numberOfStars){
		this.numberOfStars = numberOfStars;
		starInfos = new StarInfo[numberOfStars];
		graph = new int[numberOfStars, numberOfStars];

		GenerateMapInfo ();
	}

	void GenerateMapInfo(){
		StarInfo start = new StarInfo (0, new Vector2(0,0));
		starInfos [0] = start;
		for (int i = 1; i < numberOfStars; i++) {
			StarInfo star = new StarInfo (i, new Vector2 (2*i + Random.Range (-1.2f, 1.2f),Random.Range (-2.7f, 2.7f)));
			starInfos [i] = star;
		}

		graph [0, 1] = 1;
		graph [0, 2] = 1;

		graph [1, 0] = 1;
		graph [2, 0] = 1;

		for (int i = 3; i < numberOfStars; i++) {
			List<int> potentialNeigbors = new List<int>{1,2,3};
			int n1 = potentialNeigbors [Random.Range (0, 3)];
			int n2 = Random.Range (0f, 1f) < 0.5 ? potentialNeigbors [0] : -1;
			int n3 = Random.Range (0f, 1f) < 0.5 ? potentialNeigbors [0] : -1;

			graph [i, i - n1] = 1;
			graph [i - n1, i ] = 1;

			if (n2 != -1) {
				graph [i, i - n2] = 1;
				graph [i - n2, i] = 1;

			}
			if (n3 != -1) {
				graph [i, i - n3] = 1;
				graph [i - n3, i] = 1;
			}
		}
	}
}


public struct StarInfo{
	public int id;
	public Vector2 location;

	public StarInfo(int id, Vector2 location){
		this.id = id;
		this.location = location;
	}
}