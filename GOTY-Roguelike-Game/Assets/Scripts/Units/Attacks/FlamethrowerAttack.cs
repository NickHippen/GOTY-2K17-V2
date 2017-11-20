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
		Attacker.StartCoroutine(StartFlame(target));
		//Attacker.StartCoroutine(DisableFlame(target));
	}

	private IEnumerator StartFlame(Transform target) {
		yield return new WaitForSeconds(0.5f);
		OriginPoint.gameObject.SetActive(true);
		yield return null;
	}

	//private IEnumerator DisableFlame(Transform target) {
	//	yield return new WaitForSeconds(2.5f);
	//	OriginPoint.gameObject.SetActive(false);
	//	Attacker.speed = Attacker.DefaultSpeed;
	//	yield return null;
	//}

}
