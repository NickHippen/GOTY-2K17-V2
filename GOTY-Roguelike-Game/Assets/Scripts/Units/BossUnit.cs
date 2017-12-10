using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class BossUnit : AggressiveUnit {

	// Bosses must have aggro before they will take damage
	public override void Damage(float amount) {
		if (this.target == null) {
			return;
		}
		base.Damage(amount);
	}

	public override void Damage(float amount, Transform attacker) {
		if (this.target == null) {
			return;
		}
		base.Damage(amount, attacker);
	}

}
