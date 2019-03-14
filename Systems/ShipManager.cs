using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShipManager : MonoBehaviour {
	private static ShipManager instance;

	public static List<ShipClass> List;

	void Awake () {
		if (instance != null)
			Destroy (instance);
		instance = this;

		List = new List<ShipClass> ();

		foreach (ShipClass s in FindObjectsOfType<ShipClass> ())
			List.Add (s);
	}

	void Start () {
		Debug.Log ("ShipManager Start");
	}

	public static ShipClass AddShip (GameObject ship, Vector3 position, Quaternion rotation, Transform parent) {
		ShipClass shipClass = Instantiate (ship, position, rotation, parent).GetComponent<ShipClass> ();
		List.Add (shipClass);
		return shipClass;
	}

	public static void RemoveShip (ShipClass ship) {
		List.Remove (ship);
	}

	public static List<ShipClass> ListAllShips () {
		return List;
	}

	public static List<ShipClass> ListAllFriendlyShips (Fleet fleet) {
		return fleet.Friendlies;
	}

	public static List<ShipClass> ListAllFriendlyShipsOther (Fleet fleet, ShipClass self) {
		List<ShipClass> friendlies = fleet.Friendlies;
		friendlies.Remove(self);
		return friendlies;
	}

	public static List<ShipClass> ListAllEnemyShips (Fleet fleet) {
		List<ShipClass> enemies = new List<ShipClass>();
		foreach (ShipClass ship in List)
			if (!fleet.Friendlies.Contains(ship))
				enemies.Add(ship);
		return enemies;
	}
	

}