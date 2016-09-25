using UnityEngine;
using System.Collections;

public class CameraController : MonoBehaviour {
	[SerializeField]
	private  float minFOV = 20f;
	[SerializeField]
	private  float maxFOV = 60f;

	public float currentFOV = 50;
	Vector3 midBtnClickPos;

	void Start () {
		currentFOV = Camera.main.fieldOfView;
	}
	
	// Update is called once per frame
	void Update () {
		if (Input.mouseScrollDelta != Vector2.zero) {
			currentFOV = Mathf.Clamp (currentFOV + Input.mouseScrollDelta.y, minFOV, maxFOV);
			Camera.main.fieldOfView = currentFOV;
			print (Input.mouseScrollDelta);
		}
		if (Input.GetMouseButtonDown (2)) {
			midBtnClickPos = Input.mousePosition;
		}
		if (Input.GetMouseButton(2)){
			print ("botao do meio apertado");
			Vector3 delta = Input.mousePosition - midBtnClickPos;
			Camera.main.transform.position += new Vector3 (delta.x,0f,delta.y) /100f;
		}

	}

	public void SetPosition(Vector3 pos){
		transform.position = pos;
	}


}
