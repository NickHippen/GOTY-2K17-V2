using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour {

	private const float Y_ANGLE_MIN = -30f;
	private const float Y_ANGLE_MAX = 30f;
	private const float ZOOM_MIN = 2f;
	private const float ZOOM_MAX = 5f;

	public bool lockMouse = true;
	public Transform lookAt;

	//private Camera cam;
	private Transform camTransform;

	private float currentX = 0f;
	private float currentY = 0f;
	private float currentZoom = 200f;
	//private float sensitivityX = 4f;
	//private float sensitivityY = 1f;

	void Start () {
		if (lockMouse) {
			Cursor.visible = false;
			Cursor.lockState = CursorLockMode.Locked;
		}

		camTransform = transform;
		//cam = Camera.main;
	}

	private void Update() {
		currentX += Input.GetAxis("Mouse X");
		currentY -= Input.GetAxis("Mouse Y");
		//currentZoom -= Input.GetAxis("Mouse ScrollWheel") * sensitivityZoom;

		currentY = Mathf.Clamp(currentY, Y_ANGLE_MIN, Y_ANGLE_MAX);
		currentZoom = Mathf.Clamp(currentZoom, ZOOM_MIN, ZOOM_MAX);
	}

	void LateUpdate () {
		Vector3 dir = new Vector3(0, 1, -currentZoom);
		Quaternion rotation = Quaternion.Euler(currentY, currentX, 0);
		camTransform.position = lookAt.position + rotation * dir;

		// Face player
		camTransform.LookAt(lookAt.position);
	}
}
