using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Lifetime : MonoBehaviour, iDestroyable
{
	public bool onAwake;
	public int lifetime;

    void Start()
    {
		if(onAwake)
		Invoke("SelfDestruct", lifetime);
    }

	public void Destroy()
	{
		Invoke("SelfDestruct", lifetime);
	}

	private void SelfDestruct()
	{
		Destroy(gameObject);
	}
}
