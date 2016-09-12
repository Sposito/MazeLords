using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.Serialization;
[System.Serializable]
public class GridMap  {
	[SerializeField]
	int width;
	[SerializeField]
	int height;
	[SerializeField]
	GridTile[] grid;

	public GridTile[] Grid{get{return grid;}}


	public GridMap(int width, int height){
		this.width = width;
		this.height = height;
		grid = new GridTile[width * height];
	}

	public bool AddTile(Position pos, GridTile tile){
		if (grid [GetIndex(pos)].IsEmpty && CheckFour(pos)) {
			grid [GetIndex(pos)] = tile;
			grid [GetIndex(pos)].SetPosition (pos);
			return true;
		} 
		else return false;

	}

	int GetIndex(int x, int y){
		return y * width + x;
	}
	int GetIndex(Position pos){
		return pos.y * width + pos.x;
	}

	bool CheckDiagonal(Position pos){
		Position[] postions = new Position[4];
		postions[0] = pos.NE;
		postions[1] = pos.SE;
		postions[2] = pos.SW;
		postions[3] = pos.NW;

		bool result = true;
		foreach (Position p in postions) {
			if (!(p.x < 0 || p.x >= width || p.y < 0 || p.y >= height)) {
				result = result && grid [GetIndex (p)].IsEmpty;
			}
		}
		return result;
	}

	public bool CheckFour(Position pos){
		Position[] postions = new Position[8];
		postions[0] = pos.N;
		postions[1] = pos.NE;
		postions[2] = pos.E;
		postions[3] = pos.SE;
		postions[4] = pos.S;
		postions[5] = pos.SW;
		postions[6] = pos.W;
		postions[7] = pos.NW;

		bool[] boolMap = new bool[8];
		Position p;
		for(int i = 0; i<8;i++){
			p = postions [i];
			if (p.x < 0 || p.x >= width || p.y < 0 || p.y >= height) {
				boolMap [i] = true;
			} 
			else {
				boolMap [i] = !(grid [GetIndex (p)].IsEmpty);
			}
		}
		int next = 0;
		for (int i = 1; i < 8; i += 2) {
			if (boolMap [i]) {
				next = i + 1;
				if (next == 8) {
					next = 0;
				}

				if (boolMap [next] && boolMap [i - 1]) {
					return false;
				}
			}

		}
		return true;
	
	}


}