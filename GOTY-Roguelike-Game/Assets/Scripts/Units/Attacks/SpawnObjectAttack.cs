using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnObjectAttack : Attack {

	private GameObject gameObject;
	private bool circle;

	public SpawnObjectAttack(AttackController attackController, GameObject gameObject, bool circle=false) : base(attackController) {
		this.gameObject = gameObject;
		attackController.RequireGoal = false;
		this.circle = circle;
	}

	public override void Use(Transform target) {
		if (target == null) {
			return;
		}
		Vector3 spawnMidPoint = target.position;
		if (!circle) {
			GameObject.Instantiate(gameObject, spawnMidPoint, gameObject.transform.rotation);
		} else {
			float radius = 5f;
			GameObject.Instantiate(gameObject, spawnMidPoint + new Vector3(radius, 0, 0), gameObject.transform.rotation);
			GameObject.Instantiate(gameObject, spawnMidPoint + new Vector3(-radius, 0, 0), gameObject.transform.rotation);
			GameObject.Instantiate(gameObject, spawnMidPoint + new Vector3(0, 0, radius), gameObject.transform.rotation);
			GameObject.Instantiate(gameObject, spawnMidPoint + new Vector3(0, 0, -radius), gameObject.transform.rotation);
			GameObject.Instantiate(gameObject, spawnMidPoint + new Vector3(radius, 0, radius), gameObject.transform.rotation);
			GameObject.Instantiate(gameObject, spawnMidPoint + new Vector3(-radius, 0, -radius), gameObject.transform.rotation);
			GameObject.Instantiate(gameObject, spawnMidPoint + new Vector3(radius, 0, -radius), gameObject.transform.rotation);
			GameObject.Instantiate(gameObject, spawnMidPoint + new Vector3(-radius, 0, radius), gameObject.transform.rotation);
		}
	}

}
