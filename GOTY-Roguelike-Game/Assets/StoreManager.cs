using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StoreManager : MonoBehaviour {
	public GameObject weaponSpawner;

	// Use this for initialization
	void Start () {
		for (int i = -1; i < 2; i++) {
			for (int j = -1; j < 2; j++) {
				int randomNum = Random.Range (0, 3);
				if (randomNum == 0)
					continue;
				GameObject instance = Instantiate (weaponSpawner);
				instance.transform.SetParent (this.transform);
				instance.transform.localPosition = new Vector3 (8 * i, .5f, 8 * j);
			}
		}
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
