using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FleetManager : MonoBehaviour {
	private static FleetManager instance;

	public static List<Fleet> List;

	void Awake () {
		if (instance != null)
			Destroy (instance);
		instance = this;

		List = new List<Fleet> ();

		foreach (Fleet f in FindObjectsOfType<Fleet> ())
			RegisterFleet (f);
	}

	public static void RegisterFleet (Fleet fleet) {
		List.Add (fleet);
		Debug.Log ("Fleet " + fleet.name + " registered");
	}

	public static void UnregisterFleet (Fleet fleet) {
		List.Remove (fleet);
		Debug.Log ("Fleet " + fleet.name + " unregistered");
	}

}