using System;
using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.Serialization;
[Serializable]
public class ChestTile : GridTile {
	
	public List<GridTiles> contentList;
	[SerializeField]
	public int capacity;
	[SerializeField]
	public GridTiles[] content;

	int totalItens = 0;
	public ChestTile(int id, int capacity){
		base.id = id;
		this.capacity = capacity;
		contentList = new List<GridTiles> ();
	}

	int CountTotalItens(){
		int _totalItens = 0;
		for (int i = 0; i < contentList.Count; i++) {
			_totalItens += contentList [i].Amount; 
		}
		return _totalItens;
	}

	public void BuildList(){
		foreach (GridTiles g in content) {
			contentList.Add (g);
		}
		totalItens = CountTotalItens ();
		contentList.Sort ();
	}

	public void BuildArray(){
		contentList.Sort ();
		content = contentList.ToArray ();

	}

	public bool RemoveTile( GridTile tile){
		for (int i = 0; i < contentList.Count; i++) {
			if (contentList[i].Id == tile.Id) {
				if (contentList[i].DecreaseAndCheck ()) {//decrease the amount and check if there is any tiles 
					contentList.RemoveAt(i);
				}
				totalItens -= 1;
				return true; //Returning trues means there was a tile to be removed;
			}
		}
		return false; //it will return false if it iterates the list and doest find nothing to remove;
	}

	/// <summary>
	/// Gets the amount of certain item in the chest.
	/// </summary>
	/// <returns>The amount of itens of a Given ID.</returns>
	/// <param name="id">Identifier.</param>
	public int GetAmount(int id){
		for (int i = 0; i < contentList.Count; i++) {
			if (contentList[i].Id == id) {
				return contentList [i].Amount;
			}
		}
		return 0;
	}

	public bool AddTile( GridTile tile){
		if (capacity <= totalItens) {
			return false; //it will return false if the list is full
		}
		//First it looks if there is already this item on the list
		for (int i = 0; i < contentList.Count; i++) {
			
			if (contentList[i].Id == tile.Id) {
				contentList [i].Increase ();
				totalItens += 1;
				return true;
			}

		}
		//if it doest find any and exits the loop it add a new item on the list
		contentList.Add (new GridTiles (tile.Id, 1));
		return true; 

	}

	public void BaseFill(){
		AddTile(new GridTile(401)); //chest
		AddTile(new GridTile(301)); //start point
		AddTile(new GridTile(302));//endpoint
		int i = 80;

		while ( i > 0){
			AddTile(new GridTile (1));//base tile
			i--;
		}
		contentList.Sort ();
	}

}
