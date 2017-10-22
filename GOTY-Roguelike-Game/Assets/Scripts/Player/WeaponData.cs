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

	void OnCollisionEnter(Collision collision) {
		if (collision.gameObject.tag == "Monster") {
			AggressiveUnit monster = collision.gameObject.GetComponent<AggressiveUnit>();
			monster.Damage(10f);
		}
		//List<ICollection> interfaceList;
		//GetInterfaces<ICollection>(out interfaceList, collision.gameObject);
	}

	private void GetInterfaces<T>(out List<T> resultList, GameObject objectToSearch) where T : class {
		MonoBehaviour[] list = objectToSearch.GetComponents<MonoBehaviour>();
		resultList = new List<T>();
		foreach (MonoBehaviour mb in list) {
			if (mb is T) {
				//found one
				resultList.Add((T)((System.Object)mb));
			}
		}
	}

}
