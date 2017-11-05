using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossSlime : AggressiveUnit {

	public BossSlime slimeUnit;

	protected override void ApplyAttackBehavior() {
		attacks.Add(new BasicDamageAttack(
			new IntervalAttackController(this, 2, 2)
		));
		if (slimeUnit != null) { // Null if final slimes
			attacks.Add(new SpawnAttack(
				new DeathAttackController(this),
				slimeUnit,
				2
			));
		}
	}

	public override void OnDeath() {
		if (slimeUnit != null) {
			Destroy(gameObject, 1f);
		} else {
			base.OnDeath();
		}
	}

}
