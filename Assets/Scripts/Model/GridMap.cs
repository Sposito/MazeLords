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
	GridTile[] gridMap;
	[SerializeField]
	public ChestTile chest;
	public Vector2 Size {get{ return new Vector2 ((float)width, (float)height);}}
	public GridTile[] Grid{get{return gridMap;}}


	public GridMap(int width, int height){
		this.width = width;
		this.height = height;
		gridMap = new GridTile[width * height];
	}

	public  void SetBaseGridMap(){
		
		chest = new ChestTile (100, 99);
		chest.BaseFill ();
		chest.BuildArray ();
	}

	public bool AddTile(Position pos, GridTile tile){
		if (gridMap [GetIndex(pos)].IsEmpty && CheckFour(pos)) {
			gridMap [GetIndex(pos)] = tile;
			gridMap [GetIndex(pos)].SetPosition (pos);
			return true;
		} 
		else return false;

	}

	public GridTile RemoveTile(Position pos){
		GridTile backup = gridMap [GetIndex (pos)];
		gridMap [GetIndex (pos)] = GridTile.Empty;
		if (backup == null) {
			
			return null;
		} 
		else if (backup.IsEmpty) {
			return null;
		} 
		else {
			Debug.Log (gridMap [GetIndex (pos)]);
			return backup;
		}
	}

	public bool MoveTile(Position pos, GridTile tile){
		if(gridMap[GetIndex(pos)].IsEmpty)
			return false;
		gridMap [GetIndex (pos)] = tile;
		return true;
		
	}

	int GetIndex(int x, int y){
		return y * width + x;
	}

	int GetIndex(Position pos){
		return pos.y * width + pos.x;
	}

	public void SetEmpty(Position pos, bool empty){
		gridMap [GetIndex (pos)].IsEmpty = empty;
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
				result = result && gridMap [GetIndex (p)].IsEmpty;
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
				boolMap [i] = !(gridMap [GetIndex (p)].IsEmpty);
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