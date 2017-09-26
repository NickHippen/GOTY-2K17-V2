using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Unit : MonoBehaviour {

	const float minPathUpdateTime = 0.2f;
	const float pathUpdateMoveThreshold = 0.5f;

	public Transform target;
	public float speed = 5f;
	public float turnSpeed = 3;
	public float turnDist = 2;

	Path path;
	//Vector3[] path;
	//int targetIndex;

	void Start() {
		StartCoroutine(UpdatePath());
		//PathRequestManager.RequestPath(transform.position, target.position, OnPathFound);
	}

	public void OnPathFound(Vector3[] waypoints, bool pathSuccessful) {
		if (pathSuccessful) {
			//path = newPath;
			path = new Path(waypoints, transform.position, turnDist);
			//targetIndex = 0;
			StopCoroutine("FollowPath");
			StartCoroutine("FollowPath");
		}
	}

	IEnumerator UpdatePath() {

		if (Time.timeSinceLevelLoad < 0.3f) {
			yield return new WaitForSeconds(0.3f);
		}
		//PathRequestManager.RequestPath(transform.position, target.position, OnPathFound);
		PathRequestManager.RequestPath(new PathRequest(transform.position, target.position, OnPathFound));

		float sqrMoveThreshold = pathUpdateMoveThreshold * pathUpdateMoveThreshold;
		Vector3 targetPosOld = target.position;

		while (true) {
			yield return new WaitForSeconds(minPathUpdateTime);
			if ((target.position - targetPosOld).sqrMagnitude > sqrMoveThreshold) {
				//PathRequestManager.RequestPath(transform.position, target.position, OnPathFound);
				PathRequestManager.RequestPath(new PathRequest(transform.position, target.position, OnPathFound));
				targetPosOld = target.position;
			}
		}
	}

	IEnumerator FollowPath() {
		//Vector3 currentWaypoint = path[0];

		//while (true) {
		//	if (transform.position == currentWaypoint) {
		//		targetIndex++;
		//		if (targetIndex >= path.Length) {
		//			targetIndex = 0;
		//			path = new Vector3[0];
		//			yield break;
		//		}
		//		currentWaypoint = path[targetIndex];
		//	}
		//	transform.position = Vector3.MoveTowards(transform.position, currentWaypoint, speed * Time.deltaTime);
		//	yield return null;
		//}
		bool followingPath = true;
		int pathIndex = 0;
		transform.LookAt(path.lookPoints[0]);

		while (followingPath) {
			Vector2 pos2D = new Vector2(transform.position.x, transform.position.z);
			while (path.turnBoundaries[pathIndex].HasCrossedLine(pos2D)) {
				if (pathIndex == path.finishLineIndex) {
					followingPath = false;
					break;
				} else {
					pathIndex++;
				}
			}

			if (followingPath) {
				Quaternion targetRotation = Quaternion.LookRotation(path.lookPoints[pathIndex] - transform.position);
				transform.rotation = Quaternion.Lerp(transform.rotation, targetRotation, Time.deltaTime * turnSpeed);
				transform.Translate(Vector3.forward * Time.deltaTime * speed, Space.Self);
			}

			yield return null;
		}
	}

	public void OnDrawGizmos() {
		if (path != null) {
			path.DrawWithGizmos();
			//for (int i = targetIndex; i < path.Length; i++) {
			//	Gizmos.color = Color.black;
			//	Gizmos.DrawCube(path[i], Vector3.one);

			//	if (i == targetIndex) {
			//		Gizmos.DrawLine(transform.position, path[i]);
			//	} else {
			//		Gizmos.DrawLine(path[i - 1], path[i]);
			//	}
			//}
		}
	}

}
