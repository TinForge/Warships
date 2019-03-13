using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public enum Status
{
Friendly,
Neutral,
Hostile
}

/// <summary>
/// Hard stats
/// idk about dynamic stats.
/// should this hold the ship list?
/// </summary>
[System.Serializable]
public class FleetClass
{
	public FleetClass(Fleet fleet)
	{
		this.fleet = fleet;
		//skill = Random.Range(0, 1);
	}

	Fleet fleet;

	[SerializeField][Range(0, 1)] private float skill;
	public float Skill { get { return skill; } }

	[SerializeField] private const int orderTimer = 25;
	public int OrderTimer { get { return Mathf.CeilToInt(orderTimer - (orderTimer * skill)); } }

	[SerializeField] private int fleetSize;
	public int FleetSize { get { return fleetSize; } }

	[SerializeField] private int healthTotal;
	public int HealthTotal { get { return HealthTotal; } }
	[SerializeField] private float healthAve;
	public float HealthAve { get { return healthAve; } }

	[SerializeField] private int levelTotal;
	public int LevelTotal { get { return levelTotal; } }
	[SerializeField] private float levelAve;
	public float LevelAve { get { return levelAve; } }


}

[System.Serializable]
public class Fleet : MonoBehaviour
{
	public bool playerFleet;

	public new string name;

	public FleetClass fleetClass;

	public Formation formation;
	public Strategy strategy;


	public List<GameObject> ships;
	public List<ShipClass> detectedShips;

	//[SerializeField] private Image fleetIcon;
	//public Image FleetIcon { get { return fleetIcon; } }

	//public Dictionary<FleetClass, Status> agressors;
	//public List<FleetClass> oppositionFleets;


	void Awake()
	{
		transform.name = name + " Fleet";
		fleetClass = new FleetClass(this);
		detectedShips = new List<ShipClass>();
	}

	void Start()
	{
		for(int i = 0; i < ships.Count; i++){
			ships[i] = ShipManager.AddShip(ships[i], transform.position + Vector3.right * (i*100) , transform.rotation, transform);
		}

		if (playerFleet) {
			camPivot = new GameObject();
			FindObjectOfType<Orbital>().target = camPivot.transform;
			FindObjectOfType<Orbital>().transform.parent = camPivot.transform;
		}
	}

	GameObject camPivot;
	bool scoped;

	void LateUpdate()
	{
		if (playerFleet) {
			Vector3 ave = Vector3.zero;
			foreach(GameObject ship in ships) {
				ave += ship.transform.position;
			}
			ave /= ships.Count;
			camPivot.transform.position = Vector3.MoveTowards(camPivot.transform.position, ave + Vector3.up*30, 0.5f);


			if (Input.GetKeyDown(KeyCode.F)) {
				scoped = !scoped;
				Camera.main.transform.parent.GetComponent<Orbital>().enabled = !scoped;
				Camera.main.transform.parent.GetComponent<ScopedCamera>().enabled = scoped;
			}

		}
		
	}

	public void RegisterShip(GameObject ship)
	{
		ships.Add(ship);
	}

	public void UnregisterShip(GameObject ship)
	{
		ships.Remove(ship);
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


public class Strategy
{

}

public class Formation
{

}