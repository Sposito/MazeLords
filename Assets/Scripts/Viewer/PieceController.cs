using UnityEngine;
using System.Collections;
public enum PieceKind{Common, OnlyMovable}

public class PieceController : MonoBehaviour {
	public Position position;

	public PieceKind pieceKind;
	public Pieces id;
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void OnMouseOver(){
		
		if(Input.GetMouseButton(1)){
			LevelEditorController.Singleton.PieceClick (position, pieceKind);
		}
	}
}
