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
	public bool forceDestinationRadius = false;

	public bool atGoal;

	private Path path;

	public Animator UnitAnimator { get; set; }

	public float DefaultSpeed { get; set; }

	protected virtual void Start() {
		UnitAnimator = GetComponent<Animator>();
		DefaultSpeed = speed;

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
			if (HasLineOfSight(target)) {
				OnPathFound(new Vector3[] { target.position }, true);
				yield return new WaitForSeconds(minPathUpdateTime / 2); // Allow updating here twice as fast
			} else {
				yield return new WaitForSeconds(minPathUpdateTime);
				if ((target.position - targetPosOld).sqrMagnitude > sqrMoveThreshold) {
					pathRequestManager.RequestPath(new PathRequest(transform.position, target.position, OnPathFound));
					targetPosOld = target.position;
				}
			}
		}
	}

	protected virtual IEnumerator FollowPath() {
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
						if (!forceDestinationRadius) {
							followingPath = false;
						}
						break;
					} else {
						pathIndex++;
					}
				}

				Collider[] hitColliders = Physics.OverlapSphere(transform.position, destinationRadius);
				foreach (Collider collider in hitColliders) {
					if (collider.Equals(target.GetComponent<Collider>()) && HasLineOfSight(target.transform, destinationRadius)) { // Within range & has LoS
						followingPath = false;
						break;
					}
				}

				if (followingPath) {
					//Quaternion targetRotation = Quaternion.LookRotation((path.lookPoints[pathIndex]) - transform.position);
					////targetRotation.y = 0;
					//transform.rotation = Quaternion.Lerp(transform.rotation, targetRotation, Time.deltaTime * turnSpeed);
					//transform.Translate(Vector3.forward * Time.deltaTime * speed, Space.Self);
					MoveTowards(path.lookPoints[pathIndex]);
					atGoal = false;
				} else {
					atGoal = true;
				}
				if (UnitAnimator != null) {
					UnitAnimator.SetBool("Move", followingPath);
				}

				yield return null;
			}
		}
	}

	protected virtual void MoveTowards(Vector3 location) {
		Quaternion targetRotation = Quaternion.LookRotation(location - transform.position);
		transform.rotation = Quaternion.Lerp(transform.rotation, targetRotation, Time.deltaTime * turnSpeed);
		transform.Translate(Vector3.forward * Time.deltaTime * speed, Space.Self);
	}

	public bool HasLineOfSight(Transform targetTransform) {
		return HasLineOfSight(targetTransform, (target.position - transform.position).magnitude);
	}

	public bool HasLineOfSight(Transform targetTransform, float distance) {
		Vector3 rayDirection = targetTransform.position - transform.position;
		int layerMask = LayerMask.GetMask("Unwalkable", "Player"); // Only collisions in layer Unwalkable and Player
		RaycastHit hit;
		if (Physics.Raycast(transform.position + new Vector3(0, 1f, 0), rayDirection, out hit, distance, layerMask)) {
			return hit.transform == targetTransform;
		}
		Debug.Log("No LoS");
		return false;
	}

	protected virtual void UpdateAnimator() {
	}

	protected virtual void AnimationComplete(AnimationEvent animationEvent) {
		if (animationEvent.stringParameter.StartsWith("reset_")) {
			string animationName = animationEvent.stringParameter.Substring(6);
			if (UnitAnimator != null) {
				UnitAnimator.SetBool(animationName, false);
			}
		}
	}

	public abstract void OnRigTriggerEnter(Collider collider);

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
