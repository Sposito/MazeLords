using UnityEngine;
using System.Collections;
using System.IO;
using System;
using System.Text;
using System.Collections.Generic;

public class LevelEditorController : MonoBehaviour {
	[SerializeField]
	int width = 15;
	[SerializeField]
	int height = 15;

	public static LevelEditorController Singleton;

	GameObject[,] grid;
	GridMap gridMap;

	GameObject baseCube ;

	void Start () {
		if (Singleton == null) {
			Singleton = this;
		}
		baseCube = (GameObject)Resources.Load ("Prefabs/BaseWall");
		BuildGrid (width, height);
		SetupCamera ();

	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public void TileClick(Position pos){
		//Quaternion rot = Quaternion.Euler (0f, Random.Range (0, 4) * 90f, 0f);
		if (gridMap.AddTile (pos, GridTile.BasicBlock)) {
			grid[pos.x, pos.y] = (GameObject)Instantiate (baseCube, new Vector3 (pos.x, 0.5f, pos.y), Quaternion.identity);
			grid[pos.x, pos.y] .GetComponent<PieceController> ().position = pos;
			grid[pos.x, pos.y] .transform.SetParent (transform);
		}
	}

	public void PieceClick(Position pos){
		
		if (gridMap.RemoveTile (pos) != null) {
			print ("entrou");
			Destroy( grid[pos.x, pos.y]); 
		}
	}

	void SetupCamera(){
		Vector3 pos = new Vector3 ((width - 1) / 2, 25f, -8f);
		Camera.main.GetComponent<CameraController> ().SetPosition (pos);
	}
	void BuildGrid(int x, int y){
		
		GameObject tile = (GameObject)Resources.Load ("Prefabs/Tile");
		grid = new GameObject[x, y];
		gridMap = new GridMap (x, y);
		gridMap.SetBaseGridMap ();
		JsonUtility.ToJson (gridMap); //it forces intialization of all positions

		for (int i = 0; i < x; i++) {
			for (int j = 0; j < y; j++) {
				grid [i, j] = (GameObject)Instantiate (tile, new Vector3 (i, 0, j), Quaternion.identity);
				grid [i, j].transform.SetParent (transform);
				grid [i, j].GetComponent<TileController> ().pos = new Position (i, j);

			}
		}

	}

	public void SaveMaze(){
		string save = JsonUtility.ToJson (gridMap);
		#if UNITY_EDITOR
		StreamWriter writer = new StreamWriter (Application.dataPath + "/save.json");
		writer.Write (save);
		writer.Close ();
		print (Application.dataPath + "/save.json");
		#endif

		#if UNITY_WEBGL && !UNITY_EDITOR
		Dictionary<string,string> headers = new Dictionary<string, string>();
		headers.Add("Content-Type", "application/json");

		byte[] pData = Encoding.UTF8.GetBytes(save.ToCharArray());

		WWW www = new WWW ("52.67.94.184:3000/api", pData, headers);
		#endif
	}

	public void LoadAsync(){
		StartCoroutine (LoadMaze ());
	}

	public IEnumerator LoadMaze(){
		string save ="";
		#if UNITY_EDITOR
		StreamReader reader = new StreamReader (Application.dataPath + "/save.json");
		save = reader.ReadToEnd();
		reader.Close ();
		#endif

		#if UNITY_WEBGL && !UNITY_EDITOR
		print("webGL"); 
		WWW www = new WWW ("52.67.94.184:3000/api/get");
		yield return www;
		save = www.text;
		#endif



		gridMap = JsonUtility.FromJson<GridMap> (save);

		GameObject[] goToDestroy = GameObject.FindGameObjectsWithTag ("Tile");
		foreach (GameObject go in goToDestroy) {
			GameObject.DestroyImmediate (go);
		}

		foreach (GridTile tile in gridMap.Grid) {
			if (!tile.IsEmpty) {
				Position pos = new Position (tile.X, tile.Y);
				grid[tile.X, tile.Y] = (GameObject)Instantiate (baseCube, new Vector3 (pos.x, 0.5f, pos.y), Quaternion.identity);
				grid [tile.X, tile.Y].GetComponent<PieceController> ().position = pos;
				grid[tile.X, tile.Y].transform.SetParent (transform);
				gridMap.SetEmpty (pos, false);
			}
		}
		yield return new WaitForEndOfFrame ();
	}
}
