using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;

public class HealthController : MonoBehaviour, iDamageable
{
	private ShipClass sc;

	[SerializeField] private ParticleSystem damagedEffect;
	[SerializeField] private GameObject explosionEffect;

	public int MaxHealth { get { return sc.Health; } }

	public int Health { get; set; }

	public float Ratio { get { return (float) Health / MaxHealth; } }


    private void Awake()
	{
		sc = GetComponent<ShipClass>();
	}

	private void Start()
	{
		InvokeRepeating("Regenerate", 0, 5);
		Health = MaxHealth;
	}

	private void Update()
	{
		if (Health == 0)
			return;
		TippedOver();
	}

	public void TippedOver()
	{
		float z = transform.eulerAngles.z;
		if (z > 180)
			z -= 360;
		if (Mathf.Abs(z) > 40)
			Damage(Mathf.RoundToInt(Mathf.Pow(z, 2) * Time.deltaTime));
	}

	private void OnCollisionEnter(Collision collision)
	{
		if (collision.transform.tag == "Ship")
			if (collision.relativeVelocity.magnitude > 5)
				Damage((int) (collision.relativeVelocity.magnitude * (collision.rigidbody.mass / 1000)));
	}

	private void Regenerate()
	{
		int regen = Mathf.RoundToInt(MaxHealth * 0.01f);

		int delta = Health;
		Health = Mathf.Clamp(Health + regen, 0, MaxHealth);
		delta -= Health;

		if (delta < 0)
		foreach (iHealthChange i in GetComponentsInChildren<iHealthChange>())
			i.HealthChange(delta, Ratio);
	}

	public void Damage(int damage)
	{
		Health = Mathf.Clamp(Health - damage, 0, MaxHealth);

		foreach (iHealthChange i in GetComponentsInChildren<iHealthChange>())
			i.HealthChange(damage, Ratio);

		if (Health == 0) 
			DisableShipControllers();

	}

	private void DisableShipControllers()
	{

		foreach (iShipDisable i in GetComponentsInChildren<iShipDisable>())
			i.Disable();

		ObjectPooler.instance.Instantiate(explosionEffect, transform.position, Quaternion.identity);

		CancelInvoke();
		Destroy(this);
	}


}
