using UnityEngine;
using System.Collections;
using System.IO;
using System;
using System.Text;
using System.Collections.Generic;
using UnityEngine.SceneManagement;

public class GameController : MonoBehaviour {

	GridMap gridMap;

	public Material floorMaterial;

	GameObject baseCube ;
	GameObject startPoint;
	GameObject endPoint;
	GameObject chest;

	GameObject currentGameObject;
	Pieces selectedPiece = Pieces.Base;
	public GameObject floor;
	void Start () {
		LoadMaze ();

		GameObject floorGo = (GameObject)Instantiate (floor, Vector3.zero, Quaternion.identity);
		float width = (gridMap.Size.x / 10 * 2);
		float height = (gridMap.Size.y / 10 *2);
		float xPos = gridMap.Size.x / 2;
		float zPos = gridMap.Size.y / 2;

		floorGo.transform.position = new Vector3 (xPos, 0f, zPos);
		floorGo.transform.localScale = new Vector3 (width, 1f, height);
		floorGo.GetComponent<Renderer> ().material = floorMaterial;

	
		currentGameObject = baseCube;



	}
	void Update(){
		if (Input.GetKeyDown(KeyCode.Escape)){
			SceneManager.LoadScene (1, LoadSceneMode.Single);
		}
	}
	public void SelectPiece(int pieceID){
		selectedPiece = (Pieces)pieceID;
		ChangeCurrentGO(selectedPiece);
	}

	void ChangeCurrentGO(Pieces i){
		switch (i) {
		case Pieces.Base:
			currentGameObject = baseCube;
			break;
		case Pieces.StartPoint:
			currentGameObject = startPoint;
			break;
		case Pieces.EndPoint:
			currentGameObject = endPoint;
			break;
		case Pieces.Chest:
			currentGameObject = chest;
			break;
		default:
			break;
		}

	}

	public void LoadMaze(){
		print ("works");
		string save ="";
		#if UNITY_EDITOR  || UNITY_STANDALONE
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
		baseCube = (GameObject)Resources.Load ("Prefabs/BaseWall");
		startPoint = (GameObject)Resources.Load ("Prefabs/StartPoint");
		endPoint = (GameObject)Resources.Load ("Prefabs/EndPoint");
		chest = (GameObject)Resources.Load ("Prefabs/BasicChest");

		foreach (GridTile tile in gridMap.Grid) {
//			if (!tile.IsEmpty) {
//				Position pos = new Position (tile.X, tile.Y);
//				GameObject go = (GameObject)Instantiate (baseCube, new Vector3 (pos.x, 0.5f, pos.y), Quaternion.identity);
//				go.transform.SetParent (transform);
//				go.GetComponent<Collider> ().isTrigger = false;
//				Destroy (go.GetComponent<PieceController> ());
//			}
			if (!tile.IsEmpty) {
				Position pos = new Position (tile.X, tile.Y);
				SelectPiece (tile.Id);
				GameObject go = (GameObject)Instantiate (currentGameObject, new Vector3 (pos.x, 0.5f, pos.y), Quaternion.identity);
				go.transform.SetParent (transform);
				go.GetComponent<Collider> ().isTrigger = false;
				Destroy (go.GetComponent<PieceController> ());
			}
		}

	}
}
