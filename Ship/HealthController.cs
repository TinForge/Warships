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

		CheckSink();
	}

	public void CheckSink()
	{
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
		if (health == 0) {
			DisableShipControllers();
		}
		LibraryUI.CreateDamageCounter(transform, damage);

		float magnitude = Mathf.Lerp(100, 750, damage) / 200;	//magnitude based on damage
		float roughness = Mathf.Lerp(1,5, Ratio);					//roughness based on health remaining

		if(GetComponent<PlayerShip>() !=null)
			EZCameraShake.CameraShaker.Instance.ShakeOnce(magnitude, roughness, 0.5f, 0.5f);
	}

	private void OnCollisionEnter(Collision collision)
	{
		if(collision.transform.tag == "Ship") {
			Debug.Log(collision.relativeVelocity.magnitude * collision.rigidbody.mass);
			if (collision.relativeVelocity.magnitude > 5)
				ParseHit((int)(collision.relativeVelocity.magnitude * (collision.rigidbody.mass /1000)));
		}
	}

	private void DisableShipControllers()
	{
		foreach (iDestroyable i in GetComponentsInChildren<iDestroyable>())
			i.Destroy();

		Destroy(wc);
		Destroy(ec);
		ObjectPooler.instance.Instantiate(explosionEffect, transform.position, Quaternion.identity);

		if (GetComponent<PlayerShip>() != null)
			EZCameraShake.CameraShaker.Instance.ShakeOnce(3, 5, 1f, 5f);
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