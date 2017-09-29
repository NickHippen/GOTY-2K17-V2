using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public struct Line {

	const float verticalLineSlope = 1e5f;

	float slope;
	float yIntercept;
	Vector2 pointOnLine1;
	Vector2 pointOnLine2;

	float slopePerpendicular;

	bool approachSide;

	public Line(Vector2 pointOnLine, Vector2 pointPerpendicularToLine) {
		float dx = pointOnLine.x - pointPerpendicularToLine.x;
		float dy = pointOnLine.y - pointPerpendicularToLine.y;

		if (dx == 0) {
			slopePerpendicular = verticalLineSlope;
		} else {
			slopePerpendicular = dy / dx;
		}

		if (slopePerpendicular == 0) {
			slope = verticalLineSlope;
		} else {
			slope = -1 / slopePerpendicular;
		}

		yIntercept = pointOnLine.y - slope * pointOnLine.x; // y = mx + b --> b = y - mx
		pointOnLine1 = pointOnLine;
		pointOnLine2 = pointOnLine + new Vector2(1, slope); // (Can be any point along the line)

		approachSide = false;
		approachSide = GetSide(pointPerpendicularToLine);
	}

	bool GetSide(Vector2 p) {
		return (p.x - pointOnLine2.x) * (pointOnLine2.y - pointOnLine1.y) > (p.y - pointOnLine1.y) * (pointOnLine2.x - pointOnLine1.x); // cross product
	}

	public bool HasCrossedLine(Vector2 p) {
		return GetSide(p) != approachSide;
	}

	public void DrawWithGizmos(float length) {
		Vector3 lineDir = new Vector3(1, 0, slope).normalized;
		Vector3 lineCenter = new Vector3(pointOnLine1.x, 0, pointOnLine1.y) + Vector3.up;
		Gizmos.DrawLine(lineCenter - lineDir * length / 2f, lineCenter + lineDir * length / 2f);
	}

}
