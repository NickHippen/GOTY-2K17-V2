using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Attack {

	public AttackController Controller { get; set; }
	public AggressiveUnit Attacker { get; set; }

	public Attack(AttackController controller) {
		this.Controller = controller;
		this.Attacker = controller.Attacker;
	}

	public abstract void Use();

}
