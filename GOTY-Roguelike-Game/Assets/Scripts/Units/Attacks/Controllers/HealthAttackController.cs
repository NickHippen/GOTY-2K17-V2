using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthAttackController : AttackController {

	private float healthPercentageTrigger;
	private bool used = false;

	public HealthAttackController(AggressiveUnit attacker, float healthPercentageTrigger) : base(attacker) {
		this.healthPercentageTrigger = healthPercentageTrigger;
	}

	protected override bool CheckSpecifics() {
		if (!used && Attacker.HealthPercentage <= healthPercentageTrigger) {
			used = true;
			return true;
		}
		return false;
	}

	public void Reset() {
		used = false;
	}

}
