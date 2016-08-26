using UnityEngine;
using System.Collections;

public class GridMap  {
	int width;
	int height;
	public GridMap(int width, int height){
		this.width = width;
		this.height = height;
	}

}

public class GridTile{
	bool isAnchor;
	GridTile anchor;
	int[,] position; 
}

public class AnchorTile : GridTile{
	GridTile[,] connectedTiles;
}


