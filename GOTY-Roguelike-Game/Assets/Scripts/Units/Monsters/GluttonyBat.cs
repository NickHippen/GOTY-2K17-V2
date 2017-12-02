﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GluttonyBat : AggressiveUnit {

	public KinematicProjectile projectile;

	protected override void ApplyAttackBehavior() {
		attacks.Add(new BasicDamageAttack(
			new IntervalAttackController(this, 2, 2)
		));
		attacks.Add(new KinematicProjectileAttack(
			new IntervalAttackController(this, 2, 2),
			projectile,
			(healthManager) => {
				healthManager.Damage(this.attackPower);
			},
			0.4f,
			3f
		));
	}

}
