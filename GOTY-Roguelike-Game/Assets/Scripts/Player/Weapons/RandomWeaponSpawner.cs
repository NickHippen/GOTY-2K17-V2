using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomWeaponSpawner : MonoBehaviour {

	public GameObject swordBase;
	public GameObject gunBase;
    public GameObject staffBase;
    public GameObject daggerBase;

    void Start () {
		int roll = Random.Range(0,4);
		switch (roll) {
			case 0:
				WeaponFactory.CreateRandomSword (swordBase, this.transform.position, 1, 0.5f, this.transform);
				break;
			case 1:
				WeaponFactory.CreateRandomGun(gunBase, this.transform.position, 1, 0.5f, this.transform);
				break;
            case 2:
                WeaponFactory.CreateRandomStaff(staffBase, this.transform.position, 1, 0.5f, this.transform);
                break;
            case 3:
                WeaponFactory.CreateRandomDagger(daggerBase, this.transform.position, 1, 0.5f, this.transform);
                break;
            default:
				break;
		}
	}
	
	void Update () {
		
	}
}
