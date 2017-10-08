using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class LivingUnit : Unit {

	private bool living;
	public float health;
	public float maxHealth;

	public bool Living { get; set; }

	public virtual float Health {
		get {
			return health;
		}
		set {
			health = value;
			if (health <= 0) {
				health = 0;
				Living = false;
			} else {
				Living = true;
				if (health > MaxHealth) {
					health = MaxHealth;
				}
			}
		}
	}

	public float MaxHealth {
		get {
			return maxHealth;
		}
		private set {
			if (value <= 0) {
				throw new ArgumentException("Max health must be > 0");
			}
			maxHealth = value;
		}
	}

	public void Damage(float amount) {
		Health -= amount;
	}

	public void Heal(float amount) {
		Health += amount;
	}

	public float GetHealthPercentage() {
		return Health / MaxHealth;
	}

}
