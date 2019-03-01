using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FloatUpward : MonoBehaviour
{
	private PositionTracker pt;

	private int serial;
	private static int count = 0;
	private Vector3 offset;
	public float speed;


	void Awake()
	{
		pt = GetComponent<PositionTracker>();
	}

	void OnEnable()
	{
		serial = count;
		//if(serial % 2 == 0)
		//offset += Vector3.up * count * 20;
		//else
		//	offset += Vector3.up * count * 10 + Vector3.right * 25;
		count++;
	}

	void OnDisable()
	{
		count--;
	}

	void LateUpdate()
    {
		if(serial < count) {
			offset += Vector3.up * 20 * (count - serial);
			serial = count;
		}

		offset += Vector3.up * speed * Time.deltaTime;
		transform.position = Camera.main.WorldToScreenPoint(pt.Position) + offset;
    }
}
