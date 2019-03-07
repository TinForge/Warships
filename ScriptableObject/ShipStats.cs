using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// Ship classification and base/add stats
/// </summary>
[CreateAssetMenu]
public class ShipStats : ScriptableObject
{

	[SerializeField] private string classification;
	public string Classification { get { return classification; } }

	[SerializeField] private Image icon;
	public Image Icon { get { return icon; } }

	[SerializeField] private int shipTier;
	public int ShipTier { get { return shipTier; } }

	//
	[Space]
	[SerializeField] private int baseHealth;
	public int BaseHealth { get { return baseHealth; } }
	[SerializeField] private int maxHealth;
	public int MaxHealth { get { return maxHealth; } }

	[Space]
	[SerializeField] private int baseMass;
	public int BaseMass { get { return baseMass; } }
	[SerializeField] private int maxMass;
	public int MaxMass { get { return maxMass; } }

	[Space]
	[SerializeField] private float baseMoveSpeed;
	public float BaseMoveSpeed { get { return baseMoveSpeed; } }
	[SerializeField] private float maxMoveSpeed;
	public float MaxMoveSpeed { get { return maxMoveSpeed; } }

	[Space]
	[SerializeField] private float baseTurnSpeed;
	public float BaseTurnSpeed { get { return baseTurnSpeed; } }
	[SerializeField] private float maxTurnSpeed;
	public float MaxTurnSpeed { get { return maxTurnSpeed; } }

	[Space]
	[SerializeField] private float baseAccuracy;
	public float BaseAccuracy { get { return baseAccuracy; } }
	[SerializeField] private float maxAccuracy;
	public float MaxAccuracy { get { return maxAccuracy; } }

	[Space]
	[SerializeField] private int baseSpottingDist;
	public int BaseSpottingDist { get { return baseSpottingDist; } }
	[SerializeField] private int maxSpottingDist;
	public int MaxSpottingDist { get { return maxSpottingDist; } }


}
