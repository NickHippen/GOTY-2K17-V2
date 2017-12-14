using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class PauseHandler : MonoBehaviour {

	GameObject pauseHud;
	GameObject weaponInfo;
	// Use this for initialization
	void Start () {
		pauseHud = GameObject.Find ("PauseHUD");
		weaponInfo = GameObject.Find ("WeaponInfo");
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	public void showWeaponInfo(){
		GameObject.Find ("PauseHUD").GetComponent<CanvasGroup> ().alpha = 0;
		GameObject.Find ("WeaponInfo").GetComponent<CanvasGroup> ().alpha = 1;
		GameObject.Find ("weaponinfobutton").GetComponent<Button> ().interactable = false;
		GameObject.Find ("resumebutton").GetComponent<Button> ().interactable = false;
		GameObject.Find ("returntomenubutton").GetComponent<Button> ().interactable = false;
		GameObject.Find ("weaponbackbutton").GetComponent<Button> ().interactable = true;
		GameObject.Find ("WeaponInfo").GetComponent<Canvas> ().sortingOrder = 1;
		GameObject.Find ("PauseHUD").GetComponent<Canvas> ().sortingOrder = 0;
	}

	public void showPauseInfo(){
		GameObject.Find ("PauseHUD").GetComponent<CanvasGroup> ().alpha = 1;
		GameObject.Find ("WeaponInfo").GetComponent<CanvasGroup> ().alpha = 0;
		GameObject.Find ("weaponinfobutton").GetComponent<Button> ().interactable = true;
		GameObject.Find ("resumebutton").GetComponent<Button> ().interactable = true;
		GameObject.Find ("returntomenubutton").GetComponent<Button> ().interactable = true;
		GameObject.Find ("weaponbackbutton").GetComponent<Button> ().interactable = false;
		GameObject.Find ("WeaponInfo").GetComponent<Canvas> ().sortingOrder = 0;
		GameObject.Find ("PauseHUD").GetComponent<Canvas> ().sortingOrder = 1;
	}
}
