using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerInventory : MonoBehaviour {

	public List<GameObject> weapons = new List<GameObject>();
	// Use this for initialization
	public int test = 0;
	public int current= 0;
	int maxCapacity = 4;
	public List<GameObject> slots;
	public Sprite empty;

	void Start () {
		for (int x = 0; x < maxCapacity; x++) {
			slots.Add (GameObject.Find ("Weapon " + x));
		}
		slots [current].GetComponent<Image>().sprite = weapons [current].GetComponent<WeaponData>().icon;
	}
	
	// Update is called once per frame
	void deltaUpdate () {
		if (Input.GetKey (KeyCode.R)) {
			test++;
		}
	}

	//Addd GameObject weapon to the weapons list
	public void addWeapon(GameObject weapon){
		if (weapons.Count < maxCapacity) {
			//current++;
			weapons.Add (weapon);
			Debug.Log ("Weapon added");
		}
		//slots [weapons.Count - 1].GetComponent<Image> ().sprite = weapons [weapons.Count - 1].GetComponent<WeaponData> ().icon;
		UpdateUI();
	}

	//returns the current weapon the player is holding if they are holding
	//one at all
	public GameObject getCurrentWeapon(){
		if (weapons.Count != 0) {
			return weapons [current];
		} else {
			return null;
		}
	}

	//Sets the current weapon to the weapon found in the given list position x. If the number is bigger
	//than the number than the number of weapons currently possesed, then nothing happens
	public void setCurrentWeapon(int x){
		if (x < weapons.Count) {
			weapons [current].SetActive(false);
			current = x;
			weapons [current].SetActive(true);
			weapons [current].transform.localEulerAngles = weapons [current].GetComponent<WeaponData> ().rotation;
		}
	}

	//Checks if scrolling input is in a forwards or backwards direction and changes the current
	//Weapon accordingly.
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

	//Sets the current weapon based on a given GameOBject
	public void setCurrentWeapon(GameObject thing){
		weapons [current] = thing;
		weapons [current].transform.localEulerAngles = weapons [current].GetComponent<WeaponData> ().rotation;
	}

	//Removes current weapon from inventory then changes current weapon to the next weapon below it.
	public void dropCurrentWeapon(){
		//weapons [current].GetComponent<Rigidbody> ().useGravity = true;

		weapons.RemoveAt (current);
		if (current >= weapons.Count && current != 0) {
			current--;
		}
		setCurrentWeapon (current);
		weapons [current].transform.localEulerAngles = weapons [current].GetComponent<WeaponData> ().rotation;
		UpdateUI ();
	}

	//Checks if list has no more items
	public bool isEmpty(){
		if (weapons.Count == 0) {
			return true;
		} else {
			return false;
		}
	}

	//Checks if list has only one item left
	public bool lastItem(){
		if (weapons.Count == 1) {
			return true;
		} else {
			return false;
		}
	}

	//Checks whether the inventory is full.
	public bool isFull(){
		if (weapons.Count == maxCapacity) {
			return true;
		} else {
			return false;
		}
	}

	void UpdateUI(){
		for (int x = 0; x < maxCapacity; x++) {
			slots [x].GetComponent<Image> ().sprite = empty;
		}

		for (int x = 0; x < weapons.Count; x++) {
			slots [x].GetComponent<Image> ().sprite = weapons [x].GetComponent<WeaponData> ().icon;
		}
	}
}
