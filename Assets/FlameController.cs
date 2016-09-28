using UnityEngine;
using System.Collections;

public class FlameController : MonoBehaviour {

	Transform tailTransform; 
	public float intensity = 1f;
	float baseHead;
	float baseTail;
	void Start () {
		tailTransform = transform.GetChild (0);	
		baseHead = transform.localScale.x;
		baseTail = tailTransform.localScale.x;
	}
	// Update is called once per frame
	void Update () {
		transform.localScale = baseHead * Vector3.one * intensity;
		tailTransform.localScale = baseTail * Vector3.one * intensity;

	}
}
