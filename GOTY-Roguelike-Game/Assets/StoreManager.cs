using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StoreManager : MonoBehaviour {
	public GameObject weaponSpawner;

	public int minCost;
	public int maxCost;

	// Use this for initialization
	void Start () {
		for (int i = -1; i < 2; i++) {
			for (int j = -1; j < 2; j++) {
				if ((int)(Random.Range (0, 2)) == 1)
					continue;
				GameObject instance = Instantiate (weaponSpawner);
				instance.transform.SetParent (this.transform);
				instance.transform.localPosition = new Vector3 (8 * i, .5f, 8 * j);

				int thisCost = Random.Range (minCost, maxCost + 1);
				instance.GetComponent<WeaponData> ().cost = thisCost;
			}
		}
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
