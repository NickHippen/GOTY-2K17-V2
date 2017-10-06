using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInventory : MonoBehaviour {

	public List<GameObject> weapons = new List<GameObject>();
	// Use this for initialization
	public int test = 0;
	private int current= 0;
	void Start () {
		
	}
	
	// Update is called once per frame
	void deltaUpdate () {
		if (Input.GetKey (KeyCode.R)) {
			test++;
		}
	}

	public void addWeapon(GameObject weapon){
		if (weapons.Count < 4) {
			//current++;
			weapons.Add (weapon);
			Debug.Log ("Weapon added");
		}
	}

	public GameObject getCurrentWeapon(){
		if (weapons.Count != 0) {
			return weapons [current];
		} else {
			return null;
		}
	}

	public void setCurrentWeapon(int x){
		if (x < weapons.Count) {
			weapons [current].SetActive(false);
			current = x;
			weapons [current].SetActive(true);
		}
	}

	public void scrollWeapon(float scrolled){
		int selection;
		if (scrolled > 0) {
			selection = current + 1;
			if (selection >= weapons.Count) {
				selection = 0;
			}
			setCurrentWeapon (selection);
		} else if (scrolled < 0) {
			selection = current - 1;
			if (selection < 0) {
				selection = weapons.Count - 1;
			}
			setCurrentWeapon (selection);
		}
	}

	public void setCurrentWeapon(GameObject thing){
		weapons [current] = thing;
	}

	public bool isEmpty(){
		if (weapons.Count == 0) {
			return true;
		} else {
			return false;
		}
	}

	public bool isFull(){
		if (weapons.Count == 4) {
			return true;
		} else {
			return false;
		}
	}
}
