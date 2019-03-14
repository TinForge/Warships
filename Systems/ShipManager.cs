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
		ship = Instantiate (ship, position, rotation, parent).GetComponent<ShipClass> ();
		List.Add (ship);
		return ship;
	}

	public static void RemoveShip (ShipClass ship) {
		List.Remove (ship);
	}

	public static List<ShipClass> ListAllShips () {
		return List;
	}

	public static List<ShipClass> ListAllFriendlyShips (Fleet fleet) {
		return fleet.ships;
	}

	public static List<ShipClass> ListAllFriendlyShipsOther (Fleet fleet) {
		return fleet.ships;
	}

	/* 
		public static List<ShipClass> ListAllEnemyShips (Fleet fleet) {
			List<ShipClass> enemies = //clone list
			return fleet.ships;
		}
	*/

}