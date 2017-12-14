using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossRoomTrigger : MonoBehaviour {

	public GameObject wall;

	private bool generated = false;

	//void Start() {
	//	GenerateWallsAround();
	//}

	private void GenerateWallsAround() {
		if (generated) {
			return;
		}
		Vector3 scaleRadius = transform.localScale / 2;
		Vector3 position = transform.position + new Vector3(scaleRadius.x, 0, 0);
		GameObject spawnedWall = Instantiate(wall, new Vector3(position.x + 0.5f, position.y, position.z), Quaternion.Euler(0, 90, 0)) as GameObject;
		spawnedWall.transform.localScale = new Vector3(transform.localScale.x, 12, 1);

		position = transform.position - new Vector3(scaleRadius.x, 0, 0);
		spawnedWall = Instantiate(wall, new Vector3(position.x - 0.5f, position.y, position.z), Quaternion.Euler(0, 90, 0)) as GameObject;
		spawnedWall.transform.localScale = new Vector3(transform.localScale.x, 12, 1);

		position = transform.position + new Vector3(0, 0, scaleRadius.y);
		spawnedWall = Instantiate(wall, new Vector3(position.x, position.y, position.z + 0.5f), Quaternion.Euler(0, 90, 0)) as GameObject;
		spawnedWall.transform.localScale = new Vector3(1, 12, transform.localScale.y);

		position = transform.position - new Vector3(0, 0, scaleRadius.y);
		spawnedWall = Instantiate(wall, new Vector3(position.x, position.y, position.z - 0.5f), Quaternion.Euler(0, 90, 0)) as GameObject;
		spawnedWall.transform.localScale = new Vector3(1, 12, transform.localScale.y);

		generated = true;
	}

	void OnTriggerEnter(Collider other) {
		if (other.tag == "Player") {
			GenerateWallsAround();
		}
	}

}
