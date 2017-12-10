using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossSlime : AggressiveUnit {

	public BossSlimeSpawn slimeUnit;

	private Vector3 baseScale;

	protected override void Start() {
		base.Start();
		baseScale = transform.localScale;
		sfx = GetComponent<SoundData> ();
	}

	protected override void ApplyAttackBehavior() {
		attacks.Add(new BasicDamageAttack(
			new IntervalAttackController(this, 2, 2)
		));
		for (float x = 0.9f; x > 0f; x -= 0.1f) {
			attacks.Add(new SpawnAttack(
				new HealthAttackController(this, x),
				slimeUnit
			));
		}
		//if (slimeUnit != null) { // Null if final slimes
		//	attacks.Add(new SpawnAttack(
		//		new DeathAttackController(this),
		//		slimeUnit,
		//		2
		//	));
		//}
	}

	//public override void OnDeath() {
	//	if (slimeUnit != null) {
	//		Destroy(gameObject, 1f);
	//	} else {
	//		base.OnDeath();
	//	}
	//}

	public override void Damage(float amount) {
		base.Damage(amount);
		transform.localScale = baseScale * (0.25f + (HealthPercentage / 1.33333f));
	}

}
