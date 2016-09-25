using UnityEngine;
using System.Collections;

public class CameraPlayController : MonoBehaviour {

	public GameObject target;
	public Vector3 offset;
	public float response = .5f;
	Vector3 targetPos;
	void Start () {
		offset = transform.position - target.transform.position  ;
	}
	
	// Update is called once per frame
	void Update () {
		targetPos = target.transform.position + offset;
		transform.position = Vector3.Lerp (transform.position, targetPos, response);
	}
}
