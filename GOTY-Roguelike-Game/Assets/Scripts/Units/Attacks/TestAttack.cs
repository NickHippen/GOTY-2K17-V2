using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestAttack : Attack {

	public TestAttack(AttackController controller) : base(controller) {
	}

	public override void Use() {
		MonoBehaviour.print("Used test attack");
	}

}
