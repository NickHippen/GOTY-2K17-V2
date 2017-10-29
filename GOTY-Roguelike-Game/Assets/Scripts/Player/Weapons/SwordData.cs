using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwordData : WeaponData {

	void OnTriggerEnter(Collider collision) {
		RigCollider rigCollider = collision.gameObject.GetComponent<RigCollider>();
		Debug.Log(collision);
		if (rigCollider != null) {
			Debug.Log("Hit");
		}
		if (rigCollider != null && rigCollider.RootUnit is AggressiveUnit) {
			Debug.Log("Damaging!!!");
			((AggressiveUnit)rigCollider.RootUnit).Damage(this.damage);
		}
	}

}
