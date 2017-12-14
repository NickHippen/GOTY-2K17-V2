using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Invincibility : MonoBehaviour {
	void Update() {
		if (Input.GetKeyDown (KeyCode.I)) {
			GameObject.Find ("remy").GetComponent<HealthManager> ().invincible ^= true;
			Debug.Log ("invincible");
		}
	}
}
