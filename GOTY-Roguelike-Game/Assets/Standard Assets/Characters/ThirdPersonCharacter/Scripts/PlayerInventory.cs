using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInventory : MonoBehaviour {

	public List<WeaponData> weapons = new List<WeaponData>();
	// Use this for initialization
	private int current=0;
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	public void addWeapon(WeaponData weapon){
		weapons.Add (weapon);
		Debug.Log ("Weapon added");
	}

	public WeaponData getCurrentWeapon(){
		return weapons [current];
	}
}
