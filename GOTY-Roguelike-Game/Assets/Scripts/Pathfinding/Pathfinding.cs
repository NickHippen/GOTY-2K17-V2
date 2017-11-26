using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System;
using UnityEngine;

public class Pathfinding : MonoBehaviour {

	Grid grid;

	void Awake() {
		grid = GetComponent<Grid>();
	}

	//public void StartFindPath(Vector3 startPos, Vector3 targetPos) {
	//	StartCoroutine(FindPath(startPos, targetPos));
	//}

	public void FindPath(PathRequest request, Action<PathResult> callback) {
		Vector3[] waypoints = new Vector3[0];
		bool pathSuccess = false;

		Node startNode = grid.FindNodeFromWorldPosition(request.pathStart);
		Node targetNode = grid.FindNodeFromWorldPosition(request.pathEnd);

		if (startNode.walkable && targetNode.walkable) {
			Heap<Node> openSet = new Heap<Node>(grid.MaxSize);
			HashSet<Node> closedSet = new HashSet<Node>();
			openSet.Add(startNode);

			while (openSet.Count > 0) {
				Node currentNode = openSet.RemoveFirst();
				closedSet.Add(currentNode);

				if (currentNode == targetNode) {
					pathSuccess = true;
					break;
				}

				foreach (Node neighbor in grid.GetNeighbors(currentNode)) {
					if (!neighbor.walkable || closedSet.Contains(neighbor)) {
						continue;
					}

					int newMovementCostToNeighbor = currentNode.gCost + GetDistance(currentNode, neighbor) + neighbor.movementPenalty;
					if (newMovementCostToNeighbor < neighbor.gCost || !openSet.Contains(neighbor)) {
						neighbor.gCost = newMovementCostToNeighbor;
						neighbor.hCost = GetDistance(neighbor, targetNode);
						neighbor.parent = currentNode;

						if (!openSet.Contains(neighbor)) {
							openSet.Add(neighbor);
						} else {
							openSet.UpdateItem(neighbor);
						}
					}
				}
			}
		}
		if (pathSuccess) {
			waypoints = RetracePath(startNode, targetNode);
		}
		//waypoints[waypoints.Length - 1] = request.pathEnd;
		callback(new PathResult(waypoints, pathSuccess, request.callback));
	}

	Vector3[] RetracePath(Node startNode, Node endNode) {
		List<Node> path = new List<Node>();
		Node currentNode = endNode;

		while (currentNode != startNode) {
			path.Add(currentNode);
			currentNode = currentNode.parent;
		}
		Vector3[] waypoints = SimplifyPath(path);
		Array.Reverse(waypoints);
		return waypoints;
	}

	Vector3[] SimplifyPath(List<Node> path) {
		List<Vector3> waypoints = new List<Vector3>();
		Vector2 directionOld = Vector2.zero;

		for (int i = 1; i < path.Count; i++) {
			Vector2 directionNew = new Vector2(path[i - 1].gridX - path[i].gridX, path[i - 1].gridY - path[i].gridY);
			if (directionNew != directionOld) {
				waypoints.Add(path[i - 1].worldPosition);
			}
			directionOld = directionNew;
		}
		return waypoints.ToArray();
		//return SmoothConnections(waypoints);
	}

	Vector3[] SmoothConnections(List<Vector3> waypoints) {
		froms = new List<Vector3>();
		fromDirs = new List<Vector3>();
		if (waypoints.Count <= 2) {
			return waypoints.ToArray();
		}
		List<Vector3> finalWaypoints = new List<Vector3> {
			waypoints[0]
		};

		int inputIndex = 2;

		while (inputIndex < waypoints.Count - 1) {
			if (!IsDirectPathClear(finalWaypoints[finalWaypoints.Count - 1], waypoints[inputIndex])) {
				UnityEngine.Debug.Log("Path not clear");
				finalWaypoints.Add(waypoints[inputIndex]);
			}
			inputIndex++;
		}
		finalWaypoints.Add(waypoints[waypoints.Count - 1]);
		return finalWaypoints.ToArray();
	}

	private List<Vector3> froms = new List<Vector3>();
	private List<Vector3> fromDirs = new List<Vector3>();

	private bool IsDirectPathClear(Vector3 from, Vector3 to) {
		float range = Vector3.Distance(from, to);
		//Vector3 direction = Vector3.RotateTowards(from, to, 99999, 99999);
		Vector3 direction = to - from;
		UnityEngine.Debug.Log(direction);
		int layerMask = LayerMask.GetMask("Unwalkable"); // Only collisions in layer Unwalkable
		RaycastHit hit;
		froms.Add(from);
		fromDirs.Add(from + direction);
		if (Physics.Raycast(from, direction, out hit, range, layerMask)) {
			// Hit
			UnityEngine.Debug.Log("Hit " + hit.transform.name);
			return false;
		} else {
			// Miss
			return true;
		}
	}

	void OnDrawGizmos() {
		for (int i = 0; i < froms.Count; i++) {
			Gizmos.color = Color.cyan;
			Gizmos.DrawSphere(froms[i], 1f);
			Gizmos.color = Color.green;
			Gizmos.DrawCube(fromDirs[i], Vector3.one * 1.5f);
		}
	}

	int GetDistance(Node nodeA, Node nodeB) {
		int distX = Mathf.Abs(nodeA.gridX - nodeB.gridX);
		int distY = Mathf.Abs(nodeA.gridY - nodeB.gridY);

		if (distX > distY) {
			return 14 * distY + 10 * (distX - distY);
		} else {
			return 14 * distX + 10 * (distY - distX);
		}
	}

}
