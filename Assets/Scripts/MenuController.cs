using UnityEngine;
using System.Collections;
//using Facebook.Unity;
using UnityEngine.SceneManagement;
public class MenuController : MonoBehaviour {

	// Use this for initialization
	void Start () {
		//FB.Init ();
	}
	
	// Update is called once per frame
	void Update () {
	
	}



	public void LoadLevelEditor(){
		SceneManager.LoadScene (1, LoadSceneMode.Single);
	}
//	public void FBLogin() {
//		//FB.LogInWithReadPermissions ();
//
//	}

//	private void FBLoginCallback(FBResult result) {
//		if(FB.IsLoggedIn) {
//			showLoggedIn();
//			StartCoroutine("ParseLogin");
//		} else {
//			Debug.Log ("FBLoginCallback: User canceled login");
//		}
//	}
}
