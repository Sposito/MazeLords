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
	Position position = Position.Zero;
	[SerializeField]
	protected int id;

	public int X{get{ return position.x;}}
	public int Y{get{ return position.y;}} 
	public bool IsEmpty = true;

	public static GridTile BasicBlock{get{return new GridTile(1);}}
	public static GridTile Empty{get{return new GridTile(-1);}}
	public GridTile(){
		
	}
	public GridTile(int id){
		this.id = id;
		this.IsEmpty = false;
		if (id == -1) {
			this.IsEmpty = true;
		}
	}

	public void SetPosition(Position position){
		this.position = position;
	}

//	public override string ToString ()
//	{
//		return string.Format ("[X={0}, Y={1}, isEmpty={2}]", X, Y, IsEmpty);
//	}
}