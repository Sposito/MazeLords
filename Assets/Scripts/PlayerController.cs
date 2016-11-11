﻿using UnityEngine;
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
	AudioSource audioSource;

	public ParticleSystem headParticleEmitter;
	public ParticleSystem tailPArticleEmitter;
	public Light gizmoLight;
	float starLightIntensity;
	public AnimationCurve falloutCurve;

	Player player;
	float energy;
	Vector3 gizmoOrignalScale;


	public float audioReponse = 0.95f;
	float speedMag = 0f;
	void Start () {
		controller = GetComponent<CharacterController> ();
		sprite = GetComponent<Sprite> ();
		rightScale = transform.localScale;
		leftScale = new Vector3 (-rightScale.x, rightScale.y, rightScale.z);
		baseYScale = transform.localScale.y;
		audioSource = GetComponent<AudioSource> ();
		player = Player.LoadCurrentPlayer ();
		energy = player.Energy;
		headParticleEmitter.startLifetime = player.Energy;
		starLightIntensity = gizmoLight.intensity;

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

		speedMag = controller.velocity.magnitude;
		verticaScale = curve.Evaluate (Time.time) - ((1 - curve.Evaluate (Time.time)) * 2*speedMag);

		transform.localScale = new Vector3 (transform.localScale.x, baseYScale * verticaScale, transform.localScale.z);
		audioSource.volume = Mathf.Lerp( speedMag/2, audioSource.volume, audioReponse);
		//audioSource.



		if (energy <= 0) {
			print ("Game Over");
			tailPArticleEmitter.startSize = 0;
		}
		else {
			headParticleEmitter.startSize *=  falloutCurve.Evaluate (energy / player.Energy);
			gizmoLight.intensity = starLightIntensity * energy / player.Energy;
			energy -= (float)Time.deltaTime;

		}
	
	}


}
