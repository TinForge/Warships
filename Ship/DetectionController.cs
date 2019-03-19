using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DetectionController : MonoBehaviour
{
	private ShipClass shipClass;
	private Fleet fleet;

	//include its own UI module I guess, managed using UIController

	private List<ShipClass> friendlies;
	private List<ShipClass> enemies;

	void Awake()
	{
		shipClass = GetComponent<ShipClass>();
		fleet = shipClass.Fleet;

		if (shipClass.Fleet == null)
			Debug.LogError("DetectionController has no fleet");
	}

	void Start()
	{
		if(!fleet.Player)
		GetComponent<UIController>().ToggleUI(false);

		friendlies = fleet.Friendlies;
		enemies = fleet.Enemies;

		StartCoroutine(Scan());
	}

	//perhaps separate fleet.ships from shipsmanager.list since they are always teammates
	public IEnumerator Scan()
	{
		yield return new WaitForSeconds(shipClass.ScanTimer);
		while (shipClass != null) {
			foreach (ShipClass s in ShipManager.ListAllEnemyShips(fleet))
				if (Vector3.Distance(s.transform.position, transform.position) < shipClass.SpottingDist - s.HidingDist) //End call
					fleet.RegisterShip(s, enemies);
				
			yield return new WaitForSeconds(shipClass.ScanTimer);
		}
	}

	void OnDrawGizmos()
	{
		if (!Application.isPlaying)
			return;

		if (false) {
			Gizmos.color = Color.white;
			Gizmos.DrawWireSphere(transform.position, shipClass.SpottingDist);
			Gizmos.color = Color.black;
			Gizmos.DrawWireSphere(transform.position, shipClass.HidingDist);
		}

		if (shipClass!=null) {
			Gizmos.color = Color.green;
			foreach (ShipClass s in friendlies) 
				Gizmos.DrawLine(transform.position, s.transform.position);
			
			Gizmos.color = Color.red;
			foreach (ShipClass s in enemies) 
				Gizmos.DrawLine(transform.position, s.transform.position);
		}
	}
}
