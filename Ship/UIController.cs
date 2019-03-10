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
	private Transform distanceTag;
	private Transform icon;
	private Transform healthBar;


	void Start()
	{
		shipClass = GetComponent<ShipClass>();

		SpawnUI();
		ToggleUI(false);
	}

	public void SpawnUI()
	{
		panel = LibraryUI.CreateShipPanel(shipClass.name);
		icon = LibraryUI.CreateIcon(panel);
		classTag = LibraryUI.CreateShipTag(shipClass.Classification, panel);
		levelTag = LibraryUI.CreateLevelTag(panel);
		levelTag.GetComponent<TextMeshProUGUI>().text = "Lv " + shipClass.Level;
		distanceTag = LibraryUI.CreateDistanceTag(panel);
		healthBar = LibraryUI.CreateHealthBar(panel);
	}

	public void ToggleUI(bool state)
	{
		panel.gameObject.SetActive(state);
	}

	public void DestroyUI()
	{
		Destroy(panel);
	}

	void LateUpdate()
	{
		UpdateUI();
	}

	private void UpdateUI()
	{
		if (panel != null) {
			if (!panel.gameObject.activeSelf)
				return;

				float distance = Vector3.Distance(transform.position, PlayerShip.instance.transform.position);

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
