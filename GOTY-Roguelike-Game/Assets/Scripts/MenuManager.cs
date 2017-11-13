using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuManager : MonoBehaviour {
	Camera  mycam;

	public GameObject primaryCanvas;
	public GameObject optionsCanvas;
	public GameObject playerSelectCanvas;

	public float speed;

	bool transformForward = false;
	bool transformOptions = false;
	bool transformPlayerSelect = false;
	bool playerSelected = false;

	// Use this for initialization
	void Start () {
		mycam = GameObject.Find ("Main Camera").GetComponent<Camera> ();
	}
	
	// Update is called once per frame
	void Update () {
		float step = speed * Time.deltaTime;
		if (transformForward) {
			mycam.transform.position = Vector3.MoveTowards (mycam.transform.position, new Vector3 (-8, 2, 5), step);
			mycam.transform.rotation = Quaternion.RotateTowards (mycam.transform.rotation, Quaternion.Euler(0,0,0), step*20);

			optionsCanvas.GetComponent<CanvasGroup> ().alpha -= step / 4;
			playerSelectCanvas.GetComponent<CanvasGroup> ().alpha -= step / 4;

			if (mycam.transform.position == new Vector3 (-8, 2, 5)) {
				transformForward = false;
				primaryCanvas.SetActive (true);
				optionsCanvas.SetActive (false);
				playerSelectCanvas.SetActive (false);
				primaryCanvas.GetComponent<CanvasGroup> ().alpha = 1;
			}
		}
		if (transformOptions) {
			mycam.transform.position = Vector3.MoveTowards (mycam.transform.position, new Vector3 (-13, 2, 3), step);
			mycam.transform.rotation = Quaternion.RotateTowards (mycam.transform.rotation, Quaternion.Euler(0,-90,0), step*20);
			primaryCanvas.GetComponent<CanvasGroup> ().alpha -= step/4;

			if (mycam.transform.position == new Vector3 (-13, 2, 3)) {
				transformOptions = false;
				primaryCanvas.SetActive (false);
				optionsCanvas.SetActive (true);
				optionsCanvas.GetComponent<CanvasGroup> ().alpha = 1;
			}
		}
		if (transformPlayerSelect) {
			mycam.transform.position = Vector3.MoveTowards (mycam.transform.position, new Vector3 (-3, 2, 3), step);
			mycam.transform.rotation = Quaternion.RotateTowards (mycam.transform.rotation, Quaternion.Euler(0,90,0), step*20);
			primaryCanvas.GetComponent<CanvasGroup> ().alpha -= step/4;

			if (mycam.transform.position == new Vector3 (-3, 2, 3)) {
				transformPlayerSelect = false;
				primaryCanvas.SetActive (false);
				playerSelectCanvas.SetActive (true);
				playerSelectCanvas.GetComponent<CanvasGroup> ().alpha = 1;
			}
		}
		if (playerSelected) {
			mycam.transform.position = Vector3.MoveTowards (mycam.transform.position, new Vector3 (-8, 2, 10), step);
			mycam.transform.rotation = Quaternion.RotateTowards (mycam.transform.rotation, Quaternion.Euler (0, 0, 0), step * 20);

			playerSelectCanvas.GetComponent<CanvasGroup> ().alpha -= step / 4;

			if (mycam.transform.position == new Vector3 (-8, 2, 8)) {
				Debug.Log ("LOAD LEVEL NOW");
			}
		}
	}


	public void faceforward(){
		transformForward = true;
	}

	public void faceoptions(){
		transformOptions = true;
	}

	public void faceplayer(){
		transformPlayerSelect = true;
	}

	public void gamestart(){
		playerSelected = true;
	}
}
