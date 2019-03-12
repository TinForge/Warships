using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class UIController : MonoBehaviour, iShipDisable, iHealthChange
{
	private ShipClass shipClass;

	private Transform panel;
	private Transform classTag;
	private Transform levelTag;
	private Transform healthTag;
	private Transform fleetTag;
	private Transform distanceTag;
	private Transform icon;
	private Transform healthBar;


	void Awake()
	{
		shipClass = GetComponent<ShipClass>();
	}

	void Start()
	{
		SpawnUI();
		ToggleUI(false);
	}

	public void SpawnUI()
	{
		panel = LibraryUI.CreateShipPanel(shipClass.name);
		icon = LibraryUI.CreateIcon(panel);
		classTag = LibraryUI.CreateShipTag(shipClass.Classification, panel);
		levelTag = LibraryUI.CreateLevelTag(panel, "Lv " + shipClass.Level);
		healthTag = LibraryUI.CreateHealthTag(panel);
		healthTag.GetComponent<TextMeshProUGUI>().text = GetComponent<HealthController>().Health + "";
		if(shipClass.Fleet!=null)
		fleetTag = LibraryUI.CreateFleetTag(panel, shipClass.Fleet.name);
		distanceTag = LibraryUI.CreateDistanceTag(panel);
		healthBar = LibraryUI.CreateHealthBar(panel);
	}

	public void ToggleUI(bool state)
	{
		panel.gameObject.SetActive(state);
	}


	void LateUpdate()
	{
		if (panel != null) {
			if (!panel.gameObject.activeSelf)
				return;

			float distance = Vector3.Distance(transform.position, Camera.main.transform.position); //PlayerShip.instance.transform.position);

			Vector3 origin = transform.position + Vector3.up * (50); // + distance/1000) ;
			Vector3 rectPos = Camera.main.WorldToScreenPoint(origin);
			if (rectPos.z > 0)
				rectPos.z = distance * LibraryUI.ZScaler;
			else
				rectPos = new Vector3(-100, -100, 0);
			panel.position = rectPos;

			distanceTag.GetComponent<TextMeshProUGUI>().text = distance.ToString("N0") + "m";
		}
	}

	/* Clashes with detection system
	private void OnMouseOver()
	{
		ToggleUI(true);
	}

	private void OnMouseExit()
	{
		ToggleUI(false);
	}
	*/

	public void HealthChange(int amount, float ratio)
	{
		healthTag.GetComponent<TextMeshProUGUI>().text = GetComponent<HealthController>().Health + "";
		healthBar.GetComponent<Slider>().value = ratio;

		if(amount > 0)
			LibraryUI.CreateDamageCounter(transform, amount);
		else
			LibraryUI.CreateHealCounter(transform, -amount);

	}

	public void Disable()
	{
		panel.GetComponent<FadeAlpha>().Activate();
	}
}
