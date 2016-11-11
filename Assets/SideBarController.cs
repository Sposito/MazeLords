using UnityEngine;
using System.Collections;
using System.Collections.Generic;
public class SideBarController : MonoBehaviour {

	// Use this for initialization
	LevelEditorController controller;
	void Start () {
		controller  = LevelEditorController.Singleton;
		//Build ();
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void Build(){
		

		List<GridTile> tiles = controller.GetChest ().contentList;
		tiles.Sort ();
		int current = -1;
		foreach (GridTile t in tiles) {
			if (t.Id != current) {
				
			}
		}
	}
}
