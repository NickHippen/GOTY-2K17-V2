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
			AggressiveUnit monster = ((AggressiveUnit)rigCollider.RootUnit);
			float damage = this.damage;
			damage = WeaponEmotionActionHandler.GetAction(Emotion)(this, monster, damage);
			monster.Damage(damage);
		}
	}

}
