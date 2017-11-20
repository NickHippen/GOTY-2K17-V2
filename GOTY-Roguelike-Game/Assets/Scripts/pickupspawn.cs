using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class pickupspawn : MonoBehaviour {

	public float speed;
	Vector3 target;
	// Use this for initialization
	void Start () {
		target = new Vector3 (this.transform.position.x + Random.Range (-1f, 1f), 2.5f + Random.Range (-1f, 1f), +this.transform.position.z + Random.Range (-1f, 1f));
	}
	
	// Update is called once per frame
	void Update () {
		float step = speed * Time.deltaTime;
		this.transform.position = Vector3.MoveTowards (this.transform.position, target, step);
	}
}
