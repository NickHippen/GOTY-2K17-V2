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

	public Animator UnitAnimator { get; set; }

	protected virtual void Start() {
		UnitAnimator = GetComponent<Animator>();

		if (target != null) {
			BeginPathing();
		}
	}

	protected virtual void Update() {
		if (atGoal) {
			// Turn towards target
			// Find the vector pointing from our position to the target
			Vector3 direction = (target.position - transform.position).normalized;
			direction.y = 0;

			// Create the rotation we need to be in to look at the target
			Quaternion lookRotation = Quaternion.LookRotation(direction);

			// Rotate over time according to speed until we are in the required rotation
			transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, Time.deltaTime * turnSpeed);
		}
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
					//targetRotation.y = 0;
					transform.rotation = Quaternion.Lerp(transform.rotation, targetRotation, Time.deltaTime * turnSpeed);
					transform.Translate(Vector3.forward * Time.deltaTime * speed, Space.Self);
					atGoal = false;
				} else {
					atGoal = true;
				}

				UnitAnimator.SetBool("Move", followingPath);

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

	protected virtual void UpdateAnimator() {
	}

	protected virtual void AnimationComplete(AnimationEvent animationEvent) {
		if (animationEvent.stringParameter.StartsWith("reset_")) {
			string animationName = animationEvent.stringParameter.Substring(6);
			UnitAnimator.SetBool(animationName, false);
		}
	}

	public abstract void OnRigCollisionEnter(Collision collision);

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
