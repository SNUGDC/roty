using UnityEngine;
using System.Linq;
using System.Collections;
using System.Collections.Generic;
using ExtensionMethods;

public class Polyomino {
	public readonly Point2[] points;
	public readonly Point2 constraints;
	public Polyomino(params Point2[] points) {
		this.points = points;

		this.constraints = points.Aggregate(
			new Point2(0, 0),
			(point, result) => new Point2(
			point.x > result.x ? point.x : result.x,
			point.y > result.y ? point.y : result.y));
	}
}
