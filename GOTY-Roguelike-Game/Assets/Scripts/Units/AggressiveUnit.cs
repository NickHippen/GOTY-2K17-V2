using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class AggressiveUnit : LivingUnit {

	[TagSelector]
	public string targetTag;
	public float aggroRadius = 20f;

	public List<Attack> attacks = new List<Attack>();

	public float attackPower = 5f;

	protected new void Start() {
		base.Start();
		ApplyAttackBehavior();
	}

	protected virtual new void Update() {
		base.Update();
		if (target == null) { // Only search if no target already
			CheckAggro();
		} else if (atGoal) { // Has target & at goal
			UpdateAttacks();
		}
	}

	private void CheckAggro() {
		Collider[] hitColliders = Physics.OverlapSphere(transform.position, aggroRadius);
		foreach (Collider collider in hitColliders) {
			if (collider.tag.Equals(targetTag)) {
				target = collider.transform;
				BeginPathing();
				break;
			}
		}
	}

	private void UpdateAttacks() {
		foreach (Attack attack in attacks) {
			if (attack.Controller.Check()) {
				attack.Use();
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

	public override void OnRigCollisionEnter(Collision collision) {
		if (this.UnitAnimator.GetBool("Attack") && collision.gameObject.tag == "Player") {
			HealthManager healthManager = collision.gameObject.GetComponent<HealthManager>();
			if (healthManager == null) {
				return;
			}
			Debug.Log("Damage");
			healthManager.Damage(CalculateAttackPower());
		}
	}

}
