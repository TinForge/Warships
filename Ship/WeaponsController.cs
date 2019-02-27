﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

///Finds transforms in 'container' and creates WeaponPlatform for each
///Transform names must include size and angle
[System.Serializable]
public class WeaponPlatform
{
	public enum Size { Small, Medium, Large };

	public Transform origin;
	private int angle;
	public Size size;
	[HideInInspector] public IArmament weapon;

	public WeaponPlatform(Transform origin)
	{
		this.origin = origin;

		string[] text = origin.name.Split(" "[0]);
		int.TryParse(text[text.Length - 1], out angle);
		if (angle == 0)
			Debug.LogError("WeaponPlatform name not ending in numeral: Angle");

		if (origin.name.Contains("Small"))
			size = Size.Small;
		else if (origin.name.Contains("Medium"))
			size = Size.Medium;
		else if (origin.name.Contains("Large"))
			size = Size.Large;
		else
			Debug.LogError("WeaponPlatform name doesn't contain word: Size");

	}

}

///Send instructions to weapon classes on ship
public class WeaponsController : MonoBehaviour
{
	[HideInInspector] public List<WeaponPlatform> platforms;

	public Transform container; //the transform containing all the platforms
	public int engagingDistance;

	void Awake()
	{
		foreach (Transform platform in container)
				platforms.Add(new WeaponPlatform(platform));
	}

	void Start()
	{
		ArmamentManager.instance.LoadWeapons(platforms);
	}

	public void Target(Vector3 point)
	{
		if (Vector3.Distance(transform.position, point) < engagingDistance)
			point = transform.position;

		foreach (WeaponPlatform wp in platforms)
			wp.weapon.Track(point, transform);
	}

	public void Fire(Vector3 point)
	{
		StartCoroutine(FireCycle(point));
		//foreach (WeaponPlatform wp in platforms)
			//wp.weapon.Fire(point, transform);
	}

	public IEnumerator FireCycle(Vector3 point)
	{
		foreach (WeaponPlatform wp in platforms) {
			wp.weapon.Fire(point, transform);
			yield return new WaitForSeconds(0.02f);
		}
	}

	void OnDrawGizmos()
	{
		if (true) {
			Gizmos.color = Color.gray;
			Gizmos.DrawWireSphere(transform.position, engagingDistance);
		}
	}

	}
