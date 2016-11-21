using UnityEngine;
using System.Collections;
using System.Collections.Generic;
public class GridTiles : GridTile {

	int amount;

	public int Amount{get{return amount;}}

	public GridTiles(int id, int amount){
		this.id = id;
		this.amount = amount;
		this.IsEmpty = false;
		if (id == -1) {
			this.IsEmpty = true;
		}
	}

	public bool DecreaseAndCheck(){
		amount -= 1;
		return amount <= 0;
	}

	public void Increase(){
		amount += 1;
	}
}
