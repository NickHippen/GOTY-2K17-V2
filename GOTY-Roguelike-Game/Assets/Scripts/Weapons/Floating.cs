using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Floating : MonoBehaviour {

	public float maxSpeed = 1.5f;
	private Vector3 startPos;

	// Use this for initialization
	void Start () {
		startPos = transform.position;
	}

	// Update is called once per frame
	void Update () {
		MoveVertical ();
	}

	void MoveVertical(){
		transform.position = new Vector3(transform.position.x, Mathf.Abs(Mathf.Sin(Time.time * maxSpeed))/2 + startPos.y, transform.position.z);
		transform.Rotate (0, 50 * Time.deltaTime, 0);
	}
}

