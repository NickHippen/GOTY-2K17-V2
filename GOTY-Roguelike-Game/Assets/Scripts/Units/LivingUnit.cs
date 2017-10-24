using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public abstract class LivingUnit : Unit {

	public float health = 100;
	public float maxHealth = 100;

	private bool destroying;
	public Image healthBar;

	protected override void Start() {
		base.Start();
	}

	public virtual float Health {
		get {
			return health;
		}
		set {
			health = value;
			if (health <= 0) {
				health = 0;
			} else {
				if (health > MaxHealth) {
					health = MaxHealth;
				}
			}
			healthBar.fillAmount = HealthPercentage;
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

	public bool Living {
		get {
			return Health > 0;
		}
	}

	public float HealthPercentage {
		get {
			return Health / MaxHealth;
		}
	}

	protected override void Update() {
		base.Update();
		UnitAnimator.SetBool("Dead", !Living);
		if (!Living) {
			OnDeath();
		}
	}

	public void Damage(float amount) {
		Health -= amount;
	}

	public void Heal(float amount) {
		Health += amount;
	}

	public virtual void OnDeath() {
		if (destroying) {
			return; // Already destroying
		}
		destroying = true;
		Destroy(gameObject, 5f);
	}

}
