using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CloudSpawn : Unit {

	public float damage = 1f;
	public float ticksPerSecond = 5f;

	private float timeSince = 0f;

	public override void OnRigTriggerEnter(Collider collider) {
	}

	protected override void MoveTowards(Vector3 location) {
		//Vector3 direction = location - transform.position;
		//transform.Translate(direction.normalized * Time.deltaTime * speed, Space.Self);
		transform.position = Vector3.MoveTowards(transform.position, location, speed * Time.deltaTime);
		transform.rotation = Quaternion.identity;
	}

	void OnTriggerStay(Collider other) {
		HealthManager playerHealth = other.transform.GetComponent<HealthManager>();
		if (playerHealth == null) {
			return;
		}
		timeSince += Time.deltaTime;
		if (timeSince >= 1 / ticksPerSecond) {
			playerHealth.Damage(damage);
			timeSince = 0f;
		}
	}

}
