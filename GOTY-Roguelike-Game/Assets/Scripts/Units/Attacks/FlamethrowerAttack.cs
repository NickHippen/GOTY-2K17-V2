using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlamethrowerAttack : Attack {

	public Transform OriginPoint { get; set; }

	public FlamethrowerAttack(AttackController controller, Transform originPoint) : base(controller) {
		this.OriginPoint = originPoint;
		controller.RequireGoal = false;
	}

	public override void Use(Transform target) {
		Attacker.transform.rotation = Quaternion.LookRotation(target.position - Attacker.transform.position);
		Attacker.speed = 0;
		Attacker.UnitAnimator.SetBool("SpecialAttack", true);
	}

}
