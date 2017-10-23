using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PointTowardsCamera : MonoBehaviour {

	public Camera camera;

	private void Start() {
		if (camera == null) {
			camera = Camera.main;
		}
	}

	void LateUpdate () {
		transform.rotation = Quaternion.RotateTowards(transform.rotation, camera.transform.rotation, float.MaxValue);
	}
}
