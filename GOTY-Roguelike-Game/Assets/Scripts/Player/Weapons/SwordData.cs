using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwordData : WeaponData {

	void OnTriggerEnter(Collider collision) {
		RigCollider rigCollider = collision.gameObject.GetComponent<RigCollider>();
		if (rigCollider != null && rigCollider.RootUnit is AggressiveUnit) {
			AggressiveUnit monster = ((AggressiveUnit)rigCollider.RootUnit);
			float damage = this.damage;
            damage *= damageMultiplier;
			damage = WeaponEmotionActionHandler.GetOnDamageAction(emotion)(this, monster, damage);
			damage = WeaponModifierActionHandler.GetOnDamageAction(modifier)(this, monster, damage);
			monster.Damage(damage);
		}
	}
}
