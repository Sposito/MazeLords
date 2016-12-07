using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EndGameBehaviour : MonoBehaviour {

	Collider col;
	void Start () {
		col = GetComponent<Collider> ();
		col.isTrigger = true;
	}

	void OnTriggerEnter(){
		GameController.UnlockFlag ();
	}
	// Update is called once per frame
	void Update () {
		
	}
}
