using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IntervalAttackController : AttackController {

	private float minAttackRate;
	private float maxAttackRate;

	private float timeSinceAttack = 0f;
	private float nextAttackTime;

	public IntervalAttackController(float minAttackRate, float maxAttackRate) {
		this.minAttackRate = minAttackRate;
		this.maxAttackRate = maxAttackRate;
		SetNextAttackTime();
	}

	public override bool Check() {
		timeSinceAttack += Time.deltaTime;
		if (timeSinceAttack >= nextAttackTime) {
			timeSinceAttack = 0; // Reset attack time
			SetNextAttackTime();
			return true;
		}
		return false;
	}

	private void SetNextAttackTime() {
		nextAttackTime = Random.Range(minAttackRate, maxAttackRate);
	}

}
