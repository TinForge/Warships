using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public enum Status {
	Friendly,
	Neutral,
	Hostile
}

[System.Serializable]
public class FleetClass {
	public FleetClass (Fleet fleet) {
		this.fleet = fleet;
		//skill = Random.Range(0, 1);
	}

	Fleet fleet;

	[SerializeField][Range (0, 1)] private float skill;
	public float Skill { get { return skill; } }

	[SerializeField] private const int orderTimer = 25;
	public int OrderTimer { get { return Mathf.CeilToInt (orderTimer - (orderTimer * skill)); } }

	public int FleetSize { get { return fleet.ships.Count; } }

	public int HealthTotal { get { return CalculateHealthTotal (); } }

	public float HealthAve { get { return CalculateHealthAve (); } }

	public int LevelTotal { get { return CalculateLevelTotal (); } }

	public float LevelAve { get { return CalculateTotalAve (); } }

	public int SkillTotal { get { return CalculateSkillTotal (); } }

	public float SkillAve { get { return CalculateSkillAve (); } }

	public float MobilityTotal { get { return CalculateMobilityTotal (); } }

	public float MobilityAve { get { return CalculateMobilityAve (); } }

	public Vector3 PosAve { get { return (CalculatePosAve ()); } }

	private int CalculateHealthTotal () {
		int total;
		foreach (ShipClass ship in ships)
			total += ship.Health;
		return total;
	}

	private float CalculateHealthAve () {
		float ave;
		foreach (ShipClass ship in ships)
			ave += ship.Health;
		ave = ave / FleetSize;
		return ave;
	}

	private int CalculateLevelTotal () {
		int total;
		foreach (ShipClass ship in ships)
			total += ship.Level;
		return total;
	}

	private float CalculateTotalAve () {
		float ave;
		foreach (ShipClass ship in ships)
			ave += ship.Level;
		ave = ave / FleetSize;
		return ave;
	}

	private int CalculateSkillTotal () {
		int total;
		foreach (ShipClass ship in ships)
			total += ship.Skill;
		return total;
	}

	private float CalculateSkillAve () {
		float ave;
		foreach (ShipClass ship in ships)
			ave += ship.Skill;
		ave = ave / FleetSize;
		return ave;
	}

	private int CalculateMobilityTotal () {
		int total;
		foreach (ShipClass ship in ships)
			total += ship.MovementSpeed;
		return total;
	}

	private float CalculateMobilityAve () {
		float ave;
		foreach (ShipClass ship in ships)
			ave += ship.MovementSpeed;
		ave = ave / FleetSize;
		return ave;
	}

	private Vector3 CalculatePosAve () {
		Vector3 ave;
		foreach (ShipClass ship in ships)
			ave += ship.transform.position;
		ave = ave / FleetSize;
		return ave;
	}

}

[System.Serializable]
public class Fleet : MonoBehaviour {
	public bool playerFleet;

	public new string name;

	public FleetClass fleetClass;

	public Formation formation;
	public Strategy strategy;

	public List<ShipClass> ships;
	public List<ShipClass> detectedShips;

	//[SerializeField] private Image fleetIcon;
	//public Image FleetIcon { get { return fleetIcon; } }

	//public Dictionary<FleetClass, Status> agressors;
	//public List<FleetClass> oppositionFleets;

	void Awake () {
		transform.name = name + " Fleet";
		fleetClass = new FleetClass (this);
		detectedShips = new List<ShipClass> ();
	}

	void Start () {
		InstantiateShips ();

		DirtyCodeStart ();
	}

	void LateUpdate () {
		DirtyCode ();
	}

	public void RegisterShip (GameObject ship) {
		ships.Add (ship);
	}

	public void UnregisterShip (GameObject ship) {
		ships.Remove (ship);
	}

	//AI ships will be capable of detecting, and identifying other ships
	//As a fleet, this information should be shared.

	//They need instructions on formation building, and group strategies
	//A ship by itself will be able to move and fire weapons. Fleet produces directions and targets.

	//Strategies will be selected based on odds and skill

	//Fleet will need to receive lots of information
	//Difficulty modifier
	//Stats of own fleet
	//Stats of other fleet
	//Outcome prediction : Machine Learning!?
	//Strategies
	//Formation positions

}

public void InstantiateShips () {
	for (int i = 0; i < ships.Count; i++)
		ships[i] = ShipManager.AddShip (ships[i], transform.position + Vector3.right * (i * 100), transform.rotation, transform); //casting error for gameobject/shipclass
}

public void DirtyCodeStart () {
	if (playerFleet) {
		camPivot = new GameObject ();
		FindObjectOfType<Orbital> ().target = camPivot.transform;
		FindObjectOfType<Orbital> ().transform.parent = camPivot.transform;
	}
}

GameObject camPivot;
bool scoped;
public void DirtyCode () {
	if (playerFleet) {
		Vector3 ave = Vector3.zero;
		foreach (GameObject ship in ships) {
			ave += ship.transform.position;
		}
		ave /= ships.Count;
		camPivot.transform.position = Vector3.MoveTowards (camPivot.transform.position, ave + Vector3.up * 30, 0.5f);

		if (Input.GetKeyDown (KeyCode.F)) {
			scoped = !scoped;
			Camera.main.transform.parent.GetComponent<Orbital> ().enabled = !scoped;
			Camera.main.transform.parent.GetComponent<ScopedCamera> ().enabled = scoped;
		}

	}

}

public class Strategy {

}

public class Formation {

}