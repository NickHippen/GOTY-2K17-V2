using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KinematicProjectile : MonoBehaviour {

	public AggressiveUnit caster;
	public Transform target;
	public float speed = 0.3f;
	public float maxDuration = 3f;
	public bool seek = false;

	private float duration = 0f;
	private Vector3 targetPosition;
	private Vector3 vectorTowards;

	public PlayerCollision HandlePlayerCollision { get; set; }

	void LateUpdate() {
		if (target == null) {
			return;
		}
		if (!seek) {
			if (targetPosition == null || targetPosition.Equals(new Vector3(0,0,0))) {
				targetPosition = target.position + Vector3.up;
				this.vectorTowards = (targetPosition - this.transform.position).normalized * speed;
			}
		} else {
			targetPosition = target.position + Vector3.up;
			this.vectorTowards = (targetPosition - this.transform.position).normalized * speed;
		}
		this.transform.position += this.vectorTowards;

		this.duration += Time.deltaTime;
		CheckDuration();
	}

	private void CheckDuration() {
		if (duration >= maxDuration) {
			Destroy(this.gameObject);
		}
	}

	void OnCollisionEnter(Collision collision) {
		HealthManager health = collision.transform.GetComponent<HealthManager>();
		if (health != null) {
			HandlePlayerCollision(health);
		}
		Destroy(this.gameObject);
	}

}

public delegate void PlayerCollision(HealthManager health);