using UnityEngine;
using System.Collections;

public class LevelEditorController : MonoBehaviour {
	[SerializeField]
	int width = 16;
	[SerializeField]
	int height = 16;

	GameObject[,] grid;

	GameObject baseCube ;

	void Start () {
		baseCube = (GameObject)Resources.Load ("Prefabs/BaseWall");
		BuildGrid (width, height);
		SetupCamera ();

	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public void TileClick(Position pos){
		//Quaternion rot = Quaternion.Euler (0f, Random.Range (0, 4) * 90f, 0f);
		GameObject go = (GameObject)Instantiate (baseCube, new Vector3 (pos.x, 0.5f, pos.y), Quaternion.identity);

		//go.transform.Translate (Vector3.up/2);
		go.transform.SetParent (transform);
		go.GetComponent<Renderer> ().material.color = Color.grey;
	}
	void SetupCamera(){
		Vector3 pos = new Vector3 ((width - 1) / 2, 25f, -8f);
		Camera.main.GetComponent<CameraController> ().SetPosition (pos);
	}
	void BuildGrid(int x, int y){
		GameObject tile = (GameObject)Resources.Load ("Prefabs/Tile");
		grid = new GameObject[x, y];
		for (int i = 0; i < x; i++) {
			for (int j = 0; j < y; j++) {
				grid [i, j] = (GameObject)Instantiate (tile, new Vector3 (i, 0, j), Quaternion.identity);
				grid [i, j].transform.SetParent (transform);
				grid [i, j].GetComponent<TileController> ().pos = new Position (i, j);

			}
		}
	}
}
