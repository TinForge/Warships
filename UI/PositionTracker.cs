using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PositionTracker : MonoBehaviour
{
	public Transform target;

	public Vector3 Position { get { return target.position; } }

	public void SetTarget(Transform target)
	{
		this.target = target;
	}

}
