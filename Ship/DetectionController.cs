using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DetectionController : MonoBehaviour
{
	private ShipClass shipClass;

	//include its own UI module I guess, managed using UIController

	public List<ShipClass> detectedShips = new List<ShipClass>();
	public List<ShipClass> friendlyShips = new List<ShipClass>();
	public List<ShipClass> enemyShips = new List<ShipClass>();

	private float scanTimer;

	void Awake()
    {
		shipClass = GetComponent<ShipClass>();
    }

	void Start()
	{
		scanTimer = (1.1f - shipClass.Skill) * 10;

		if (shipClass.Fleet != null)
			detectedShips = shipClass.Fleet.detectedShips;

		StartCoroutine(Scan());
	}

	public IEnumerator Scan()
	{
		yield return new WaitForSeconds(scanTimer);
		while (true) {
			foreach (ShipClass s in ShipManager.Ships) {
				if (detectedShips.Contains(s))
					continue;
				if (s == shipClass)
					continue;
				if (Vector3.Distance(s.transform.position, transform.position) < shipClass.SpottingDist - s.HidingDist) {
					AddShip(detectedShips, s);
					break;
				}
			}
			yield return new WaitForSeconds(scanTimer);
		}
	}

	public void AddShip(List<ShipClass> ships, ShipClass ship)
	{
		ships.Add(ship);

		if (GetComponent<PlayerShip>() != null)
			ship.GetComponent<UIController>().ToggleUI(true);
	}

	public void RemoveShip(List<ShipClass> ships, ShipClass ship)
	{
		ships.Remove(ship);

		if (GetComponent<PlayerShip>() != null)
			ship.GetComponent<UIController>().ToggleUI(false);
	}

	void OnDrawGizmos()
	{
		if (Application.isPlaying) {
			if (true) {
				Gizmos.color = Color.white;
				Gizmos.DrawWireSphere(transform.position, shipClass.SpottingDist);
				Gizmos.color = Color.black;
				Gizmos.DrawWireSphere(transform.position, shipClass.HidingDist);
			}

			if (true) {
				Gizmos.color = Color.green;
				foreach (ShipClass s in detectedShips)
					Gizmos.DrawLine(transform.position, s.transform.position);
			}
		}
	}

}
