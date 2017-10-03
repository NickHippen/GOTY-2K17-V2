using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInventory : MonoBehaviour {

	public List<GameObject> weapons = new List<GameObject>();
	// Use this for initialization
	private int current=-1;
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		if (!isEmpty()) {
			//weapons [current].transform.position = weapons [current].transform.parent.position;
			weapons[current].transform.parent.localPosition = new Vector3 (0, 0, 0);
		}
	}

	public void addWeapon(GameObject weapon){
		current++;
		weapons.Add (weapon);
		Debug.Log ("Weapon added");
	}

	public GameObject getCurrentWeapon(){
		if (weapons.Count != 0) {
			return weapons [current];
		} else {
			return null;
		}
	}

	public bool isEmpty(){
		if (weapons.Count == 0) {
			return true;
		} else {
			return false;
		}
	}
}
