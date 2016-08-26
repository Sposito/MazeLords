using UnityEngine;
using System.Collections;

public class AddBlock : MonoBehaviour {

	bool clicked;
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void OnMouseDown(){
		if (!clicked) {
			GameObject go = GameObject.CreatePrimitive (PrimitiveType.Cube);
			go.transform.position = transform.position;
			go.transform.Translate (Vector3.up/2);
			go.transform.SetParent (transform);
			go.GetComponent<Renderer> ().material.color = Color.red;
		}
		clicked = true;


	}
}
