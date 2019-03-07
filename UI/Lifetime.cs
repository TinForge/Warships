using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class sLifetime : MonoBehaviour, iShipDisable
{
	public bool onAwake;
	public int lifetime;

    void Start()
    {
		if(onAwake)
		Invoke("SelfDestruct", lifetime);
    }

	public void Disable()
	{
		Invoke("SelfDestruct", lifetime);
	}

	private void SelfDestruct()
	{
		Destroy(gameObject);
	}
}
