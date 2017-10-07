using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Attack {

	public AttackController controller;

	public Attack(AttackController controller) {
		this.controller = controller;
	}

	public abstract void Use();

}
