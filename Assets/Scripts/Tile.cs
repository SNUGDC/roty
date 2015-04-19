using UnityEngine;
using System.Collections;

public class Tile : MonoBehaviour
{
	public Point2 point {
		get {
			return (Point2)transform.position;
		}
	}
	public int depth {
		get {
			return (int)transform.position.z;
		}
	}
}

