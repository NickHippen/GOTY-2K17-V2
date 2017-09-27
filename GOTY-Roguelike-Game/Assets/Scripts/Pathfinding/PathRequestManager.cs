using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;

public class PathRequestManager : MonoBehaviour {

	//Queue<PathRequest> pathRequestQueue = new Queue<PathRequest>();
	//PathRequest currentPathRequest;

	Queue<PathResult> results = new Queue<PathResult>();
	
	static PathRequestManager instance;
	Pathfinding pathfinding;

	//bool isProcessingPath;

	void Awake() {
		instance = this;
		pathfinding = GetComponent<Pathfinding>();
	}

	private void Update() {
		if (results.Count > 0) {
			int itemsInQueue = results.Count;
			lock (results) {
				for (int i = 0; i < itemsInQueue; i++) {
					PathResult result = results.Dequeue();
					result.callback(result.path, result.success);
				}
			}
		}
	}

	//public static void RequestPath(Vector3 pathStart, Vector3 pathEnd, Action<Vector3[], bool> callback) {
	//	PathRequest newRequest = new PathRequest(pathStart, pathEnd, callback);
	//	instance.pathRequestQueue.Enqueue(newRequest);
	//	instance.TryProcessNext();
	//}

	public static void RequestPath(PathRequest request) {
		ThreadStart threadStart = delegate {
			instance.pathfinding.FindPath(request, FinishedProcessingPath);
		};
		threadStart.Invoke();
	}

	//void TryProcessNext() {
	//	if (!isProcessingPath && pathRequestQueue.Count > 0) {
	//		currentPathRequest = pathRequestQueue.Dequeue();
	//		isProcessingPath = true;
	//		pathfinding.StartFindPath(currentPathRequest.pathStart, currentPathRequest.pathEnd);
	//	}
	//}

	//public void FinishedProcessingPath(Vector3[] path, bool success) {
	//	currentPathRequest.callback(path, success);
	//	isProcessingPath = false;
	//	TryProcessNext();
	//}

	public static void FinishedProcessingPath(PathResult result) {
		lock (instance.results) {
			instance.results.Enqueue(result);
		}
	}

}

public struct PathRequest {
	public Vector3 pathStart;
	public Vector3 pathEnd;
	public Action<Vector3[], bool> callback;

	public PathRequest(Vector3 start, Vector3 end, Action<Vector3[], bool> callback) {
		this.pathStart = start;
		this.pathEnd = end;
		this.callback = callback;
	}
}

public struct PathResult {
	public Vector3[] path;
	public bool success;
	public Action<Vector3[], bool> callback;

	public PathResult(Vector3[] path, bool success, Action<Vector3[], bool> callback) {
		this.path = path;
		this.success = success;
		this.callback = callback;
	}

}
