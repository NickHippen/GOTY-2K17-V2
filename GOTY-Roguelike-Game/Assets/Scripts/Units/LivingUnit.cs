using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public abstract class LivingUnit : Unit {

	public float health = 100;
	public float maxHealth = 100;

	public float canvasHeight = 0.03f;

	private bool destroying;
	private GameObject monsterCanvas;
	private Image healthBar;

	private List<Status> statuses = new List<Status>();

	public virtual float Health {
		get {
			return health;
		}
		set {
			health = value;
			if (health <= 0) {
				health = 0;
				UnitAnimator.SetBool("Dead", true);
				OnDeath();
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

	protected override void Start() {
		base.Start();
		monsterCanvas = (GameObject)Instantiate(Resources.Load("MonsterCanvas"));
		monsterCanvas.transform.SetParent(this.transform);
		monsterCanvas.transform.position = this.transform.position;
		healthBar = monsterCanvas.transform.GetChild(0).GetChild(0).GetComponent<Image>();
		ApplyStatus(new StatusStun(this, 20f));
	}

	protected override void Update() {
		if (Living) {
			base.Update();
		}
		monsterCanvas.GetComponent<RectTransform>().localPosition = new Vector3(0, canvasHeight, 0);
		UpdateStatuses();
	}

	public void ApplyStatus(Status status) {
		for (int i = statuses.Count - 1; i >= 0; i--) {
			Status existingStatus = statuses[i];
			if (existingStatus.GetType().Equals(status.GetType())) {
				statuses.RemoveAt(i);
				break;
			}
		}
		statuses.Add(status);
	}

	void UpdateStatuses() {
		for (int i = 0; i < healthBar.transform.childCount; i++) {
			healthBar.transform.GetChild(i).gameObject.SetActive(false);
		}
		for (int i = statuses.Count - 1; i >= 0; i--) {
			Status status = statuses[i];
			healthBar.transform.GetChild(status.IconIndex()).gameObject.SetActive(true);
			if (status.IsActive()) {
				status.Update();
			} else {
				status.OnExpire();
				statuses.RemoveAt(i);
			}
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

	protected override IEnumerator FollowPath() {
		if (Living) {
			return base.FollowPath();
		}
		return null;
	}

}
