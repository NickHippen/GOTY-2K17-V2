using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ImplosionPillar : MonoBehaviour {

	public float currentRadius;
	public float shrinkRate;
	public float damage;
	public float damageRadius;

	private ParticleSystem particles;
	private bool destroying = false;

	private void Start() {
		this.particles = GetComponent<ParticleSystem>();
	}

	void Update () {
		var shape = particles.shape;
		if (shape.radius >= 0) {
			shape.radius -= shrinkRate * Time.deltaTime;
			currentRadius = shape.radius;
		} else if (!destroying) {
			Explode();
		}
	}

	void Explode() {
		Collider[] hitColliders = Physics.OverlapSphere(transform.position, damageRadius);
		foreach (Collider collider in hitColliders) {
			if (collider.tag.Equals("Player")) {
				collider.GetComponent<HealthManager>().Damage(damage);
			}
		}

		destroying = true;
		Destroy(transform.parent.gameObject, 2f);

	}

	void OnDrawGizmos() {
		Gizmos.color = Color.red;
		Gizmos.DrawWireSphere(transform.position, damageRadius);
	}


}
