using UnityEngine;
using System.Collections;

public class LevelManager : MonoBehaviour {

	public float autoLoadNextLevelAfter;

	void Start () {
		if (autoLoadNextLevelAfter == 0) {
			Debug.Log ("Level auto load disabled");
		} else {
			Invoke ("LoadNextLevel", autoLoadNextLevelAfter);
		}
	}

	public void LoadLevel(string name){
		Debug.Log ("New Level load: " + name);
		Application.LoadLevel (name);
	}

	public void QuitRequest(){
		Debug.Log ("Quit requested");
		Application.Quit ();
	}
	
	public void LoadNextLevel() {
		Application.LoadLevel(Application.loadedLevel + 1);
	}

	public void ReturnToMainMenu() {
		//GameObject.Find("remy").GetComponent<HealthManager>().Heal(99999);
		Destroy(GameObject.Find("Player"));
		Destroy(GameObject.Find("HUD"));
		Time.timeScale = 1f;
		Application.LoadLevel(1);
		Destroy(GameObject.Find("DeathHUD"));
	}

	public void Resume() {
		Destroy(GameObject.Find("PauseHUD"));
		Cursor.visible = false;
		Cursor.lockState = CursorLockMode.Locked;
		Time.timeScale = 1f;
	}

}
