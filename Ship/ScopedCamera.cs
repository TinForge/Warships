using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScopedCamera : MonoBehaviour
{

	private Camera camera;
	private float fov;
	public Vector3 offset;

	[SerializeField] private float lerp = 1f;
	[SerializeField] private float distance = 15;
	[SerializeField] private float minDistance = 5f;
	[SerializeField] private float maxDistance = 15f;


	void Start()
	{
		camera = Camera.main;
		fov = camera.fieldOfView;
	}

	void OnEnable()
	{
		Cursor.lockState = CursorLockMode.Locked;
		Cursor.visible = true;
	}

	float x = 0;
	float y = 0;

	void Update()
    {
		x = x + Input.GetAxis("Mouse X") * (1 + (4 * (lerp)));
		y = Mathf.Clamp(y - Input.GetAxis("Mouse Y") * (1 + (4 * (lerp))), -90, 90);
		transform.rotation = Quaternion.Euler(y, x, 0);
		transform.localPosition = Vector3.Lerp(transform.localPosition, offset, 0.025f);

		lerp = Mathf.Clamp(lerp - Input.GetAxis("Mouse ScrollWheel"), 0, 1);
		distance = Mathf.Lerp(minDistance, maxDistance, lerp);
		camera.fieldOfView = Mathf.Lerp(10,60, lerp);

	}

	void OnDisable()
	{
		Cursor.lockState = CursorLockMode.None;
		camera.fieldOfView = fov;
		Cursor.visible = true;
	}


}
