using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;

public class HealthController : MonoBehaviour, iDamageable
{
	private ShipClass sc;
	private BoatProbes bp;

	public ParticleSystem damagedEffect;
	public GameObject explosionEffect;

	public int baseHealth;
	int health;

	public float Ratio { get { return (float) health/ baseHealth; } }


    void Awake()
	{
		health = baseHealth;

		sc = GetComponent<ShipClass>();
		bp = GetComponent<BoatProbes>();
    }

	void Update()
	{
		if (health == 0)
			return;

		CheckSink();
	}

	public void CheckSink()
	{
		float z = transform.eulerAngles.z;
		if (z > 180)
			z -= 360;
		if (Mathf.Abs(z) > 40) {
			Damage(Mathf.RoundToInt(Mathf.Pow(z, 2) * Time.deltaTime));
		}
	}

	public void Damage(int damage)
	{
		health = Mathf.Clamp(health - damage, 0, baseHealth);

		foreach (iHealthChange i in GetComponentsInChildren<iHealthChange>())
			i.HealthChange(damage, Ratio);

		if (health == 0) 
			DisableShipControllers();

		LibraryUI.CreateDamageCounter(transform, damage);

		float magnitude = Mathf.Lerp(100, 750, damage) / 200;	//magnitude based on damage
		float roughness = Mathf.Lerp(1,5, Ratio);					//roughness based on health remaining

		if(GetComponent<PlayerShip>() !=null)
			EZCameraShake.CameraShaker.Instance.ShakeOnce(magnitude, roughness, 0.5f, 0.5f);
	}

	private void OnCollisionEnter(Collision collision)
	{
		if(collision.transform.tag == "Ship") {
			if (collision.relativeVelocity.magnitude > 5)
				Damage((int)(collision.relativeVelocity.magnitude * (collision.rigidbody.mass /1000)));
		}
	}

	private void DisableShipControllers()
	{
		foreach (iShipDisable i in GetComponentsInChildren<iShipDisable>())
			i.Disable();

		Destroy(this);

		ObjectPooler.instance.Instantiate(explosionEffect, transform.position, Quaternion.identity);

		if (GetComponent<PlayerShip>() != null)
			EZCameraShake.CameraShaker.Instance.ShakeOnce(3, 5, 1f, 5f);
	}


}
