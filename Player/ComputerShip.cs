using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ComputerShip : MonoBehaviour
{

	//Fleet information

	//Move - Using navmesh A* pathfinding

	//Targetting - Using automatic targetting system

	//Computer state decision making

	private MovementController movement;
	private WeaponsController weapons;

	public bool useTargettingSystem;
	public bool targetAcquired;

	private new Camera camera;
	private float lastKnownZ;

	public bool playerControlled;



	void Start()
	{
		camera = Camera.main;
		movement = GetComponent<MovementController>();
		weapons = GetComponentInChildren<WeaponsController>();


	}

	void Update()
	{
		//Automatic:
		//Lock onto a target
		//Raycast visibility to it
		//Lead using velocity and time
		//Weapons.fire OR Fire using TargetData


		//Depending: The targetting point
		//Either: Automatic or Manual

		//Manual:
		//Raycast a point
		//Weapons.Fire

		if (!playerControlled)
			return;

		if (useTargettingSystem)
			Targetting();
		else
			MouseInput();
		MoveInput();
	}

	private void Targetting()
	{

	}

	private void MouseInput()
	{
		if (weapons == null)
			return;

		Ray ray = camera.ScreenPointToRay(Input.mousePosition);
		RaycastHit hit;
		Vector3 point;
		if (Physics.Raycast(ray, out hit, 5000F)) {
			point = hit.point;
			lastKnownZ = hit.point.z;
		}
		else {
			Vector3 screen = Input.mousePosition;
			screen.z = lastKnownZ;
			point = camera.ScreenToWorldPoint(screen);
			//Debug.DrawLine(camera.transform.position, point, Color.red, 5f);
		}

		weapons.Target(point);

		if (Input.GetMouseButtonDown(1))
			weapons.Fire(point);
	}

	private void MoveInput()
	{
		if (movement == null)
			return;

		float forwards = Input.GetAxis("Vertical");
		float sideways = Input.GetAxis("Horizontal");
		if (forwards < 0)
			sideways *= -1;
		movement.Move(forwards, sideways);

	}
}
