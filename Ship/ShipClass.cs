using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ShipClass : MonoBehaviour
{
	private string fleet;
	public string classification;
	public Image icon;
	public int level; //relates to ship armaments: health *10, speed *10, turning *10, armor *10, regen *10, equipment, weapons
	public int skill; //relates to ship handling: movement, spotting, targetting, accuracy, responsitivity
	public int exp;

	public int modHealth;
	public int modSpeed;

	private Transform panel;
	private Transform classTag;
	private Transform healthBar;

	void Start()
	{
		panel = LibraryUI.CreateShipPanel(transform.name);
		classTag = LibraryUI.CreateShipTag(classification,panel);
		healthBar = LibraryUI.CreateHealthBar(panel);
	}

	void OnEnable()
	{
		GetComponent<HealthController>().OnHealthChange += SetHealthBar;
	}

	void Update()
	{
		UpdateUI();
	}

	private void UpdateUI()
	{
		Vector3 origin = transform.position + Vector3.up * 50;
		Vector3 rectPos = Camera.main.WorldToScreenPoint(origin);
		rectPos.z = Vector3.Distance(origin, Camera.main.transform.position);
		panel.position = rectPos;
	}

	void SetHealthBar(float ratio)
	{
		healthBar.GetComponent<Slider>().value = ratio;
	}

	void OnDisable()
	{
		GetComponent<HealthController>().OnHealthChange -= SetHealthBar;
	}

}
