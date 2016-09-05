using UnityEngine;
using System.Collections;
/// <summary>
/// Tile position, (int touple with maze related methods
/// </summary>
[System.Serializable]
public class Position {
	
	public  int x;
	public int y;

	public Position(int x, int y){
		this.x = x;
		this.y = y;
	}
}