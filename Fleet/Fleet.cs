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
		Fleet = fleet;
		//skill = Random.Range(0, 1);
	}

	public Fleet Fleet { get; set; }

	[SerializeField][Range (0, 1)] private float skill;
	public float Skill { get { return skill; } }

	[SerializeField] private const int orderTimer = 25;
	public int OrderTimer { get { return Mathf.CeilToInt (orderTimer - (orderTimer * skill)); } }

	public int FleetSize { get { return Fleet.Friendlies.Count; } }

	public int LevelTotal { get { return CalculateLevelTotal (); } }

	public float LevelAve { get { return CalculateLevelTotal() / FleetSize; } }

	public float SkillTotal { get { return CalculateSkillTotal (); } }

	public float SkillAve { get { return CalculateSkillTotal() / FleetSize; } }

	public int HealthTotal { get { return CalculateHealthTotal(); } }

	public float HealthAve { get { return CalculateHealthTotal() / FleetSize; } }

	public int FirepowerTotal { get { return CalculateFirepowerTotal(); } }

	public float FirepowerAve { get { return CalculateFirepowerTotal() / FleetSize; } }

	public float MobilityTotal { get { return CalculateMobilityTotal (); } }

	public float MobilityAve { get { return CalculateMobilityTotal() / FleetSize; } }

	public float CruiseSpeed { get { return CalculateCruiseSpeed(); } }

	public Vector3 PosAve { get { return (CalculatePosAve ()); } }

	private int CalculateLevelTotal () {
		int total = 0;
		foreach (ShipClass ship in Fleet.Friendlies)
				total += ship.Level;
		return total;
	}

	private float CalculateSkillTotal () {
		float total = 0;
		foreach (ShipClass ship in Fleet.Friendlies)
				total += ship.Skill;
		return total;
	}

	private int CalculateHealthTotal()
	{
		int total = 0;
		foreach (ShipClass ship in Fleet.Friendlies)
				total += ship.Health;
		return total;
	}

	private int CalculateFirepowerTotal()
	{
		int total = 0;
		foreach (ShipClass ship in Fleet.Friendlies)
				total += ship.Firepower;
		return total;
	}

	private float CalculateMobilityTotal () {
		float total = 0;
		foreach (ShipClass ship in Fleet.Friendlies)
				total += ship.MovementSpeed;
		return total;
	}
	
	private float CalculateCruiseSpeed()
	{
		float lowest = Mathf.Infinity;
		foreach (ShipClass ship in Fleet.Friendlies)
				if (lowest > ship.MovementSpeed)
				lowest = ship.MovementSpeed;
		return lowest;
	}

	private Vector3 CalculatePosAve () {
		Vector3 ave = Vector3.zero;
		foreach (ShipClass ship in Fleet.Friendlies)
			ave += ship.transform.position;
		if(FleetSize > 0)
			ave = ave / FleetSize;
		return ave;
	}

}

[System.Serializable]
public class Fleet : MonoBehaviour
{
	public bool Player; // { get; set; }

	public string Name { get; set; }

	public FleetClass FleetClass { get; set; }

	public Formation formation { get; set; }
	public Strategy strategy { get; set; }

	[SerializeField] private List<GameObject> spawnShips;
	public List<ShipClass> Friendlies { get; set; }
	public List<ShipClass> Enemies { get; set; }
	[SerializeField] private List<ShipClass> FRIENDS_DISPLAY;
	[SerializeField] private List<ShipClass> ENEMIES_DISPLAY;

	//[SerializeField] private Image fleetIcon;
	//public Image FleetIcon { get { return fleetIcon; } }

	//public Dictionary<FleetClass, Status> agressors;
	//public List<FleetClass> oppositionFleets;

	void Awake()
	{
		transform.name = Name + " Fleet";
		FleetClass = new FleetClass(this);
		Friendlies = new List<ShipClass>();
		Enemies = new List<ShipClass>();
		FRIENDS_DISPLAY = Friendlies;
		ENEMIES_DISPLAY = Enemies;
	}

	void Start()
	{
		InstantiateFleetShips();
		DirtyCodeStart();
	}

	void LateUpdate()
	{
		DirtyCode();
	}

	//

	public void RegisterShip(ShipClass ship, List<ShipClass> list)
	{
		if (list.Contains(ship)) {
			ship.GetComponent<UIController>().ToggleUI(true);
			return;
		}

		list.Add(ship);
		ship.GetComponent<UIController>().ToggleUI(true);
	}

	public void UnregisterShip(ShipClass ship, List<ShipClass> list)
	{
		list.Remove(ship);
		ship.GetComponent<UIController>().ToggleUI(false);
	}


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


	public void InstantiateFleetShips()
	{
		for (int i = 0; i < spawnShips.Count; i++) {
			ShipClass spawn = ShipManager.AddShip(spawnShips[i], transform.position + Vector3.right * (i * 100), transform.rotation, transform);
			RegisterShip(spawn, Friendlies);
		}
	}

	public void DirtyCodeStart()
	{
		if (Player) {
			camPivot = new GameObject();
			FindObjectOfType<Orbital>().target = camPivot.transform;
			FindObjectOfType<Orbital>().transform.parent = camPivot.transform;
		}
	}

	GameObject camPivot;
	bool scoped;
	public void DirtyCode()
	{
		if (Player) {

			Vector3 ave = FleetClass.PosAve;
			camPivot.transform.position = Vector3.MoveTowards(camPivot.transform.position, ave + Vector3.up * 30, 2.5f);

				if (Input.GetKeyDown(KeyCode.F)) {
					scoped = !scoped;
					Camera.main.transform.parent.GetComponent<Orbital>().enabled = !scoped;
					Camera.main.transform.parent.GetComponent<ScopedCamera>().enabled = scoped;
				}

			
		}
	}



	public class Strategy
	{

	}

	public class Formation
	{

	}
}