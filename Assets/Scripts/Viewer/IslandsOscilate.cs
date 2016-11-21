using UnityEngine;
using System.Collections;

public class IslandsOscilate : MonoBehaviour {

	public AnimationCurve islandAnimation;
	public float speed = 1f;
	public float amount = 1f;

	void Start () {
		speed += Random.Range (-20f, 20f);

	}
	
	// Update is called once per frame
	void Update () {
		transform.Translate( (amount * Vector3.up * islandAnimation.Evaluate(speed * Time.time )));
	}
}
