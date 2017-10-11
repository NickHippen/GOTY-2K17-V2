using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInventory : MonoBehaviour {

	public List<GameObject> weapons = new List<GameObject>();
	// Use this for initialization
	public int test = 0;
	public int current= 0;
	int maxCapacity = 4;
	void Start () {
		
	}
	
	// Update is called once per frame
	void deltaUpdate () {
		if (Input.GetKey (KeyCode.R)) {
			test++;
		}
	}

	public void addWeapon(GameObject weapon){
		if (weapons.Count < maxCapacity) {
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
			weapons [current].transform.localEulerAngles = weapons [current].GetComponent<WeaponData> ().rotation;
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
		weapons [current].transform.localEulerAngles = weapons [current].GetComponent<WeaponData> ().rotation;
	}

	public void dropCurrentWeapon(){
		//weapons [current].GetComponent<Rigidbody> ().useGravity = true;

		weapons.RemoveAt (current);
		if (current >= weapons.Count && current != 0) {
			current--;
		}
		setCurrentWeapon (current);
		weapons [current].transform.localEulerAngles = weapons [current].GetComponent<WeaponData> ().rotation;
	}

	public bool isEmpty(){
		if (weapons.Count == 0) {
			return true;
		} else {
			return false;
		}
	}

	public bool lastItem(){
		if (weapons.Count == 1) {
			return true;
		} else {
			return false;
		}
	}

	public bool isFull(){
		if (weapons.Count == maxCapacity) {
			return true;
		} else {
			return false;
		}
	}
}
