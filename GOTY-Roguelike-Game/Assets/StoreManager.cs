using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StoreManager : MonoBehaviour {
	public int hello;
	public GameObject weaponSpawner;

	// Use this for initialization
	void Start () {
		for (int i = -1; i < 2; i++) {
			for (int j = -1; j < 2; j++) {
				if ((int)(Random.Range (0, 2)) == 1)
					continue;
				GameObject instance = Instantiate (weaponSpawner);
				instance.transform.SetParent (this.transform);
				instance.transform.position = this.transform.position;
				instance.transform.position = new Vector3 (8 * i, 22f, 8 * j);
			}
		}
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
