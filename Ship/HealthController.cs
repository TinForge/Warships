using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;

public class HealthController : MonoBehaviour
{
	private ShipClass sc;
	private WeaponsController wc;
	private EngineController ec;
	private BoatProbes bp;

	public ParticleSystem damagedEffect;
	public GameObject explosionEffect;

	public int baseHealth;
	int health;

	public float Ratio { get { return (float) health/ baseHealth; } }

	public event Action<float> OnHealthChange;


    void Awake()
	{
		health = baseHealth;

		sc = GetComponent<ShipClass>();
		wc = GetComponent<WeaponsController>();
		ec = GetComponent<EngineController>();
		bp = GetComponent<BoatProbes>();
    }

	void Update()
	{
		if (health == 0)
			return;

		float z = transform.eulerAngles.z;
		if (z > 180)
			z -= 360;
		if (Mathf.Abs(z) > 40) {
			Debug.Log(Mathf.Abs(z));
			ParseHit(Mathf.RoundToInt(Mathf.Pow(z, 2) * Time.deltaTime));
		}
	}

	public void ParseHit(int damage)
	{
		if (health == 0)
			return;

		health = Mathf.Clamp(health - damage, 0, baseHealth);
		OnHealthChange(Ratio);
		bp.SetBuoyancy(Ratio);
		var m = damagedEffect.main;
		m.startColor = new Color(190 / 255f, 190 / 255f, 190 / 255f, Ratio);
		//Debug.Log(health);
		if (health == 0)
			DisableShipControllers();

	}

	private void DisableShipControllers()
	{
		Destroy(wc);
		Destroy(ec);
		ObjectPooler.instance.Instantiate(explosionEffect, transform.position, Quaternion.identity);
	}


}
/* This copy goes in boatprobe
public void SetBuoyancy(float ratio)
{
	_forceMultiplier = Mathf.Lerp(_baseForceMultiplier / 2, _baseForceMultiplier, ratio);
	if (ratio == 0) {
		_forceMultiplier = 0.25f;
		RB.drag = Mathf.Sqrt(RB.mass) / 50; //Some arbitrary calculation
	}
}
*/