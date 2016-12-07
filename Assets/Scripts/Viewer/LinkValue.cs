using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class LinkValue : MonoBehaviour {
	public Text label;
	public int target;
	public Pieces piece;
	public int max = 80;

	LevelEditorController levelEditor;
	// Use this for initialization
	void Start () {
		levelEditor = FindObjectOfType <LevelEditorController> ();
	}
	
	// Update is called once per frame
	void Update () {
		label.text = "" + Mathf.Max(0, max - levelEditor.CountPieces (piece));

	}
}
