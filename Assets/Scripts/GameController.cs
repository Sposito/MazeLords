using UnityEngine;
using System.Collections;
using System.IO;
using System;
using System.Text;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameController : MonoBehaviour {

	GridMap gridMap;
	public Image chestCheck;
	public Image flagCheck;
	public Color checkedColor = Color.green;

	public Material floorMaterial;
	public bool mockControl = false;
	public bool testMaze = false;
	GameObject baseCube ;
	GameObject startPoint;
	GameObject endPoint;
	GameObject chest;

	GameObject player;

	GameObject currentGameObject;
	Pieces selectedPiece = Pieces.Base;

	public GameObject sucessWindow;

	Vector3 startPos;

	private static bool chestCleared = false;
	private static bool flagCleared  = false;


	public GameObject floor;
	public static void UnlockChest(){
		chestCleared = true;


	}
	public void ReturnToMainMenu(){
		SceneManager.LoadScene (4, LoadSceneMode.Single);
	}
	public static void UnlockFlag(){
		flagCleared = true;
	}


	void Start () {
		chestCleared = false;
		flagCleared = false;
		player = GameObject.FindGameObjectWithTag ("Player");
		if (!testMaze) {
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
		player.transform.position = new Vector3 (startPos.x, player.transform.position.y, startPos.z);
		}
	}
	void Update(){
		if (Input.GetKeyDown(KeyCode.Escape) && !mockControl){
			SceneManager.LoadScene (1, LoadSceneMode.Single);
		}
		else if (Input.GetKeyDown(KeyCode.Escape) && mockControl){
			SceneManager.LoadScene (4, LoadSceneMode.Single);
		}

		if (chestCleared) {
			chestCheck.color = checkedColor;
		}
		if (flagCleared) {
			flagCheck.color = checkedColor;

			if (chestCleared) {
				if (!testMaze){
					gridMap.Validated = true;
					SaveMaze ();
				}
				sucessWindow.SetActive (true);
				//Time.timeScale = 0f;
			}
		}
	}

	public void LoadLevelEditor(){
		SceneManager.LoadScene (1, LoadSceneMode.Single);
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
	public void SaveMaze(){
		string save = JsonUtility.ToJson (gridMap);
		//#if UNITY_EDITOR || UNITY_STANDALONE
		StreamWriter writer = new StreamWriter (Application.dataPath + "/save.json");
		writer.Write (save);
		writer.Close ();
		print (Application.dataPath + "/save.json");
		//#endif

		#if UNITY_WEBGL && !UNITY_EDITOR
		Dictionary<string,string> headers = new Dictionary<string, string>();
		headers.Add("Content-Type", "application/json");

		byte[] pData = Encoding.UTF8.GetBytes(save.ToCharArray());

		WWW www = new WWW ("52.67.94.184:3000/api", pData, headers);
		#endif
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
		chest = (GameObject)Resources.Load ("Prefabs/Chest/BasicChest");

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
//				if (tile.Id == 301) {
//					Destroy (go.GetComponent<Collider> ());
//				}

				if (tile.Id == (int)Pieces.StartPoint) {
					startPos = go.transform.position;
				}
				Destroy (go.GetComponent<PieceController> ());
			}
		}



	}
}
