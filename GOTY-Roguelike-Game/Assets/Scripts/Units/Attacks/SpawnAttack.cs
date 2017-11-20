using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnAttack : Attack {

	private Unit unit;
	private int count = 1;
	private float radius = 10f;

	public SpawnAttack(AttackController attackController, Unit unit) : base(attackController) {
		this.unit = unit;
		this.unit.pathRequestManager = Attacker.pathRequestManager;
	}

	public SpawnAttack(AttackController attackController, Unit unit, int count) : this(attackController, unit) {
		this.count = count;
	}

	public SpawnAttack(AttackController attackController, Unit unit, int count, float radius) : this(attackController, unit) {
		this.count = count;
		this.radius = radius;
	}

	public override void Use(Transform target) {
		Debug.Log("Spawn Attack");
		Vector3 spawnMidPoint = Attacker.transform.position;
		float pi2 = 2 * Mathf.PI;
		for (int i = 0; i < count; i++) {
			Vector3 pos = spawnMidPoint + new Vector3(Mathf.Cos(i * pi2 / count) * radius, 0, Mathf.Sin(i * pi2 / count) * radius);
			GameObject.Instantiate(unit, pos, Attacker.transform.rotation);
		}
	}
}
