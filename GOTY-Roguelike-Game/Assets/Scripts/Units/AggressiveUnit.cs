using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AggressiveUnit : Unit {

	[TagSelector]
	public string targetTag;
	public float aggroRadius = 20f;

	void OnDrawGizmosSelected() {
		Gizmos.color = Color.yellow;
		Gizmos.DrawWireSphere(transform.position, aggroRadius);
	}

	protected new void Update() {
		base.Update();
		if (target == null) { // Only search if no target already
			Collider[] hitColliders = Physics.OverlapSphere(transform.position, aggroRadius);
			foreach (Collider collider in hitColliders) {
				if (collider.tag.Equals(targetTag)) {
					target = collider.transform;
					BeginPathing();
					break;
				}
			}
		}
	}

}
