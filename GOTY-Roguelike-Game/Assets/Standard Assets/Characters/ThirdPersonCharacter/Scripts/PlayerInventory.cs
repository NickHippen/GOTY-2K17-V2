using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInventory : MonoBehaviour {

	public List<GameObject> weapons = new List<GameObject>();
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	public void addWeapon(GameObject weapon){
		weapons.Add (weapon);
		Debug.Log ("Weapon added");
	}
}
