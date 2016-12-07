using UnityEngine;
using System.Collections;

public class CameraPlayController : MonoBehaviour {

	public GameObject target;
	public Vector3 offset;
	public float response = .5f;
	Vector3 targetPos;

	public bool goAway = false;
	public float goAwaySpeed = 0.1f;
	void Start () {
		offset = transform.position - target.transform.position  ;
		transform.Translate (Vector3.forward * -100f, Space.Self);
	}
	
	// Update is called once per frame
	void Update () {
		if (!goAway) {
			targetPos = target.transform.position + offset;
			transform.position = Vector3.Lerp (transform.position, targetPos, response);
		}
		else {
			transform.Translate (Vector3.forward * -goAwaySpeed, Space.Self);
		}
	}
}
