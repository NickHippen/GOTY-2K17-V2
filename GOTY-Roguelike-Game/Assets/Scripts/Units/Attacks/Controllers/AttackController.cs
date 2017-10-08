using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class AttackController {

	private readonly List<AttackPredicate> conditionals = new List<AttackPredicate>();

	public List<AttackPredicate> Conditionals {
		get {
			return conditionals;
		}
	}

	public AttackController AddConditional(AttackPredicate conditional) {
		Conditionals.Add(conditional);
		return this;
	}

	/**
	 * <summary>Determines whether or not this controller should be triggered by conditionals, then checks with AttackController#CheckSpecifics</summary>
	 * <returns>whether or not the controller should be triggered</returns>
	 * <remarks>Assumes the check is called every frame</remarks>
	 * <seealso cref="CheckSpecifics"/>
	 */
	public bool Check() {
		foreach (AttackPredicate conditional in conditionals) {
			if (!conditional()) {
				return false;
			}
		}
		return CheckSpecifics();
	}

	/**
	 * <summary>Abstract method to determine whether or not this controller should be triggered, specific to the subclass implementation</summary>
	 * <returns>whether or not the controller should be triggered</returns>
	 * <remarks>Assumes the check is called every frame</remarks>
	 */
	protected abstract bool CheckSpecifics();
	
}

public delegate bool AttackPredicate();