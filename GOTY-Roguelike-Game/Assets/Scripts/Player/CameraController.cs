using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour {

	public bool lockMouse = true;
	public Transform lookAt;
	public Vector3 relativePos;
	public float cameraHeight = 5f;

	//private Camera cam;
	private Transform camTransform;
	public float currentZoom = 3f;

	private float currentX = 0f;
	private float currentY = 5f;
	private float yAngleMin = -30f;
	private float yAngleMax = 30f;
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
		//if (Input.GetMouseButton(1)) {
		//	relativePos = Vector3.MoveTowards(relativePos, new Vector3(-0.5f, 2f, 0), 0.5f);
		//	currentZoom = Vector2.MoveTowards(new Vector2(currentZoom, 0), new Vector2(2, 0), 0.5f).x;
		//	cameraHeight = 0f;
		//	yAngleMin = 0f;
		//	yAngleMax = 60f;
		//} else {
		//	SetDefaults();
		//}

		currentY = Mathf.Clamp(currentY, yAngleMin, yAngleMax);
	}

	void SetDefaults() {
		relativePos = new Vector3(0, 2f, 0);
		currentZoom = 5f;
		yAngleMin = -30f;
		yAngleMax = 30f;
	}

	void LateUpdate () {
		if (lookAt == null) {
			return;
		}
		Vector3 dir = new Vector3(0, cameraHeight, -currentZoom);
		Quaternion rotation = Quaternion.Euler(currentY, currentX, 0);
		camTransform.position = lookAt.position + rotation * dir;

		// Face player
		camTransform.LookAt(lookAt.position + relativePos);
	}

	void OnDrawGizmos() {
		if (lookAt == null) {
			return;
		}
		Gizmos.DrawCube(lookAt.position + relativePos, Vector3.one / 10);
	}

}
