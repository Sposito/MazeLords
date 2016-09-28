using UnityEngine;
using System.Collections;
using System.Collections.Generic;
public class SideBarController : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void Build(){
		LevelEditorController controller  = LevelEditorController.Singleton;

		List<GridTile> tiles = controller.GetChest ().contentList;
		tiles.Sort ();
		int current = -1;
		foreach (GridTile t in tiles) {
			if (t.Id != current) {
				
			}
		}
	}
}
