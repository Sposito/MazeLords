using UnityEngine;
using System.Collections;

public class LevelEditorController : MonoBehaviour {

	int width = 32;
	int height = 32;

	GameObject[,] grid;

	void Start () {
		GameObject tile = (GameObject)Resources.Load ("Prefabs/Tile");
		grid = new GameObject[width, height];
		for (int i = 0; i < width; i++) {
			for (int j = 0; j < height; j++) {
				grid [i, j] = (GameObject)Instantiate (tile, new Vector3 (i, 0, j), Quaternion.identity);
				grid [i, j].transform.SetParent (transform);
			}
		}

	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
