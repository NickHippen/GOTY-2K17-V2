using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class LivingUnit : Unit {

	public virtual float Health {
		get {
			return Health;
		}
		set {
			Health = value;
			if (Health <= 0) {
				Health = 0;
			} else {
				if (Health > MaxHealth) {
					Health = MaxHealth;
				}
			}
		}
	}

	public float MaxHealth {
		get {
			return MaxHealth;
		}
		private set {
			if (value <= 0) {
				throw new ArgumentException("Max health must be > 0");
			}
			MaxHealth = value;
		}
	}

	public bool Living {
		get {
			return Health >= 0;
		}
	}

	public float HealthPercentage {
		get {
			return Health / MaxHealth;
		}
	}

	public void Damage(float amount) {
		Health -= amount;
	}

	public void Heal(float amount) {
		Health += amount;
	}

}
