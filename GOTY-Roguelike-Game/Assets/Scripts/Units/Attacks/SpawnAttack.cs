using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnAttack : Attack {

	private Unit unit;
	private int count = 1;
	private float radius = 10f;
	private bool onTarget = false;
	private OnUnitSpawn spawnSetup;

	public SpawnAttack(AttackController attackController, Unit unit) : base(attackController) {
		this.unit = unit;
		this.unit.pathRequestManager = Attacker.pathRequestManager;
	}

	public SpawnAttack(AttackController attackController, Unit unit, int count) : this(attackController, unit) {
		this.count = count;
	}

	public SpawnAttack(AttackController attackController, Unit unit, int count, bool onTarget, OnUnitSpawn spawnSetup) : this(attackController, unit) {
		this.count = count;
		this.radius = 0f;
		this.onTarget = true;
		this.spawnSetup = spawnSetup;
	}

	public SpawnAttack(AttackController attackController, Unit unit, int count, float radius) : this(attackController, unit) {
		this.count = count;
		this.radius = radius;
	}

	public override void Use(Transform target) {
		Vector3 spawnMidPoint;
		if (onTarget) {
			spawnMidPoint = target.position;
		} else {
			spawnMidPoint = Attacker.transform.position;
		}
		float pi2 = 2 * Mathf.PI;
		for (int i = 0; i < count; i++) {
			Vector3 pos = spawnMidPoint + new Vector3(Mathf.Cos(i * pi2 / count) * radius, 0, Mathf.Sin(i * pi2 / count) * radius);
			Unit spawnedUnit = GameObject.Instantiate(unit, pos, Attacker.transform.rotation);
			spawnedUnit.target = Attacker.target;
			if (spawnSetup != null) {
				spawnSetup(spawnedUnit);
			}
		}
	}
}

public delegate void OnUnitSpawn(Unit unit);