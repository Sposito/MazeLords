using System;
using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.Serialization;
[Serializable]
public class ChestTile : GridTile {
	[SerializeField]
	public List<GridTile> content;
	[SerializeField]
	public int capacity;

	public ChestTile(int id, int capacity){
		base.id = id;
		this.capacity = capacity;
		content = new List<GridTile> ();
	}

	public void BaseFill(){
		content.Add(new GridTile(301));
		content.Add(new GridTile(302));
		int i = 80;

		while ( i > 0){
			content.Add (new GridTile (1));
			i--;
		}
	}

}
