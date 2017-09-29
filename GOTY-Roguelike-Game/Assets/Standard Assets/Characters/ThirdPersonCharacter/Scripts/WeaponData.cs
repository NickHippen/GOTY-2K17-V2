using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponData : MonoBehaviour{

	public int id;
	public string name;
	public string desc;
	public int damage;
	public GameObject model;
	
	public WeaponData(){
		id = -1;
	}

	public WeaponData(int ID, string Name, string Description, int dmg, GameObject m){
		id = ID;
		name = Name;
		desc = Description;
		damage = dmg;
		model = m;
	}

	void Update(){
		if (transform.parent != null) {
			transform.position = transform.parent.position;
		}
	}
}
