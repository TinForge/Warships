using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System;

public class LibraryUI : MonoBehaviour
{
	private static LibraryUI instance;


	[SerializeField] public Transform overlay;


	[SerializeField] public GameObject shipPanel;
	[SerializeField] public GameObject classTag;
	[SerializeField] public GameObject healthBar;
	[SerializeField] public GameObject damageCounter;

	void Awake()
    {
		if (instance != this && instance == null)
			instance = this;
		else
			Debug.LogWarning("Multiple Instances");
    }

	void LateUpdate()
	{
		for (int i = 0; i < overlay.transform.childCount-1; i++)
			if (overlay.GetChild(i).position.z < overlay.GetChild(i+1).position.z)
				overlay.GetChild(i).SetSiblingIndex(i+1);

		//for (int i = 0; i < overlay.transform.childCount - 1; i++)
		//	if ( overlay.GetChild(i).position.z < overlay.GetChild(i + 1).position.z)

	}

	public static void CreatePlayerUI()
	{

	}

	public static Transform CreateShipPanel(string name)
	{
		GameObject t = Instantiate(instance.shipPanel, instance.overlay);
		t.name = name;
		return t.transform;
	}

	public static Transform CreateShipTag(string name,Transform parent)
	{
		GameObject t = Instantiate(instance.classTag, parent);
		t.GetComponent<TextMeshProUGUI>().text = name;
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
		t.GetComponent<PositionTracker>().SetTarget(parent);
		t.GetComponent<TextMeshProUGUI>().text = damage +"";
		return t.transform;
	}

}
