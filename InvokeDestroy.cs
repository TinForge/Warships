using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InvokeDestroy : MonoBehaviour
{
	public int lifetime;

    void Start()
    {
		Invoke("SelfDestruct", lifetime);
    }

	private void SelfDestruct()
	{
		Destroy(gameObject);
	}

}
