using UnityEngine;
using System.Collections;

public class PieceController : MonoBehaviour {
	public Position position;
	public enum PieceKind{Common, NonRemovable}
	public PieceKind pieceKind;
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void OnMouseOver(){
		
		if(Input.GetMouseButton(1)){
			LevelEditorController.Singleton.PieceClick (position);
		}
	}
}
