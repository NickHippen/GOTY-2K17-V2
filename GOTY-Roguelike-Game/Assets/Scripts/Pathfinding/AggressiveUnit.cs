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

}
