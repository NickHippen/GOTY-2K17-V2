using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MenuManager : MonoBehaviour {
	Camera  mycam;

	public GameObject primaryCanvas;
	public GameObject optionsCanvas;
	public GameObject playerSelectCanvas;
	public GameObject levelManager;

	//Buttons to make interactive
	public GameObject primary_play;
	public GameObject primary_options;
	public GameObject primary_quit;
	public GameObject options_back;
	public GameObject select_back;
	public GameObject select_start;
	public GameObject select_ber;
	public GameObject select_gun;
	public GameObject select_wiz;
	public GameObject select_rog;

	public Sprite berIcon;
	public Sprite gunIcon;
	public Sprite wizIcon;
	public Sprite rogIcon;

	public GameObject berAbilities;
	public GameObject gunAbilities;
	public GameObject wizAbilities;
	public GameObject rogAbilities;

	public GameObject sword;
	public GameObject gun;
	public GameObject staff;

	public float speed;

	bool transformForward = false;
	bool transformOptions = false;
	bool transformPlayerSelect = false;
	bool playerSelected = false;

	Animator anim;

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
				togglePrimarySounds ();
			}
		}
		//Move the camera to view the options menu
		if (transformOptions) {
			mycam.transform.position = Vector3.MoveTowards (mycam.transform.position, new Vector3 (-13, 2, 3), step);
			mycam.transform.rotation = Quaternion.RotateTowards (mycam.transform.rotation, Quaternion.Euler(0,-90,0), step*20);
			primaryCanvas.GetComponent<CanvasGroup> ().alpha -= step/4;

			if (mycam.transform.position == new Vector3 (-13, 2, 3)) {
				transformOptions = false;
				primaryCanvas.SetActive (false);
				optionsCanvas.SetActive (true);
				optionsCanvas.GetComponent<CanvasGroup> ().alpha = 1;
				toggleOptionsSounds ();
			}
		}
		//Move the camera to view the player selection menu
		if (transformPlayerSelect) {
			mycam.transform.position = Vector3.MoveTowards (mycam.transform.position, new Vector3 (-3, 2, 3), step);
			mycam.transform.rotation = Quaternion.RotateTowards (mycam.transform.rotation, Quaternion.Euler(0,90,0), step*20);
			primaryCanvas.GetComponent<CanvasGroup> ().alpha -= step/4;

			if (mycam.transform.position == new Vector3 (-3, 2, 3)) {
				transformPlayerSelect = false;
				primaryCanvas.SetActive (false);
				playerSelectCanvas.SetActive (true);
				playerSelectCanvas.GetComponent<CanvasGroup> ().alpha = 1;
				toggleSelectSounds ();
			}
		}
		//Move the camera to the entrance, then load the game
		if (playerSelected) {
			mycam.transform.position = Vector3.MoveTowards (mycam.transform.position, new Vector3 (-8, 2, 10), step);
			mycam.transform.rotation = Quaternion.RotateTowards (mycam.transform.rotation, Quaternion.Euler (0, 0, 0), step * 20);

			playerSelectCanvas.GetComponent<CanvasGroup> ().alpha -= step / 4;
			if (mycam.transform.position == new Vector3 (-8, 2, 10)) {
				levelManager.GetComponent<LevelManager> ().LoadNextLevel ();
			}
		}
	}


	public void faceforward(){
		//Turn the options sounds and buttons off
		if (options_back.GetComponent<Button> ().interactable == true) {
			toggleOptionsButtons ();
			toggleOptionsSounds ();
		}

		//Turn the select sounds and buttons off
		if (select_back.GetComponent<Button> ().interactable == true) {
			toggleSelectButtons ();
			toggleSelectSounds ();
		}

		togglePrimaryButtons ();
		transformForward = true;
	}

	public void faceoptions(){
		togglePrimaryButtons ();
		togglePrimarySounds ();
		toggleOptionsButtons ();
		transformOptions = true;
	}

	public void faceplayer(){
		togglePrimaryButtons ();
		togglePrimarySounds ();
		toggleSelectButtons ();
		transformPlayerSelect = true;
	}

	public void gamestart(){
		toggleSelectButtons ();
		toggleSelectSounds ();
		playerSelected = true;
	}

	public void selectBerseker(){
		GameObject.Find ("remy").GetComponent<AbilityController> ().enabled = false;
		GameObject.Find ("remy").GetComponent<AbilityController> ().classType = "berserker";
		GameObject.Find ("remy").GetComponent<AbilityController> ().enabled = true;

		GameObject.Find ("remy").GetComponent<PlayerInventory> ().setCurrentWeapon (0);
		GameObject.Find ("remy").GetComponent<ThirdPersonCharacter> ().setWeaponAnimations ();

		GameObject.Find ("UserPic").GetComponent<Image> ().sprite = berIcon;
		berAbilities.GetComponent<CanvasGroup> ().alpha = 1;
		gunAbilities.GetComponent<CanvasGroup> ().alpha = 0;
		wizAbilities.GetComponent<CanvasGroup> ().alpha = 0;
		rogAbilities.GetComponent<CanvasGroup> ().alpha = 0;
	}

	public void selectGunslinger(){
		GameObject.Find ("remy").GetComponent<AbilityController> ().enabled = false;
		GameObject.Find ("remy").GetComponent<AbilityController> ().classType = "gunslinger";
		GameObject.Find ("remy").GetComponent<AbilityController> ().enabled = true;

		GameObject.Find ("remy").GetComponent<PlayerInventory>().setCurrentWeapon(1);
		GameObject.Find ("remy").GetComponent<ThirdPersonCharacter> ().setWeaponAnimations ();

		GameObject.Find ("UserPic").GetComponent<Image> ().sprite = gunIcon;
		berAbilities.GetComponent<CanvasGroup> ().alpha = 0;
		gunAbilities.GetComponent<CanvasGroup> ().alpha = 1;
		wizAbilities.GetComponent<CanvasGroup> ().alpha = 0;
		rogAbilities.GetComponent<CanvasGroup> ().alpha = 0;
	}

	public void selectWizard(){
		GameObject.Find ("remy").GetComponent<AbilityController> ().enabled = false;
		GameObject.Find ("remy").GetComponent<AbilityController> ().classType = "wizard";
		GameObject.Find ("remy").GetComponent<AbilityController> ().enabled = true;
		GameObject.Find ("UserPic").GetComponent<Image> ().sprite = wizIcon;
		berAbilities.GetComponent<CanvasGroup> ().alpha = 0;
		gunAbilities.GetComponent<CanvasGroup> ().alpha = 0;
		wizAbilities.GetComponent<CanvasGroup> ().alpha = 1;
		rogAbilities.GetComponent<CanvasGroup> ().alpha = 0;
	}

	public void selectRogue(){
		GameObject.Find ("remy").GetComponent<AbilityController> ().enabled = false;
		GameObject.Find ("remy").GetComponent<AbilityController> ().classType = "rogue";
		GameObject.Find ("remy").GetComponent<AbilityController> ().enabled = true;
		GameObject.Find ("UserPic").GetComponent<Image> ().sprite = rogIcon;
		berAbilities.GetComponent<CanvasGroup> ().alpha = 0;
		gunAbilities.GetComponent<CanvasGroup> ().alpha = 0;
		wizAbilities.GetComponent<CanvasGroup> ().alpha = 0;
		rogAbilities.GetComponent<CanvasGroup> ().alpha = 1;
	}

	public void togglePrimaryButtons(){
		primary_play.GetComponent<Button> ().interactable = !primary_play.GetComponent<Button> ().interactable;
		primary_options.GetComponent<Button> ().interactable = !primary_options.GetComponent<Button> ().interactable;
		primary_quit.GetComponent<Button> ().interactable = !primary_quit.GetComponent<Button> ().interactable;

	}

	public void toggleOptionsButtons(){
		options_back.GetComponent<Button> ().interactable = !options_back.GetComponent<Button> ().interactable;
	}

	public void toggleSelectButtons(){
		select_back.GetComponent<Button> ().interactable = !select_back.GetComponent<Button> ().interactable;
		select_start.GetComponent<Button> ().interactable = !select_start.GetComponent<Button> ().interactable;
		select_ber.GetComponent<Button> ().interactable = !select_ber.GetComponent<Button> ().interactable;
		select_gun.GetComponent<Button> ().interactable = !select_gun.GetComponent<Button> ().interactable;
		select_wiz.GetComponent<Button> ().interactable = !select_wiz.GetComponent<Button> ().interactable;
		select_rog.GetComponent<Button> ().interactable = !select_rog.GetComponent<Button> ().interactable;
	}

	public void togglePrimarySounds(){
		primary_play.GetComponentInParent<PointerSound> ().enabled = !primary_play.GetComponentInParent<PointerSound> ().enabled;
		primary_options.GetComponentInParent<PointerSound> ().enabled = !primary_options.GetComponentInParent<PointerSound> ().enabled;
		primary_quit.GetComponentInParent<PointerSound> ().enabled = !primary_quit.GetComponentInParent<PointerSound> ().enabled;
	}

	public void toggleOptionsSounds(){
		options_back.GetComponentInParent<PointerSound> ().enabled = !options_back.GetComponentInParent<PointerSound> ().enabled;
	}

	public void toggleSelectSounds(){
		select_back.GetComponentInParent<PointerSound> ().enabled = !select_back.GetComponentInParent<PointerSound> ().enabled;
		select_start.GetComponentInParent<PointerSound> ().enabled = !select_start.GetComponentInParent<PointerSound> ().enabled;
		select_ber.GetComponentInParent<PointerSound> ().enabled = !select_ber.GetComponentInParent<PointerSound> ().enabled;
		select_gun.GetComponentInParent<PointerSound> ().enabled = !select_gun.GetComponentInParent<PointerSound> ().enabled;
		select_wiz.GetComponentInParent<PointerSound> ().enabled = !select_wiz.GetComponentInParent<PointerSound> ().enabled;
		select_rog.GetComponentInParent<PointerSound> ().enabled = !select_rog.GetComponentInParent<PointerSound> ().enabled;
	}
}
