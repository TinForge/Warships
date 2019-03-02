using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageCounter : MonoBehaviour
{
	private PositionTracker pt;

	private static Dictionary<Transform, List<DamageCounter>> dict = new Dictionary<Transform,List<DamageCounter>>();

	private Vector3 offset;
	public float speed;

	private int thisValue;
	private List<DamageCounter> values;

	void Awake()
	{
		pt = GetComponent<PositionTracker>();
	}

	void Start()
	{
		transform.position = Camera.main.WorldToScreenPoint(pt.Position);
	}

	void OnEnable()
	{
		if (!dict.ContainsKey(pt.target))
			dict.Add(pt.target, new List<DamageCounter>());

		dict.TryGetValue(pt.target, out values);
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
		Vector3 wtsp = Camera.main.WorldToScreenPoint(pt.Position);

		transform.position = wtsp+offset; //pin to screen position
		offset += Vector3.left * speed * Time.deltaTime;
		transform.position = Vector3.Lerp(transform.position, wtsp + offset, .05f); //UI lerp effect
    }
}
