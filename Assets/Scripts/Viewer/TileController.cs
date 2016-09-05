using UnityEngine;
using System.Collections;

public class TileController : MonoBehaviour {

	bool clicked;
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
		if (!clicked) {
			levelEditorController.TileClick (pos);
		}
		clicked = true;

	}

	void OnMouseEnter(){
		StartCoroutine (LightUp());

	}
	void OnMouseExit(){
		StartCoroutine (LightDown());
		Invoke ("ForceLightDown", .5f);

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
