using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Attack {

	private AttackController controller;

	public Attack(AttackController controller) {
		this.Controller = controller;
	}

	public AttackController Controller { get; set; }

	public abstract void Use();

}
