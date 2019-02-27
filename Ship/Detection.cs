using System.Collections;
using System.Collections.Generic;
using UnityEngine;

///
public class Detection : MonoBehaviour
{
	public List<Transform> enemyShips; //Once spotted
	public List<Transform> friendlyShips; //Always visible

	public int detectRadius;

	void Start()
    {
        
    }

	void OnMouseOver()
	{
		Debug.Log("Is targeted");
	}

	public void ScanRadius()
	{

	}

}
