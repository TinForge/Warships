using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cannon : MonoBehaviour, IArmament
{
	public ProjectileData projectileData;

	[Header("Components")]
	public Transform turret;
	public Transform cannons;
	public Transform exit;
	public GameObject shell;

	[Space]

	[Header("Adjustables")]
	[SerializeField] private int minAngle = -45;
	[SerializeField] private int maxAngle = 45;
	[SerializeField] private float reloadTime = 1F;

	//Internal
	const float MaxElevation = 45F;
	const float MinElevation = -15F;
	private float traverseSpeed = 25.0f;
	private float bufferAngle = 5.0f;
	private float acceleration_Time = 0.2f;
	const float RecoilRateIn = 15;
	const float RecoilRateOut = 1;

	bool reloading;
	bool tracking;
	bool lockedX;
	bool lockedY;

	float turretAngle;
	float speedRateX;
	float cannonAngle;
	float speedRateY;

	void Awake()
	{
		projectileData.owner = transform.GetComponentInParent<ShipClass>().transform;

		string[] text = transform.parent.name.Split(" "[0]);
		int.TryParse(text[text.Length - 1], out maxAngle);
		minAngle = -maxAngle;
	}


	public void Track(Vector3 point, Transform origin)
	{
		//can track?

		TraverseTurret(point);
		TraverseCannon(point,origin);
	}

	public void Fire(Vector3 point, Transform owner)
	{
		if (reloading || !tracking || !lockedX ||!lockedY)
			return;

		reloading = true;
		Invoke("Reload", reloadTime);
		Vector3 velocity = HitTargetBySpeed(exit.position, point, Physics.gravity * projectileData.gravity, projectileData.speed);
		//owner.GetComponent<Rigidbody>().AddForceAtPosition(-velocity*100, exit.position,ForceMode.Impulse); //recoil
		EZCameraShake.CameraShaker.Instance.ShakeOnce(2, 2.5f, 0.25f, 0.6f);
		for (int i= -1; i < 2; i++) {
			GameObject projectile = ObjectPooler.instance.Instantiate(shell, exit.position+(Vector3.right*(i*2)), cannons.rotation);
			projectile.GetComponent<Projectile>().Activate(projectileData, velocity);
		}
		StartCoroutine(GunRecoil());
	}

	private void Reload()
	{
		reloading = false;
	}

	private IEnumerator GunRecoil()
	{
		float t = 0;
		Vector3 from = cannons.localPosition;
		Vector3 to = cannons.localPosition - exit.localPosition/4;
		while(t<1) {
			t += Time.deltaTime* RecoilRateIn;
			cannons.localPosition = Vector3.Lerp(from, to, t);
			yield return null;
		}
		t = 0;
		while (t < 1) {
			t += Time.deltaTime* RecoilRateOut;
			cannons.localPosition = Vector3.Lerp(to,from, t);
			yield return null;
		}
	}

	private void TraverseTurret(Vector3 targetPos)
	{
		Vector3 localTargetPos = turret.InverseTransformPoint(targetPos);
		float deltaAngle = Vector2.Angle(Vector2.up, new Vector2(localTargetPos.x, localTargetPos.z)) * Mathf.Sign(localTargetPos.x);

		if (turretAngle + deltaAngle > maxAngle || turretAngle + deltaAngle < minAngle) {//if angle is outside of bounds, return to default position
			tracking = false;
			deltaAngle = turretAngle * -1;
		}
		else
			tracking = true;

		if (Mathf.Abs(deltaAngle) > 0.01f) {
			float targetSpeedRate = Mathf.Lerp(0.0f, 1.0f, Mathf.Abs(deltaAngle) / (traverseSpeed * Time.fixedDeltaTime + bufferAngle)) * Mathf.Sign(deltaAngle);
			speedRateX = Mathf.MoveTowardsAngle(speedRateX, targetSpeedRate, Time.fixedDeltaTime / acceleration_Time);
			turretAngle += traverseSpeed * speedRateX * Time.fixedDeltaTime;
			turretAngle = Mathf.Clamp(turretAngle, minAngle, maxAngle);
			turret.localRotation = Quaternion.Euler(new Vector3(0.0f, turretAngle, 0.0f));
		}

		if (Mathf.Abs(deltaAngle) < 2f)
			lockedX = true;
		else
			lockedX = false;
	}

	private void TraverseCannon(Vector3 targetPos,Transform origin)
	{
		Vector3 velocity = HitTargetBySpeed(cannons.position, targetPos, Physics.gravity* projectileData.gravity, projectileData.speed); //The velocity direction
		Quaternion direction = Quaternion.LookRotation(velocity, Vector3.up); //Rotational form of velocity direction
		float targetAngle = direction.eulerAngles.x - origin.eulerAngles.x; //x angle - in proper format

		if (targetAngle > 180)
			targetAngle -= 360;

		if (!tracking)
			targetAngle = 0;

		float deltaAngle = targetAngle - (cannonAngle * -1);
		float targetSpeedRate = Mathf.Lerp(0.0f, 1.0f, Mathf.Abs(deltaAngle) / (traverseSpeed * Time.fixedDeltaTime + bufferAngle)) * Mathf.Sign(deltaAngle * -1); //buffer angle. Precision speed.
		speedRateY = Mathf.MoveTowardsAngle(speedRateY, targetSpeedRate, Time.fixedDeltaTime / acceleration_Time); //Interpolates between 360s
		cannonAngle += traverseSpeed * speedRateY * Time.fixedDeltaTime;
		cannonAngle = Mathf.Clamp(cannonAngle, MinElevation, MaxElevation);
		cannons.localRotation = Quaternion.Euler(cannonAngle * -1, 0, 0); //needs to be -1 since its inverted?

		if (Mathf.Abs(deltaAngle) < 2f)
			lockedY = true;
		else
			lockedY = false;
	}

	private static Vector3 GetHorizontalVector(Vector3 AtoB, Vector3 gravityBase)
	{
		Vector3 output;
		Vector3 perpendicular = Vector3.Cross(AtoB, gravityBase);
		perpendicular = Vector3.Cross(gravityBase, perpendicular);
		output = Vector3.Project(AtoB, perpendicular);
		return output;
	}

	private static Vector3 GetVerticalVector(Vector3 AtoB, Vector3 gravityBase)
	{
		Vector3 output;
		output = Vector3.Project(AtoB, gravityBase);
		return output;
	}

	private static Vector3 HitTargetBySpeed(Vector3 startPosition, Vector3 targetPosition, Vector3 gravityBase, float launchSpeed)
	{
		Vector3 AtoB = targetPosition - startPosition;
		Vector3 horizontal = GetHorizontalVector(AtoB, gravityBase);
		float horizontalDistance = horizontal.magnitude;
		Vector3 vertical = GetVerticalVector(AtoB, gravityBase);
		float verticalDistance = vertical.magnitude * Mathf.Sign(Vector3.Dot(vertical, -gravityBase));

		float x2 = horizontalDistance * horizontalDistance;
		float v2 = launchSpeed * launchSpeed;
		float v4 = launchSpeed * launchSpeed * launchSpeed * launchSpeed;

		float gravMag = gravityBase.magnitude;

		float launchTest = v4 - (gravMag * ((gravMag * x2) + (2 * verticalDistance * v2)));

		Vector3 launch;

		if (launchTest < 0) {
			launch = (horizontal.normalized * launchSpeed * Mathf.Cos(45.0f * Mathf.Deg2Rad)) - (gravityBase.normalized * launchSpeed * Mathf.Sin(45.0f * Mathf.Deg2Rad));
		}
		else {
			float tanAngle;
			tanAngle = (v2 - Mathf.Sqrt(v4 - gravMag * ((gravMag * x2) + (2 * verticalDistance * v2)))) / (gravMag * horizontalDistance);

			float finalAngle;
			finalAngle = Mathf.Atan(tanAngle);
			launch = (horizontal.normalized * launchSpeed * Mathf.Cos(finalAngle)) - (gravityBase.normalized * launchSpeed * Mathf.Sin(finalAngle));
		}

		return launch;
	}

	void OnDrawGizmos()
	{
		if (turret != null) {
			Gizmos.color = Color.grey;
			Gizmos.DrawLine(turret.position, turret.position + ((Quaternion.Euler(0, minAngle, 0) * transform.forward).normalized * 15));
			Gizmos.DrawLine(turret.position, turret.position + ((Quaternion.Euler(0, maxAngle, 0) * transform.forward).normalized * 15));

			if (!tracking)
				return;
			else if (reloading)
				Gizmos.color = Color.red;
			else if (!lockedX || !lockedY)
				Gizmos.color = Color.yellow;
			else if (lockedX && lockedY)
				Gizmos.color = Color.green;
			Gizmos.DrawSphere(turret.position + Vector3.up *4, 1);
		}
	}
}
