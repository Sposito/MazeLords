using UnityEngine;
using System.Collections;
using System.IO;
using System;
using System.Text;
using System.Collections.Generic;

public class GameController : MonoBehaviour {

	GridMap gridMap;
	public GameObject baseCube;
	public Material floorMaterial;
	void Start () {
		LoadMaze ();

		GameObject floor = GameObject.CreatePrimitive (PrimitiveType.Plane);
		float width = (gridMap.Size.x / 10 * 2);
		float height = (gridMap.Size.y / 10 *2);
		float xPos = gridMap.Size.x / 2;
		float zPos = gridMap.Size.y / 2;

		floor.transform.position = new Vector3 (xPos, 0f, zPos);
		floor.transform.localScale = new Vector3 (width, 1f, height);
		floor.GetComponent<Renderer> ().material = floorMaterial;

	}

	public void LoadMaze(){
		print ("works");
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
				GameObject go = (GameObject)Instantiate (baseCube, new Vector3 (pos.x, 0.5f, pos.y), Quaternion.identity);
				go.transform.SetParent (transform);
				go.GetComponent<Collider> ().isTrigger = false;
				Destroy (go.GetComponent<PieceController> ());
			}
		}

	}
}
