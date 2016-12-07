using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChestBehaviour : MonoBehaviour {

	SphereCollider trigger;
	public Material litMat;
	public float OpenTime = 5f;
	float timer = 0f;
	public Color lockedColor = Color.red;
	public Color unlockedColor = Color.green;
	Animator anim;
	bool isOpening = false;
	bool isOpen = false;
	void Start () {
		trigger = GetComponent<SphereCollider> ();
		litMat.SetColor ("_EmissionColor", lockedColor);
		anim = GetComponent<Animator> ();
			
	}
	void OnTriggerEnter(){
		if (!isOpen) {
			isOpening = true;
		}
	}

	void OnTriggerExit(){
		if (!isOpen) {
			timer = 0;
			isOpening = false;
			litMat.SetColor ("_EmissionColor", lockedColor);
		}
	}


	// Update is called once per frame
	void Update () {
		if (isOpening) {
			timer += Time.deltaTime;
			if (timer < OpenTime / 2) {
				litMat.SetColor ("_EmissionColor",Color.Lerp(lockedColor, Color.white , timer/OpenTime * 2));
			}
			else{
				litMat.SetColor ("_EmissionColor",Color.Lerp(Color.white ,unlockedColor, ((timer/OpenTime) -0.5f) * 2  ) );		
			}
				
		}

		if (timer > OpenTime) {
			isOpen = true;
			anim.SetBool ("isOpen", true);
			GameController.UnlockChest ();
		}
	}
}
