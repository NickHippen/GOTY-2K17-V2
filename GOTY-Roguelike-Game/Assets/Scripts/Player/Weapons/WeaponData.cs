using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponData : MonoBehaviour{

	public int id;
	public string name;
	public string desc;
	public int damage;
	public Vector3 rotation;
	public GameObject model;
	public Sprite icon;
	
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

	void fixedUpdate(){
		if (transform.parent != null) {
			Debug.Log ("Update Position");
			//transform.localPosition = new Vector3 (0, 0, 0);
		}
	}

	void OnTriggerEnter(Collider collision) {
		RigCollider rigCollider = collision.gameObject.GetComponent<RigCollider>();
		Debug.Log(collision);
		if (rigCollider != null) {
			Debug.Log("Hit");
		}
		if (rigCollider != null && rigCollider.RootUnit is AggressiveUnit) {
			Debug.Log("Damaging!!!");
			((AggressiveUnit)rigCollider.RootUnit).Damage(10f);
		}
	}

}
