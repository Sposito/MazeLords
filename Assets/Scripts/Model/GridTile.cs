/// <summary>
/// Base maze tile
/// </summary>
using UnityEngine;
[System.Serializable]
public class GridTile{
	[SerializeField]
	bool isAnchor;
	GridTile anchor;
	[SerializeField]
	Position position;
	[SerializeField]
	int id;

	public int X{get{ return position.x;}}
	public int Y{get{ return position.y;}} 
	public bool IsEmpty = true;

	public static GridTile BasicBlock{get{return new GridTile(1);}}
	public GridTile(){
		
	}
	public GridTile(int id){
		this.id = id;
		this.IsEmpty = false;
	}

	public void SetPosition(Position position){
		this.position = position;
	}
}