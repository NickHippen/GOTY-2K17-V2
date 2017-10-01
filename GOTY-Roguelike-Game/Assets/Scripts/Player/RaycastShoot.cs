using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RaycastShoot : MonoBehaviour {

	public float fireRate = 0.25f;
	public float weaponRange = 50f;
	public float hitForce = 100f;
	public Transform raycastPoint;
	public Camera raycastCamera;

	private WaitForSeconds shotDuration = new WaitForSeconds(0.07f);
	private LineRenderer laserLine;
	private float nextFire;

	void Start() {
		laserLine = GetComponent<LineRenderer>();
	}

	void Update() {
		if (Input.GetButtonDown("Fire1") && Time.time > nextFire) {
			nextFire = Time.time + fireRate;

			StartCoroutine(ShotEffect());

			Vector3 rayOrigin = raycastCamera.ViewportToWorldPoint(new Vector3(1.5f, 1.5f, 0));
			RaycastHit hit;

			laserLine.SetPosition(0, raycastPoint.position);

			if (Physics.Raycast(rayOrigin, raycastCamera.transform.forward, out hit, weaponRange)) {
				laserLine.SetPosition(1, hit.point);

				if (hit.rigidbody != null) {
					hit.rigidbody.AddForce(-hit.normal * hitForce);
				}
			} else {
				laserLine.SetPosition(1, rayOrigin + (raycastCamera.transform.forward * weaponRange));
			}
		}
	}

	private IEnumerator ShotEffect() {
		laserLine.enabled = true;
		yield return shotDuration;
		laserLine.enabled = false;
	}

}
