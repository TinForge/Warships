using UnityEngine;
using System.Collections;

///Player Input for all ship components
public class PlayerShip : MonoBehaviour
{
	public static PlayerShip instance;

	private EngineController engine;
	private WeaponsController weapons;

	public bool useTargettingSystem;
	public bool targetAcquired;

	private new Camera camera;
	private float lastKnownZ;

	private bool scoped;


	private void Awake()
	{
		instance = this;
	}

	void Start()
	{
		camera = Camera.main;
		engine = GetComponent<EngineController>();
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

		if (Input.GetKeyDown(KeyCode.F))
		ToggleScope();



		if (useTargettingSystem)
			Targetting();
		else
			MouseInput();
		MoveInput();
	}

	private void ToggleScope()
	{
		scoped = !scoped;

		camera.transform.parent.GetComponent<Orbital>().enabled = !scoped;
		camera.transform.parent.GetComponent<ScopedCamera>().enabled = scoped;


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
		if (engine == null)
			return;
		
		float forwards = Input.GetAxis("Vertical");
		float sideways = Input.GetAxis("Horizontal");
		if (forwards < 0)
			sideways *= -1;
		engine.Move(forwards, sideways);

	}

}
