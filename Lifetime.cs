using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Lifetime : MonoBehaviour, iDestruct
{
	public bool onAwake;
	public int lifetime;

    void Start()
    {
		if(onAwake)
		Invoke("SelfDestruct", lifetime);
    }

	public void Destruct()
	{
		Invoke("SelfDestruct", lifetime);
	}

	private void SelfDestruct()
	{
		Destroy(gameObject);
	}
}
