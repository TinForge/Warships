﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FleetClass
{
	[Range(0, 100)] public int skill;
	public const int orderTimer = 25;

	public int fleetSize;

	public int healthTotal;
	public int healthAve;

	public int levelTotal;
	public int levelAve;



	public List<Transform> ships;
}

//class for hostility/passive/friendly

public class Strategy
{

}

public class Formation
{

}

public abstract class Fleet : MonoBehaviour
{
	public FleetClass myFleet;
	public List<FleetClass> oppositionFleets = new List<FleetClass>();
	public Strategy strategy;
	public Formation formation;

	//Integrates on top of the ship system.

	//AI ships will be capable of detecting, and identifying other ships
	//As a fleet, this information should be shared.

	//They need instructions on formation building, and group strategies
	//A ship by itself will be able to move and fire weapons. Fleet produces directions and targets.

	//ENGAGEMENT CASES
	//Player Fleet vs AI Fleet
	//AI Fleet vs AI Fleet
	//Fleet vs Multiple Fleets

	//Orders will be given on a timer
	//Strategies will be selected based on odds and skill

	//Fleet will need to receive lots of information
		//Difficulty modifier
		//Stats of own fleet
		//Stats of other fleet
		//Outcome prediction : Machine Learning!?
		//Strategies
		//Formation positions

	public abstract void AssignFormation();




}
