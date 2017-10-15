using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeathAttackController : AttackController {

	private bool used = false;

	public DeathAttackController(AggressiveUnit attacker) : base(attacker) {
	}

	protected override bool CheckSpecifics() {
		if (!used && !Attacker.Living) {
			used = true;
			return true;
		}
		return false;
	}

	public void Reset() {
		used = false;
	}

}