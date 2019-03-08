﻿using System.Collections;
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
		panel = LibraryUI.CreateShipPanel(shipClass.name);
		icon = LibraryUI.CreateIcon(panel);
		classTag = LibraryUI.CreateShipTag(shipClass.Classification, panel);
		levelTag = LibraryUI.CreateLevelTag(panel);
		distanceTag = LibraryUI.CreateDistanceTag(panel);
		healthBar = LibraryUI.CreateHealthBar(panel);

		RefreshLevel();
	}

	void LateUpdate()
	{
		UpdateUI();
	}

	private void UpdateUI()
	{
		if (panel != null) {
			Vector3 origin = transform.position + Vector3.up * 50;
			Vector3 rectPos = Camera.main.WorldToScreenPoint(origin);
			if (rectPos.z > 0)
				rectPos.z = Vector3.Distance(origin, Camera.main.transform.position);
			else
				rectPos = new Vector3(-100, -100, 0);
			panel.position = rectPos;

			float dist = Vector3.Distance(transform.position, PlayerShip.instance.transform.position);
			distanceTag.GetComponent<TextMeshProUGUI>().text = dist.ToString("N0") + "m";
		}
	}

	public void RefreshLevel()
	{
		levelTag.GetComponent<TextMeshProUGUI>().text = "Lv " + shipClass.Level;
	}

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
