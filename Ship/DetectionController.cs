using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DetectionController : MonoBehaviour {
	private ShipClass shipClass;

	//include its own UI module I guess, managed using UIController

	private List<ShipClass> detectedShips;

	void Awake () {
		shipClass = GetComponent<ShipClass> ();
		detectedShips = new List<ShipClass> ();

		//if (shipClass.Fleet != null)
		//	detectedShips = shipClass.Fleet.detectedShips;

		StartCoroutine (Scan ());
	}

	//perhaps separate fleet.ships from shipsmanager.list since they are always teammates
	public IEnumerator Scan () {
		yield return new WaitForSeconds (shipClass.ScanTimer);
		while (shipClass != null) { //Start cull
			foreach (ShipClass s in ShipManager.List) {
				if (detectedShips.Contains (s))
					continue;
				if (s == shipClass)
					continue;
				if (Vector3.Distance (s.transform.position, transform.position) < shipClass.SpottingDist - s.HidingDist) { //End call
					AddShip (detectedShips, s);
					break;
				}
			}
			yield return new WaitForSeconds (shipClass.ScanTimer);
		}
	}

	public void AddShip (List<ShipClass> ships, ShipClass ship) {
		ships.Add (ship);

		//if (GetComponent<PlayerShip>() != null)
		ship.GetComponent<UIController> ().ToggleUI (true);
	}

	public void RemoveShip (List<ShipClass> ships, ShipClass ship) {
		ships.Remove (ship);

		//if (GetComponent<PlayerShip>() != null)
		ship.GetComponent<UIController> ().ToggleUI (false);
	}

	void OnDrawGizmos () {
		if (!Application.isPlaying)
			return;

		if (false) {
			Gizmos.color = Color.white;
			Gizmos.DrawWireSphere (transform.position, shipClass.SpottingDist);
			Gizmos.color = Color.black;
			Gizmos.DrawWireSphere (transform.position, shipClass.HidingDist);
		}

		if (true) {
			foreach (ShipClass s in detectedShips) {
				if (s == null)
					continue;
				if (shipClass.Fleet != null) { //if OUR ship has a fleet
					if (shipClass.Fleet == s.Fleet) //if OUR fleet contains THEIR ship
						Gizmos.color = Color.green;
					else
						Gizmos.color = Color.red;
				}
				if (s.Fleet == null) //if THEIR ship does not have a fleet.
					Gizmos.color = Color.gray;

				Gizmos.DrawLine (transform.position, s.transform.position);
			}
		}
	}

}