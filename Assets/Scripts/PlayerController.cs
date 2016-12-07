using UnityEngine;
using System.Collections;
using UnityEngine.UI;

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
	public RectTransform lifeBarTransform;

	public Texture frontTexture;
	public Texture backTexture;
	public Texture frontNormalTexture;
	public Texture backNormalTexture;

	public Material playerQuadMaterial;


	public ParticleSystem headParticleEmitter;
	public ParticleSystem tailPArticleEmitter;
	public Light gizmoLight;
	float starLightIntensity;
	public AnimationCurve falloutCurve;

	Player player;
	public float energy;
	float maxEnergy;
	Vector3 gizmoOrignalScale;
	public GameObject gameOverMenu;
	public float totalEnergy = 60f;



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
		player.SetEnergy (totalEnergy);
		energy = player.Energy;
		maxEnergy = energy; 
		headParticleEmitter.startLifetime = player.Energy;
		starLightIntensity = gizmoLight.intensity;

	}
	
	// Update is called once per frame

	public void GameOver(){
		StartCoroutine (GameOverCoroutine ());
	}
	IEnumerator GameOverCoroutine(){
		Camera.main.gameObject.GetComponent<CameraPlayController> ().goAway = true;
		yield return new WaitForSeconds(5f);
		gameOverMenu.SetActive (true);
	}
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

		if (v > 0) {
			playerQuadMaterial.SetTexture ("_MainTex", backTexture);
			playerQuadMaterial.SetTexture ("_BumpMap", backNormalTexture);
		} 
		else if (v < 0) {
			playerQuadMaterial.SetTexture ("_MainTex", frontTexture);
			playerQuadMaterial.SetTexture ("_BumpMap", frontNormalTexture);
		}

		move = new Vector3 (h, 0f, v);
		controller.SimpleMove (move);

		speedMag = controller.velocity.magnitude;
		verticaScale = curve.Evaluate (Time.time) - ((1 - curve.Evaluate (Time.time)) * 2*speedMag);

		transform.localScale = new Vector3 (transform.localScale.x, baseYScale * verticaScale, transform.localScale.z);
		audioSource.volume = Mathf.Lerp( speedMag/2, audioSource.volume, audioReponse);
		//audioSource.

		lifeBarTransform.SetSizeWithCurrentAnchors (RectTransform.Axis.Vertical, 800 * energy / maxEnergy);

		if (energy <= 0) {
			print ("Game Over");
			GameOver ();
			tailPArticleEmitter.startSize = 0;
		}
		else {
			headParticleEmitter.startSize *=  falloutCurve.Evaluate (energy / player.Energy);
			gizmoLight.intensity = starLightIntensity * energy / player.Energy;
			energy -= (float)Time.deltaTime;

		}
	
	}


}
