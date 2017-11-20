using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackCollider : RigCollider {
	
	protected override void Start () {
		base.Start();
	}

	protected override void OnTriggerEnter(Collider collider) {
		if (RootUnit is AggressiveUnit) {
			((AggressiveUnit)RootUnit).OnWeaponTriggerEnter(collider);
		}
	}

}
