using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IntervalAttackController : AttackController {

	private float minAttackRate;
	private float maxAttackRate;

	private float timeSinceAttack = 0f;
	private float nextAttackTime;

	public IntervalAttackController(AggressiveUnit attacker, float minAttackRate, float maxAttackRate) : base(attacker, true) {
		this.minAttackRate = minAttackRate;
		this.maxAttackRate = maxAttackRate;
		nextAttackTime = 0f; // Set to 0 first so enemies attack on arrival
	}

	protected override bool CheckSpecifics() {
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
