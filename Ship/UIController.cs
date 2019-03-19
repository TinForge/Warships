using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class UIController : MonoBehaviour, iShipDisable, iHealthChange
{
	private ShipClass shipClass;

	private Transform icon;
	private Transform panel;
	private Transform classTag;
	private Transform levelTag;
	private Transform healthTag;
	private Transform fleetTag;
	private Transform distanceTag;
	private Transform icon2;
	private Transform healthBar;


	void Awake()
	{
		shipClass = GetComponent<ShipClass>();
		SpawnUI();
		ToggleUI(false);
	}

	void Start()
	{
		healthTag.GetComponent<TextMeshProUGUI>().text = GetComponent<HealthController>().Health + "";
	}

	public void SpawnUI()
	{
		icon = LibraryUI.CreateShipIcon(shipClass.name, shipClass.Fleet);
		panel = LibraryUI.CreateShipPanel(icon);
		icon2 = LibraryUI.CreateIcon(panel);
		classTag = LibraryUI.CreateShipTag(shipClass.Classification, panel);
		levelTag = LibraryUI.CreateLevelTag(panel, "Lv " + shipClass.Level);
		healthTag = LibraryUI.CreateHealthTag(panel);
		healthTag.GetComponent<TextMeshProUGUI>().text = GetComponent<HealthController>().Health + "";
		if(shipClass.Fleet!=null)
		fleetTag = LibraryUI.CreateFleetTag(panel, shipClass.Fleet.name);
		distanceTag = LibraryUI.CreateDistanceTag(panel);
		healthBar = LibraryUI.CreateHealthBar(panel);


		icon.gameObject.SetActive(false);
		panel.gameObject.SetActive(false);
	}

	public void ToggleUI(bool state)
	{
		icon.gameObject.SetActive(state);
	}


	void LateUpdate()
	{
		if (icon != null) {
			if (!icon.gameObject.activeSelf)
				return;

			float distance = Vector3.Distance(transform.position, Camera.main.transform.position); //PlayerShip.instance.transform.position);

			SetPosition(distance);

			ScaleIcons(distance);

			RefreshDistance(distance);
		}
	}

	void SetPosition(float distance)
	{
		Vector3 origin = transform.position + Vector3.up * (50); // + distance/1000) ;
		Vector3 rectPos = Camera.main.WorldToScreenPoint(origin);
		if (rectPos.z > 0)
			rectPos.z = distance * LibraryUI.ZScaler;
		else
			rectPos = new Vector3(-100, -100, 0);
		icon.position = rectPos;
	}

	void ScaleIcons(float distance)
	{
		float lerp = Mathf.InverseLerp(500, 3000, distance);
		float size = Mathf.Lerp(25, 5f, lerp);
		icon.GetComponent<RectTransform>().sizeDelta = new Vector2(size, size);
	}

	void RefreshDistance(float distance)
	{
		distanceTag.GetComponent<TextMeshProUGUI>().text = distance.ToString("N0") + "m";
	}
	

	public void HealthChange(int current, int delta, float ratio)
	{
		healthTag.GetComponent<TextMeshProUGUI>().text = GetComponent<HealthController>().Health + "";
		healthBar.GetComponent<Slider>().value = ratio;

		if(delta > 0)
			LibraryUI.CreateDamageCounter(transform, delta);
		else if (delta < 0)
			LibraryUI.CreateHealCounter(transform, -delta);

	}

	void OnMouseOver()
	{
		panel.gameObject.SetActive(true);
		Cursor.visible = false;
	}

	void OnMouseExit()
	{
		panel.gameObject.SetActive(false);
		Cursor.visible = true;
	}

	public void Disable()
	{
		icon.GetComponent<FadeAlpha>().Activate();
	}
}
