using System;
using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.Serialization;
[Serializable]
public class ChestTile : GridTile {
	
	public List<GridTile> contentList;
	[SerializeField]
	public int capacity;
	[SerializeField]
	public GridTile[] content;
	public ChestTile(int id, int capacity){
		base.id = id;
		this.capacity = capacity;
		contentList = new List<GridTile> ();
	}

	public void BuildList(){
		foreach (GridTile g in content) {
			contentList.Add (g);
		}
		contentList.Sort ();
	}

	public void BuildArray(){
		contentList.Sort ();
		content = contentList.ToArray ();
	}

	public bool RemoveTile( GridTile tile){
		
		return contentList.Remove (tile);
		
	}
	public bool AddTile( GridTile tile){
		bool hasSpace = contentList.Count <= capacity;
		if (hasSpace) {
			contentList.Add (tile);
		}
		return hasSpace; 

	}

	public void BaseFill(){
		contentList.Add(new GridTile(401)); //chest
		contentList.Add(new GridTile(301)); //start point
		contentList.Add(new GridTile(302));//endpoint
		int i = 80;

		while ( i > 0){
			contentList.Add (new GridTile (1));//base tile
			i--;
		}
		contentList.Sort ();
	}

}
