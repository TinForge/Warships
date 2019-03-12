using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArmamentManager : MonoBehaviour
{
	public static ArmamentManager instance;

	public GameObject large;
	public GameObject medium;
	public GameObject small;

	void Awake()
	{
		if (instance != this && instance == null)
			instance = this;
	}

	public void LoadWeapons(List<WeaponPlatform> platforms)
	{
		foreach(WeaponPlatform wp in platforms) {
			GameObject obj = null;
			switch (wp.size) {
			case WeaponPlatform.Size.Large:
				obj = large;
				break;
			case WeaponPlatform.Size.Medium:
				obj = medium;
				break;
			case WeaponPlatform.Size.Small:
				obj = small;
				break;
			}
			wp.weapon = Instantiate(obj, wp.origin).GetComponent<iWeapon>();
		}
	}


}
