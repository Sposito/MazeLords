using UnityEngine;
using System.Collections;
using UnityEngine.UI;
public class LinkValue : MonoBehaviour {
	public Text label;
	public int target;

	LevelEditorController levelEditor;
	// Use this for initialization
	void Start () {
		levelEditor = FindObjectOfType <LevelEditorController> ();
	}
	
	// Update is called once per frame
	void Update () {
		label.text = levelEditor.GetChest ().GetAmount (target) + "";

	}
}
