using UnityEngine;
using System.Collections;

public class GizmoController : MonoBehaviour {
	public GameObject gizmdo;
	public float rotationSpeed = 1f;
	public float translateSpeed = 1f;
	Vector3 movement;
	// Use this for initialization
	void Start () {
		movement = Vector3.zero;
	}
	
	// Update is called once per frame
	void Update () {

		gizmdo.transform.Rotate(Vector3.up * rotationSpeed);
		movement = translateSpeed/100 * Vector3.up * Mathf.Sin (gizmdo.transform.rotation.eulerAngles.y * Mathf.Deg2Rad);
		gizmdo.transform.Translate (movement);
	}
}
