using UnityEngine;
using System.Collections;
/// <summary>
/// Tile position, (int touple with maze related methods
/// </summary>
[System.Serializable]
public class Position {
	public static Position Zero { get { return new Position (0, 0); } }

	public Position N { get{return new Position(  x, y+1);}}
	public Position S { get{return new Position(  x, y-1);}}
	public Position E { get{return new Position(x+1, y  );}}
	public Position W { get{return new Position(x-1, y  );}}

	public Position NE{ get{return new Position(x+1,  y+1);}}
	public Position SE{ get{return new Position(x+1,  y-1);}}
	public Position NW{ get{return new Position(x-1,  y+1);}}
	public Position SW{ get{return new Position(x-1,  y-1);}}

	public int x = 0;
	public int y = 0;

	public Position(int x, int y){
		this.x = x;
		this.y = y;
	}

	public override string ToString ()
	{
		return string.Format ("{0}, {1}", x, y);
	}
}