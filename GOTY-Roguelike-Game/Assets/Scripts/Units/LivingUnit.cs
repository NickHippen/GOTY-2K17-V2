using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Random = UnityEngine.Random;

public abstract class LivingUnit : Unit {

	public float health = 100;
	public float maxHealth = 100;

	public float canvasHeight = 0.03f;

	protected bool destroying;
	private GameObject monsterCanvas;
	private Image healthBar;

	private List<Status> statuses = new List<Status>();

	public GameObject healthDrop;
	public GameObject gemDrop;
    public int DropIncrease { get; set; }

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
	}

	protected override void Update() {
		if (Living) {
			base.Update();
		}
		monsterCanvas.GetComponent<RectTransform>().localPosition = new Vector3(0, canvasHeight, 0);
		UpdateStatuses();
	}

	public void ApplyStatus(Status status, bool overrideExisting) {
		for (int i = statuses.Count - 1; i >= 0; i--) {
			Status existingStatus = statuses[i];
			if (existingStatus.GetType().Equals(status.GetType())) {
				if (overrideExisting) {
					statuses.RemoveAt(i);
					break;
				} else {
					return;
				}
			}
		}
		statuses.Add(status);
	}

	public void ApplyStatus(Status status) {
		ApplyStatus(status, true);
	}

    public void ApplyKnockback(float distance)
    {
        RaycastHit hit;
        int layerMask = LayerMask.GetMask("Unwalkable", "Ground");
        if (Physics.Raycast(this.transform.position, -this.transform.forward, out hit, distance, layerMask))
        {
            this.transform.position = hit.point;
        }
        else
        {
            this.transform.position -= this.transform.forward * distance;
        }
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

	public bool IsStunned() {
		foreach (Status status in statuses) {
			if (status is StatusStun) {
				return true;
			}
		}
		return false;
	}

	public virtual void Damage(float amount) {
		foreach (Status status in statuses) {
			if (status is StatusVulnerable) {
				amount *= ((StatusVulnerable)status).Multiplier;
				break;
			}
		}
		Health -= amount;
	}

	public virtual void Damage(float amount, Transform attacker) {
		Damage(amount);
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

		if (healthDrop != null) {
			int dropped = Random.Range (0, 8);
			if (dropped == 0) {
				GameObject instance = Instantiate (healthDrop, new Vector3 (this.transform.position.x, this.transform.position.y, this.transform.position.z), Quaternion.identity) as GameObject;
			}
		}

		if (gemDrop != null) {
			int maxamt = Random.Range (1, 5 + DropIncrease);
			for (int i = 0; i < maxamt; i++) {
				GameObject instance = Instantiate (gemDrop, new Vector3 (this.transform.position.x, this.transform.position.y, this.transform.position.z), Quaternion.identity) as GameObject;
			}
		}
	}

	protected override IEnumerator FollowPath() {
		if (Living && speed > 0) {
			return base.FollowPath();
		}
		UnitAnimator.SetBool("Move", false);
		return null;
	}

}
