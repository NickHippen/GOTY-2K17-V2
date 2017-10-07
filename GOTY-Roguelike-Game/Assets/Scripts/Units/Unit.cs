using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Unit : MonoBehaviour {

	const float minPathUpdateTime = 0.2f;
	const float pathUpdateMoveThreshold = 0.5f;

	public bool debugMode;

	public Transform target;
	public PathRequestManager pathRequestManager;
	public float speed = 5f;
	public float turnSpeed = 4f;
	public float turnDist = 1f;
	public float destinationRadius = 1f;

	public bool atGoal;

	private Path path;

	protected void Start() {
		if (target != null) {
			BeginPathing();
		}
	}

	protected virtual void Update() {
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
		if (path.lookPoints.Length > 0) {
			transform.LookAt(path.lookPoints[0]);
		}
		if (path.turnBoundaries.Length > 0) {
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

				Collider[] hitColliders = Physics.OverlapSphere(transform.position, destinationRadius);
				foreach (Collider collider in hitColliders) {
					if (collider.Equals(target.GetComponent<Collider>()) && HasLineOfSight(target.transform)) { // Within range & has LoS
						followingPath = false;
						break;
					}
				}

				if (followingPath) {
					Quaternion targetRotation = Quaternion.LookRotation(path.lookPoints[pathIndex] - transform.position);
					transform.rotation = Quaternion.Lerp(transform.rotation, targetRotation, Time.deltaTime * turnSpeed);
					transform.Translate(Vector3.forward * Time.deltaTime * speed, Space.Self);
					atGoal = false;
				} else {
					atGoal = true;
				}

				yield return null;
			}
		}
	}

	public bool HasLineOfSight(Transform targetTransform) {
		Vector3 rayDirection = targetTransform.position - transform.position;
		RaycastHit hit;
		if (Physics.Raycast(transform.position, rayDirection, out hit, destinationRadius)) { // Note: Currently will be blocked by ALL colldiers (not just unwalkable)
			return hit.transform == targetTransform;
		}
		return false;
	}

	protected virtual void OnDrawGizmos() {
		if (debugMode && path != null) {
			path.DrawWithGizmos();
		}
	}

	protected virtual void OnDrawGizmosSelected() {
		Gizmos.color = Color.red;
		Gizmos.DrawWireSphere(transform.position, destinationRadius);
	}

}
