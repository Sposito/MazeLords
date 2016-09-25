using UnityEngine;
using System.Collections;

public class PlayerController : MonoBehaviour {
	float h = 0;
	float v = 0;
	Vector3 move;
	CharacterController controller;
	public float speed = 10f;
	Sprite sprite;
	Vector3 rightScale;
	Vector3 leftScale;
	float verticaScale = 0f;
	float baseYScale;
	public AnimationCurve curve;
	void Start () {
		controller = GetComponent<CharacterController> ();
		sprite = GetComponent<Sprite> ();
		rightScale = transform.localScale;
		leftScale = new Vector3 (-rightScale.x, rightScale.y, rightScale.z);
		baseYScale = transform.localScale.y;
	}
	
	// Update is called once per frame
	void Update () {
		h = Input.GetAxis("Horizontal") * speed;
		v = Input.GetAxis("Vertical")   * speed;

		if (h > 0) {
			rightScale = new Vector3 (rightScale.x, baseYScale * verticaScale, rightScale.z);
			transform.localScale = rightScale;
		}
		else if(h<0){
			leftScale = new Vector3 (leftScale.x, baseYScale * verticaScale, leftScale.z);
			transform.localScale = leftScale;
		}
		move = new Vector3 (h, 0f, v);
		controller.SimpleMove (move);
		verticaScale = curve.Evaluate (Time.time) - ((1 - curve.Evaluate (Time.time)) * 2*controller.velocity.magnitude);

		transform.localScale = new Vector3 (transform.localScale.x, baseYScale * verticaScale, transform.localScale.z);

	
	}
}
