using UnityEngine;
using System.Collections;
using UnityEngine.UI;
public class TranparencyOcilation : MonoBehaviour {
	public AnimationCurve curve;
	Image image;
	float a = 1;
	void Start () {
		image = GetComponent<Image> ();
	}
	
	// Update is called once per frame
	void Update () {
		a = curve.Evaluate (Time.time);
		image.color = new Color (image.color.r, image.color.g, image.color.b, a);
	}
}
