using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShipManager : MonoBehaviour
{
	private static ShipManager instance;

	public static List<ShipClass> List;

	void Awake()
	{
		if (instance != null)
			Destroy(instance);
		instance = this;

		List = new List<ShipClass>();

		foreach (ShipClass s in FindObjectsOfType<ShipClass>())
			List.Add(s);
	}

	void Start()
	{
		Debug.Log("ShipManager Start");
	}

	public static GameObject AddShip(GameObject ship,Vector3 position, Quaternion rotation, Transform parent)
	{
		ship = Instantiate(ship, position, rotation, parent);
		List.Add(ship.GetComponent<ShipClass>());
		return ship;
	}

	public static void RemoveShip(ShipClass ship)
	{
		List.Remove(ship);
	}

}
