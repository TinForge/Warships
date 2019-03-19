using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthController : MonoBehaviour, iDamageable {

	private ShipClass shipClass;

	[SerializeField] private ParticleSystem damagedEffect;
	[SerializeField] private GameObject explosionEffect;

	public int MaxHealth { get { return shipClass.MaxHealth; } }

	public int Health { get; set; }

	public float Ratio { get { return (float) Health / MaxHealth; } }

	private void Awake () {
		shipClass = GetComponent<ShipClass> ();
		Health = MaxHealth;

		StartCoroutine(Regenerate());
		InvokeRepeating ("CapsizeCheck", 1, 0.5f);
	}

	void Start()
	{
		foreach (iHealthChange i in GetComponentsInChildren<iHealthChange>())
			i.HealthChange(Health, 0, Ratio);
	}

	public void CapsizeCheck () {
		float z = transform.eulerAngles.z;
		if (z > 180)
			z -= 360;
		if (Mathf.Abs (z) > 40)
			Damage (Mathf.RoundToInt (Mathf.Pow (z, 3) * Time.deltaTime));
	}

	private void OnCollisionEnter (Collision collision) {
		if (collision.transform.tag == "Ship")
			if (collision.relativeVelocity.magnitude > 5)
				Damage ((int) (collision.relativeVelocity.magnitude * (collision.rigidbody.mass / 1000)));
	}

	private IEnumerator Regenerate()
	{
		int healthStamp = Health;
		int counter = 0;

		while (shipClass != null) 
		{
			if (Health == MaxHealth)
				yield return null;

			if (healthStamp == Health) 
			{
				counter++;
			}
			else 
			{
				counter = 0;
				healthStamp = Health;
			}

			if (counter > 10) 
			{
				int regen = Mathf.RoundToInt(MaxHealth * 0.01f);

				int delta = Health;
				Health = Mathf.Clamp(Health + regen, 0, MaxHealth);
				delta -= Health;

				if (delta < 0)
					foreach (iHealthChange i in GetComponentsInChildren<iHealthChange>())
						i.HealthChange(Health, delta, Ratio);

				healthStamp = Health;
			}
			yield return new WaitForSeconds(1);
		}
		yield return null;
	}

	public void Damage (int damage) {
		Health = Mathf.Clamp (Health - damage, 0, MaxHealth);

		foreach (iHealthChange i in GetComponentsInChildren<iHealthChange> ())
			i.HealthChange (Health, damage, Ratio);

		if (Health == 0)
			DisableShipControllers ();

	}

	private void DisableShipControllers () {
		foreach (iShipDisable i in GetComponentsInChildren<iShipDisable> ())
			i.Disable ();

		ObjectPooler.instance.Instantiate (explosionEffect, transform.position, Quaternion.identity);

		CancelInvoke ();
		Destroy (this);
	}

}