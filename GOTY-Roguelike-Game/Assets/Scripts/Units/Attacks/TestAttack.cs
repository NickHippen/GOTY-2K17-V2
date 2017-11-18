using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestAttack : Attack {

	private string text;

	public TestAttack(AttackController controller, string text) : base(controller) {
		this.text = text;
	}

	public override void Use(Transform target) {
		MonoBehaviour.print(text);
		Attacker.UnitAnimator.SetBool("Attack", true);
	}

}
