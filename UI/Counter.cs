using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Counter : MonoBehaviour
{

	public Transform target;
	private Vector3 offset;
	public float speed;

	private int thisValue;
	private List<Counter> values;


	void Start()
	{
		transform.position = Camera.main.WorldToScreenPoint(target.position);
		
		if (!LibraryUI.Counters().ContainsKey(target))
			LibraryUI.Counters().Add(target, new List<Counter>());

		LibraryUI.Counters().TryGetValue(target, out values);
		values.Add(this);

		thisValue = values.Count;
	}

	void OnDisable()
	{
		values.Remove(this);
	}

	void LateUpdate()
    {
		if(thisValue < values.Count) {
			offset += Vector3.up * 13 * (values.Count - thisValue);
			thisValue = values.Count;
		}
		Vector3 wtsp = Camera.main.WorldToScreenPoint(target.position);

		transform.position = wtsp+offset; //pin to screen position
		offset += Vector3.left * speed * Time.deltaTime;
		transform.position = Vector3.Lerp(transform.position, wtsp + offset, .05f); //UI lerp effect
    }
}
