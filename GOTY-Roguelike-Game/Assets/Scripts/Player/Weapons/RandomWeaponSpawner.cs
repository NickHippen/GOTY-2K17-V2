using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomWeaponSpawner : MonoBehaviour {

	public GameObject swordBase;
	public GameObject gunBase;

	void Start () {
		int roll = Random.Range(0, 2);
		switch (roll) {
			case 0:
				WeaponFactory.CreateRandomSword(swordBase, this.transform.position, 1, 0.5f);
				break;
			case 1:
				WeaponFactory.CreateRandomGun(gunBase, this.transform.position, 1, 0.5f);
				break;
			default:
				break;
		}
	}
	
	void Update () {
		
	}
}
