using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossDragon : AggressiveUnit {

	public Transform flameThrowerOriginPoint;

	protected override void ApplyAttackBehavior() {
		attacks.Add(new BasicDamageAttack(
			new IntervalAttackController(this, 2, 2)
		));
		attacks.Add(new FlamethrowerAttack(
			new IntervalAttackController(this, 4, 4)
				.AddConditional(CheckFlamethrower),
			flameThrowerOriginPoint
		));
	}

	private bool CheckFlamethrower() {
		if (target == null) {
			return false;
		}
		float dist = Vector3.Distance(this.transform.position, target.position);
		Debug.Log(dist);
		if (dist < 10f && dist > destinationRadius + 1) {
			return true;
		}
		return false;
	}

}
