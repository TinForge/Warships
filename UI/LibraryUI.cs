using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System;

public class LibraryUI : MonoBehaviour
{
	private static LibraryUI instance;

	public const float ClippingDist = 1000;
	public const float MaxUIDist = 10000;
	public const float ZScaler = ClippingDist / MaxUIDist;

	public static Dictionary<Transform, List<Counter>> Counters;

	[SerializeField] public Transform overlay;


	[SerializeField] public GameObject shipIcon;
	[SerializeField] public GameObject shipPanel;
	[SerializeField] public GameObject classTag;
	[SerializeField] public GameObject levelTag;
	[SerializeField] public GameObject healthTag;
	[SerializeField] public GameObject fleetTag;
	[SerializeField] public GameObject distanceTag;
	[SerializeField] public GameObject icon;
	[SerializeField] public GameObject healthBar;
	[SerializeField] public GameObject damageCounter;
	[SerializeField] public GameObject healCounter;

	void Awake()
    {
		if (instance != null)
			Destroy(instance);
		instance = this;

		Counters = new Dictionary<Transform, List<Counter>>();
	}

	void Start()
	{
		Debug.Log("LibraryUI Start");
	}

	void LateUpdate()
	{
		for (int i = 0; i < overlay.transform.childCount-1; i++)
			if (overlay.GetChild(i).position.z < overlay.GetChild(i+1).position.z)
				overlay.GetChild(i).SetSiblingIndex(i+1);
	}

	public static Transform CreateShipIcon(string name, Fleet fleet)
	{
		GameObject t = Instantiate(instance.shipIcon, instance.overlay);
		t.name = name;
		t.GetComponent<Image>().color = (fleet.Player == true ? Color.green : Color.red);
		return t.transform;
	}

	public static Transform CreateShipPanel(Transform parent)
	{
		GameObject t = Instantiate(instance.shipPanel, parent);
		return t.transform;
	}

	public static Transform CreateShipTag(string name, Transform parent)
	{
		GameObject t = Instantiate(instance.classTag, parent);
		t.GetComponent<TextMeshProUGUI>().text = name;
		return t.transform;
	}

	public static Transform CreateLevelTag(Transform parent, string text)
	{
		GameObject t = Instantiate(instance.levelTag, parent);
		t.GetComponent<TextMeshProUGUI>().text = text;
		return t.transform;
	}

	public static Transform CreateHealthTag(Transform parent)
	{
		GameObject t = Instantiate(instance.healthTag, parent);
		return t.transform;
	}

	public static Transform CreateFleetTag(Transform parent, string name)
	{
		GameObject t = Instantiate(instance.fleetTag, parent);
		t.GetComponent<TextMeshProUGUI>().text = name;
		return t.transform;
	}

	public static Transform CreateDistanceTag(Transform parent)
	{
		GameObject t = Instantiate(instance.distanceTag, parent);
		return t.transform;
	}

	public static Transform CreateIcon(Transform parent)
	{
		GameObject t = Instantiate(instance.icon, parent);
		return t.transform;
	}

	public static Transform CreateHealthBar(Transform parent)
	{
		GameObject t = Instantiate(instance.healthBar, parent);
		return t.transform;
	}

	public static Transform CreateDamageCounter(Transform parent, int damage)
	{
		GameObject t = Instantiate(instance.damageCounter, instance.overlay);
		t.GetComponent<Counter>().target =parent;
		t.GetComponent<TextMeshProUGUI>().text = damage +"";
		return t.transform;
	}

	public static Transform CreateHealCounter(Transform parent, int heal)
	{
		GameObject t = Instantiate(instance.healCounter, instance.overlay);
		t.GetComponent<Counter>().target = parent;
		t.GetComponent<TextMeshProUGUI>().text = heal + "";
		return t.transform;
	}

}
