using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossGhoul : AggressiveUnit {

	public KinematicProjectile projectile;
	public CloudSpawn cloudSummon;
	public GameObject mask;

	private CloudSpawn spawnedCloud;
	private bool maskDestroyed = false;

	protected override void ApplyAttackBehavior() {
		attacks.Add(new KinematicProjectileAttack(
			new IntervalAttackController(this, 2, 2),
			projectile,
			(healthManager) => {
				healthManager.Damage(this.attackPower);
			},
			0.7f
		));
		attacks.Add(new SpawnAttack(
			new HealthAttackController(this, 0.5f),
			cloudSummon,
			1,
			true,
			unit => {
				spawnedCloud = (CloudSpawn)unit;
			}
		));
	}

	public override void Damage(float amount) {
		base.Damage(amount);
		if (!maskDestroyed && HealthPercentage <= 0.5f) {
			Destroy(mask);
			maskDestroyed = true;
		}
	}

	public override void OnDeath() {
		if (!destroying) {
			Destroy(spawnedCloud.gameObject);
		}
		base.OnDeath();
	}

}
