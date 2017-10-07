using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileShooter : MonoBehaviour {

	public GameObject projectile;
	public Transform target;
	public float force = 1000f;

	void Update () {
		if (Input.GetKeyDown(KeyCode.Semicolon)) {
			Vector3 direction = target.position - transform.position;
			direction.Normalize();
			GameObject bullet = Instantiate(projectile, transform.position + direction, Quaternion.identity) as GameObject;
			bullet.GetComponent<Rigidbody>().AddForce(direction * force);
		}
	}

}
