using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestUnit : AggressiveUnit {

	protected override void ApplyAttackBehavior() {
		attacks.Add(new TestAttack(
			new IntervalAttackController(2, 3)
				.AddConditional(() => GetHealthPercentage() > 0.5f),
			"Interval Attack"
		));
		attacks.Add(new TestAttack(
			new HealthAttackController(this, 0.4f),
			"Health Attack"
		));
	}

}
