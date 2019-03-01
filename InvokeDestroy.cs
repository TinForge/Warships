using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InvokeDestroy : MonoBehaviour
{
	public int timer;

    void Start()
    {
		Invoke("SelfDestruct", timer);
    }

	private void SelfDestruct()
	{
		Destroy(gameObject);
	}

}
