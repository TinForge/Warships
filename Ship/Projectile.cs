using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Contains projectile stats. Doesn't include velocity because of pass by ref.
/// Declared on Cannon.
/// </summary>
[System.Serializable]
public class ProjectileData
{
	public int damage;
	public int speed;
	public int gravity;
	[Range(0f, 0.99f)]public float accuracy;

	[HideInInspector] public Transform owner;
}

/// <summary>
/// Handles lifetime
/// </summary>
public class Projectile : MonoBehaviour
{
	[Header("Components")]
	public Rigidbody rb;
	public Transform mesh;
	public GameObject fireEffect;
	public GameObject hitEffect;
	public GameObject waterEffect;

	private ProjectileData projectileData;

	private const float LifeTime = 10F;
	private const float BaseSpread = 25F;
	private bool Grounded { get { return transform.position.y < 0; } }

	public void Activate(ProjectileData projectileData, Vector3 velocity)
	{
		gameObject.SetActive(true);
		this.projectileData = projectileData;

		Vector3 rng = Random.insideUnitSphere * (BaseSpread * (1 - projectileData.accuracy));
		rb.velocity = Vector3.zero;
		rb.AddForce(velocity + rng, ForceMode.VelocityChange);

		ObjectPooler.instance.Instantiate(fireEffect, transform.position,Quaternion.LookRotation(velocity));
		Invoke("Recycle", LifeTime);
	}

	void Update()
	{
		mesh.forward = rb.velocity;

		if(Grounded) {
			ObjectPooler.instance.Instantiate(waterEffect, transform.position, Quaternion.identity);
			Recycle();
		}
	}

	void FixedUpdate()
	{
		rb.AddForce(Physics.gravity * projectileData.gravity * rb.mass);
	}

	void OnTriggerEnter(Collider other)
	{
		if (other.transform == projectileData.owner)
			return;
		if (other.transform.tag == "Ship")
			other.transform.GetComponent<HealthController>().ParseHit(projectileData.damage);

		ObjectPooler.instance.Instantiate(hitEffect, transform.position, Quaternion.Euler(-mesh.forward));
		Recycle();
	}

	private void Recycle()
	{
		CancelInvoke();
		gameObject.SetActive(false);
	}


}
