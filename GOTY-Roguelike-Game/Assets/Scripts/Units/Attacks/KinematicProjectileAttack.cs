using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KinematicProjectileAttack : Attack {

	private float heightOffset;

	public KinematicProjectile Projectile { get; set; }
	public PlayerCollision HandlePlayerCollision { get; set; }
	public float AttackDelay { get; set; }

	public KinematicProjectileAttack(AttackController controller, KinematicProjectile projectile, PlayerCollision handlePlayerCollision, float attackDelay, float heightOffset=1f) : base(controller) {
		this.Projectile = projectile;
		this.HandlePlayerCollision = handlePlayerCollision;
		this.AttackDelay = attackDelay;
		this.heightOffset = heightOffset;
	}

	public override void Use(Transform target) {
		Attacker.UnitAnimator.SetBool("SpecialAttack", true);
		Attacker.StartCoroutine(DelayedUse(target));
	}

	private IEnumerator DelayedUse(Transform target) {
		yield return new WaitForSeconds(AttackDelay);
		KinematicProjectile projectile = GameObject.Instantiate(Projectile);
		//Debug.Log(Controller.Attacker.transform.forward);
		projectile.transform.position = Controller.Attacker.transform.position + Vector3.up * (Controller.Attacker.canvasHeight / 2f);
		projectile.caster = Controller.Attacker;
		projectile.target = target;
		projectile.HandlePlayerCollision = HandlePlayerCollision;
		Attacker.StopCoroutine(DelayedUse(target));
		yield return null;
	}

}
