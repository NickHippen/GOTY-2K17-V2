using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Unit : MonoBehaviour {

	const float minPathUpdateTime = 0.2f;
	const float pathUpdateMoveThreshold = 0.5f;

	public bool debugMode;

	public Transform target;
	public PathRequestManager pathRequestManager;
	public float speed = 5f;
	public float turnSpeed = 3;
	public float turnDist = 2;

	private Path path;

	void Start() {
		if (target != null) {
			BeginPathing();
		}
	}

	protected void Update() {
	}

	public void OnPathFound(Vector3[] waypoints, bool pathSuccessful) {
		if (pathSuccessful) {
			path = new Path(waypoints, transform.position, turnDist);
			StopCoroutine("FollowPath");
			StartCoroutine("FollowPath");
		}
	}

	public void BeginPathing() {
		StartCoroutine(UpdatePath());
	}

	IEnumerator UpdatePath() {

		if (Time.timeSinceLevelLoad < 0.3f) {
			yield return new WaitForSeconds(0.3f);
		}
		pathRequestManager.RequestPath(new PathRequest(transform.position, target.position, OnPathFound));

		float sqrMoveThreshold = pathUpdateMoveThreshold * pathUpdateMoveThreshold;
		Vector3 targetPosOld = target.position;

		while (true) {
			yield return new WaitForSeconds(minPathUpdateTime);
			if ((target.position - targetPosOld).sqrMagnitude > sqrMoveThreshold) {
				pathRequestManager.RequestPath(new PathRequest(transform.position, target.position, OnPathFound));
				targetPosOld = target.position;
			}
		}
	}

	IEnumerator FollowPath() {
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
		if (debugMode && path != null) {
			path.DrawWithGizmos();
		}
	}

}
