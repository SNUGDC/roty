﻿using UnityEngine;
using System.Collections;

public struct Point2 {
	public readonly int y, x;
	public Point2(int x, int y) {
		this.x = x;
		this.y = y;
	}
	public int idx {
		get {
			return y * TileContainer.Instance.size + x;
		}
	}

	public override bool Equals (object obj) {
		return Equals((Point2)obj);
	}
	
	public bool Equals (Point2 obj) {
		return obj.x == x && obj.y == y;
	}

	public override int GetHashCode() {
		return idx;
	}
	
	public static Point2 operator +(Point2 p1, Point2 p2) {
		return new Point2(p1.x + p2.x, p1.y + p2.y);
	}

	public static Point2 operator -(Point2 p1, Point2 p2) {
		return new Point2(p1.x - p2.x, p1.y - p2.y);
	}
	
	public static explicit operator Point2(Vector2 v) {
		return new Point2 ((int)v.x, (int)v.y);
	}
	public static explicit operator Point2(Vector3 v) {
		return new Point2 ((int)v.x, (int)v.y);
	}
	public override string ToString() {
		return "Point2( " + x + ", " + y + ")";
	}
}
