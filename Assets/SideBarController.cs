using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;

public class SideBarController : MonoBehaviour {

	// Use this for initialization
	LevelEditorController controller;
	public Text[] amounts;

	void Start () {
		controller = FindObjectOfType<LevelEditorController> ();

		//Build ();
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void Build(){
		

		List<GridTiles> tiles = controller.GetChest ().contentList;
		tiles.Sort ();
		int current = -1;
		foreach (GridTiles t in tiles) {
			if (t.Id != current) {
				
			}
		}
	}
}
