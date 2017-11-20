using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GluttonyBat : AggressiveUnit {

	protected override void ApplyAttackBehavior() {
		attacks.Add(new BasicDamageAttack(
			new IntervalAttackController(this, 2, 2)
		));
	}

}
