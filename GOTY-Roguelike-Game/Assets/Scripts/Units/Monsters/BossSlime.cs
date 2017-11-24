using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossSlime : AggressiveUnit {

	public BossSlime slimeUnit;

	private Vector3 baseScale;

	protected override void Start() {
		base.Start();
		baseScale = transform.localScale;
	}

	protected override void ApplyAttackBehavior() {
		attacks.Add(new BasicDamageAttack(
			new IntervalAttackController(this, 2, 2)
		));
		if (slimeUnit != null) { // Null if final slimes
			attacks.Add(new SpawnAttack(
				new DeathAttackController(this),
				slimeUnit,
				2
			));
		}
	}

	public override void OnDeath() {
		if (slimeUnit != null) {
			Destroy(gameObject, 1f);
		} else {
			base.OnDeath();
		}
	}

	public override void Damage(float amount) {
		base.Damage(amount);
		transform.localScale = baseScale * (0.5f + (HealthPercentage / 2f));
	}

}
