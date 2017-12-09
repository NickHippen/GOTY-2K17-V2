using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class AggressiveUnit : LivingUnit {

	[TagSelector]
	public string targetTag = "Player";
	public float aggroRadius = 20f;

	public List<Attack> attacks = new List<Attack>();

	public float attackPower = 5f;

	public SoundData sfx;

	public bool HasWeapon { get; set; }

	protected override void Start() {
		base.Start();
		ApplyAttackBehavior();
	}

	protected virtual new void Update() {
		base.Update();
		if (target == null) { // Only search if no target already
			CheckAggro();
		}

		UpdateAttacks();
	}

	private void CheckAggro() {
		Collider[] hitColliders = Physics.OverlapSphere(transform.position, aggroRadius);
		foreach (Collider collider in hitColliders) {
			if (collider.tag.Equals(targetTag)) {
				target = collider.transform;
				BeginPathing();
				if (sfx != null && sfx.soundEffects.Count > 0) {
					sfx.playSound ();
				}
				break;
			}
		}
	}

	private void UpdateAttacks() {
		if (!IsStunned()) {
			foreach (Attack attack in attacks) {
				if ((!atGoal && attack.Controller.RequireGoal) // Does the controller require a goal?
						|| (!Living && !(attack.Controller is DeathAttackController))) { // Is the monster dead?
					continue;
				}
				if (attack.Controller.Check()) {
					attack.Use(target);
				}
			}
		}
	}

	protected virtual new void OnDrawGizmosSelected() {
		base.OnDrawGizmosSelected();
		Gizmos.color = Color.yellow;
		Gizmos.DrawWireSphere(transform.position, aggroRadius);
	}

	protected abstract void ApplyAttackBehavior();

	protected override void UpdateAnimator() {
		base.UpdateAnimator();
	}

	public float CalculateAttackPower() {
		return attackPower;
	}

	public override void OnRigTriggerEnter(Collider collider) {
		if (HasWeapon) { // Damage is done by weapon collider if it exists
			return;
		}
		AttackCollision(collider);
	}

	public void OnWeaponTriggerEnter(Collider collider) {
		AttackCollision(collider);
	}

	private void AttackCollision(Collider collider) {
		if (collider.gameObject.tag == "Player") {
			HealthManager healthManager = collider.gameObject.GetComponent<HealthManager>();
			if (healthManager == null) {
				return;
			}
			if (this.UnitAnimator.GetBool("Attack")) {
				healthManager.Damage(CalculateAttackPower());
			} else if (this.UnitAnimator.GetBool("SpecialAttack")) {
				healthManager.Damage(CalculateAttackPower()); // TODO Possibly add damage modifiers for special attacks
			}
		}
	}

}
