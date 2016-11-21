using UnityEngine;
using System.Collections;
using System.IO;
using System;
using System.Text;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
public enum Pieces{Base = 1, StartPoint = 301, EndPoint = 302, Chest=401}

public class LevelEditorController : MonoBehaviour {
	[SerializeField]
	int width = 15;
	[SerializeField]
	int height = 15;

	static bool comingFromValidate = false;
	void Awake(){
		//DontDestroyOnLoad (transform.gameObject);
	}
	public static LevelEditorController Singleton;

	GameObject[,] grid;
	GridMap gridMap;


	GameObject baseCube ;
	GameObject startPoint;
	GameObject endPoint;
	GameObject chest;

	GameObject currentGameObject;
	Pieces selectedPiece = Pieces.Base;

	GameObject lineGui;
	LineRenderer line; 
	bool MouseOverUI = false;
	Position currentPosition = new Position(0,0);
	void Start () {
		if (Singleton == null) {
			Singleton = this;
		}
		LoadBasicBlocks ();
		BuildGrid (width, height);
		SetupCamera ();
		BuildLine ();
		if (comingFromValidate) {
			
			LoadAsync ();
		}


	}

	void LoadBasicBlocks(){
		baseCube = (GameObject)Resources.Load ("Prefabs/BaseWall");
		startPoint = (GameObject)Resources.Load ("Prefabs/StartPoint");
		endPoint = (GameObject)Resources.Load ("Prefabs/EndPoint");
		chest = (GameObject)Resources.Load ("Prefabs/BasicChest");
		currentGameObject = baseCube;
	}

	public void BuildLine(){
		lineGui = new GameObject ();
		line = lineGui.AddComponent<LineRenderer> ();
		line.SetPosition (0, Vector3.zero);
		line.SetPosition (1, Vector3.zero);
		line.material = (Material)Resources.Load ("Line");
	}

	// Update is called once per frame
	void Update () {
	
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
	public ChestTile GetChest(){
		return gridMap.chest;
	}
	public void TileClick(Position pos){
		if (MouseOverUI) {//Ignores click if the mouse is over UI
			return;
		}
		//Quaternion rot = Quaternion.Euler (0f, Random.Range (0, 4) * 90f, 0f);
		if (gridMap.AddTile (pos, new GridTile((int)selectedPiece) )) {
			grid[pos.x, pos.y] = (GameObject)Instantiate (currentGameObject, new Vector3 (pos.x, 0.5f, pos.y), Quaternion.identity);
			grid[pos.x, pos.y] .GetComponent<PieceController> ().position = pos;
			grid[pos.x, pos.y] .transform.SetParent (transform);
		}
	}

	public void PieceClick(Position pos, PieceKind kind){
		if (MouseOverUI) {//Ignores click if the mouse is over UI
			return;
		}
		switch (kind){
		case PieceKind.Common:
			if (gridMap.RemoveTile (pos) != null) {
				print ("entrou");
				Destroy (grid [pos.x, pos.y]);
			}
			break;
		case PieceKind.OnlyMovable:
			break;
		default:
			break;
		}

	}

	public void SetMouseOverUI(bool b){
		MouseOverUI = b;
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
		GameObject gridGO = new GameObject();

		for (int i = 0; i < x; i++) {
			for (int j = 0; j < y; j++) {
				grid [i, j] = (GameObject)Instantiate (tile, new Vector3 (i, 0, j), Quaternion.identity);
				grid [i, j].transform.SetParent (gridGO.transform);
				grid [i, j].GetComponent<TileController> ().pos = new Position (i, j);

			}
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

	public void LoadAsync(){
		StartCoroutine (LoadMaze ());
	}

	public IEnumerator LoadMaze(){
		string save ="";
		//#if UNITY_EDITOR  || UNITY_STANDALONE
		StreamReader reader = new StreamReader (Application.dataPath + "/save.json");
		save = reader.ReadToEnd();
		reader.Close ();
		//#endif

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
				SelectPiece (tile.Id);
				grid[tile.X, tile.Y] = (GameObject)Instantiate (currentGameObject, new Vector3 (pos.x, 0.5f, pos.y), Quaternion.identity);
				grid [tile.X, tile.Y].GetComponent<PieceController> ().position = pos;
				grid[tile.X, tile.Y].transform.SetParent (transform);
				gridMap.SetEmpty (pos, false);
			}
		}
		yield return new WaitForEndOfFrame ();
	}
	public void ValidateMap(){
		SaveMaze ();
		SceneManager.LoadScene (2, LoadSceneMode.Single);
		comingFromValidate = true;	

	}

	public void SetCurrentTile(Position pos){
		currentPosition = pos;	
	}

	public void DrawLine(Position start){
		Vector3 startOffset = Vector3.zero;
		Vector3 endOffset = Vector3.zero;

		if (start.x == currentPosition.x) {
			if (start.y > currentPosition.y) {
				startOffset = Vector3.forward / 2f;
				endOffset = Vector3.back / 2f;
			} 
			else if (start.y < currentPosition.y) {
				startOffset = Vector3.back / 2f;
				endOffset = Vector3.forward / 2f;
			}

			line.SetPosition (0, start.AsVec3 + startOffset);
			line.SetPosition (1, currentPosition.AsVec3 + endOffset);
			return;

		} 
		else if (start.y == currentPosition.y) {

			if (start.x > currentPosition.x) {
				startOffset = Vector3.right / 2f;
				endOffset = Vector3.left / 2f;
			} 
			else if (start.x < currentPosition.x) {
				startOffset = Vector3.left / 2f;
				endOffset = Vector3.right / 2f;
			}
			line.SetPosition (0, start.AsVec3 + startOffset);
			line.SetPosition (1, currentPosition.AsVec3 + endOffset);
			return;

		}

	
		ClearLine ();

	}

	public void ClearLine(){
		line.SetPosition (0, Vector3.zero);
		line.SetPosition (1, Vector3.zero);
	}

	public void AddLine(Position startPos){
		if (startPos == currentPosition) {
			return;
		}

		if (startPos.y == currentPosition.y) {
			if (startPos.x < currentPosition.x) {
				for (int i = startPos.x + 1; i <= currentPosition.x; i++) {
					TileClick (new Position (i, startPos.y));
				}
			}
			else {
				for (int i = startPos.x - 1; i >= currentPosition.x; i--) {
					TileClick (new Position (i, startPos.y));
				}
			}
		}

		else if (startPos.x == currentPosition.x) {
			if (startPos.y < currentPosition.y ){
				for (int i = startPos.y + 1; i <= currentPosition.y; i++) {
					TileClick (new Position (startPos.x, i));
				}
			}
			else {
				for (int i = startPos.y - 1; i >= currentPosition.y; i--) {
					TileClick (new Position (startPos.x, i));
				}
			}

		}
	}

}
