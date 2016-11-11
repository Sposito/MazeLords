using UnityEngine;
using System.Collections;

public class TileController : MonoBehaviour {


	LevelEditorController levelEditorController;
	[SerializeField]
	public Position pos;

	Color baseColor;
	public Color lightColor;
	Renderer rend;
	void Start () {
		rend = transform.GetChild(0).GetComponent<Renderer> ();
		baseColor = rend.material.GetColor ("_Color");
		levelEditorController = GameObject.FindWithTag ("LevelEditorController").GetComponent<LevelEditorController>();


	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void OnMouseDown(){

			levelEditorController.TileClick (pos);

	}

	void OnMouseUp(){
		levelEditorController.ClearLine ();
		levelEditorController.AddLine (pos);
	}

	void OnMouseEnter(){
		StartCoroutine (LightUp());
		levelEditorController.SetCurrentTile (pos);

	}
	void OnMouseExit(){
		
		StartCoroutine (LightDown());

		Invoke ("ForceLightDown", .5f);

	}
	LineRenderer line = new LineRenderer();
	void OnMouseDrag(){
		levelEditorController.DrawLine (pos);	
	}

	IEnumerator LightUp(){
		while (rend.material.color != lightColor) {

			rend.material.color = Vector4.MoveTowards (rend.material.color, lightColor, 0.5f);
			yield return new WaitForEndOfFrame ();
		}
	}
	IEnumerator LightDown(){
		while (rend.material.color != baseColor) {

			rend.material.color = Vector4.MoveTowards (rend.material.color, baseColor, 0.5f);

			yield return new WaitForEndOfFrame ();
		}
	}

	void ForceLightDown(){
		rend.material.color = baseColor;
	}


}
