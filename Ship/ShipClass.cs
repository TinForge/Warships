using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// Fleet data, ship level/skill/experience, and base+stat properties
/// </summary>
public class ShipClass : MonoBehaviour
{
	[SerializeField] private ShipStats Stats;

	//

	[SerializeField] private Fleet fleet;
	public Fleet Fleet { get { return fleet; } }

	[SerializeField] private Image fleetIcon;
	public Image FleetIcon { get { return fleetIcon; } }

	//

	public string Classification { get { return Stats.Classification; } }

	public Image Icon { get { return Stats.Icon; } }

	public int ShipTier { get { return Stats.ShipTier; } }

	//

	[SerializeField] private int level; //relates to ship armaments: health *10, speed *10, turning *10, armor *10, regen *10, equipment, weapons
	public int Level { get { return level; } }

	[SerializeField] private int skill; //relates to ship handling: movement, spotting, targetting, accuracy, responsitivity
	public float Skill { get { return skill; } }

	[SerializeField] private int experience;
	public int Experience { get { return experience; } }


	//

	public int Health { get { return Stats.BaseHealth + ((Stats.MaxHealth - Stats.BaseHealth) /100 * level); } }

	public int Mass { get { return Stats.BaseMass + ((Stats.MaxMass - Stats.BaseMass) / 100 * level); } }


	public float MovementSpeed { get { return Stats.BaseMoveSpeed + ((Stats.MaxMoveSpeed - Stats.BaseMoveSpeed) /100* level * skill); } }

	public float TurnSpeed { get { return Stats.BaseTurnSpeed + ((Stats.MaxTurnSpeed - Stats.BaseTurnSpeed) /100* level * skill); } }



	public float Accuracy { get { return Mathf.Clamp( Stats.BaseAccuracy + ((Stats.MaxAccuracy - Stats.BaseAccuracy) * skill), 0, 0.9f); } }

	public int SpottingDist { get { return Stats.BaseSpottingDist + ((Stats.MaxSpottingDist - Stats.BaseSpottingDist) / 100 * level * skill); } }

	public int HidingDist { get { return Stats.BaseHidingDist + ((Stats.MaxHidingDist - Stats.BaseHidingDist) / 100 * level * skill); } }

	private void Awake()
	{
		level = Random.Range(1, 10);
		skill = Random.Range(0, 1);

		if (GetComponent<PlayerShip>())
			skill = 1;

		ShipManager.RegisterShip(this);
	}

}
