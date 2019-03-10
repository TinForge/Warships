using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShipManager : MonoBehaviour
{
	public static ShipManager instance;

	public static List<ShipClass> Ships = new List<ShipClass>();

	private void Awake()
	{
		if (instance != this && instance == null)
			instance = this;
		else
			Debug.LogError("Duplicate singletons: ShipManager");
		Ships = new List<ShipClass>();
	}

	private void Start()
	{
		foreach (ShipClass s in GameObject.FindObjectsOfType<ShipClass>())
			RegisterShip(s);
	}

	public List<ShipClass> List()
	{
		return Ships;
	}

	public static void RegisterShip(ShipClass ship)
	{
		Ships.Add(ship);
	}

	public static void UnregisterShip(ShipClass ship)
	{
		Ships.Remove(ship);
	}

}
