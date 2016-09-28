using UnityEngine;
using System.Collections;

public class AutitudeControl : MonoBehaviour {

	public Transform playerTransform;
	float standartDistance;
	float distance;
	Vector3 planarPos;
	public float factor = 1f;
	float stdY;
	void Start () {
		standartDistance = (transform.position - playerTransform.position).sqrMagnitude;
		distance = standartDistance;
		stdY = transform.position.y;
	}
	
	// Update is called once per frame
	void Update () {
		Vector3 altitudeComp = Vector3.up * (playerTransform.position.y - transform.position.y);
		distance = (transform.position - playerTransform.position - altitudeComp).sqrMagnitude;
		if (distance <= standartDistance) {
			transform.position = new Vector3(transform.position.x, stdY + (standartDistance - distance) * factor, transform.position.z);
		}
	}
}
