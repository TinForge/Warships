using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraShake : MonoBehaviour
{

	public void Start()
	{
		if (GetComponent<PlayerShip>() == null)
			Destroy(this);
	}


}
