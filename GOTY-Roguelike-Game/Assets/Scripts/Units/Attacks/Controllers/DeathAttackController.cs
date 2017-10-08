using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeathAttackController : AttackController {

	private AggressiveUnit attacker;

	private bool used = false;

	public DeathAttackController(AggressiveUnit attacker) {
		this.attacker = attacker;
	}

	protected override bool CheckSpecifics() {
		if (!used && !attacker.Living) {
			used = true;
			return true;
		}
		return false;
	}

	public void Reset() {
		used = false;
	}

}