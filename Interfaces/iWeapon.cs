using UnityEngine;

public interface iWeapon
{
	void Track(Vector3 point, Transform origin);

	void Fire(Vector3 point, Transform owner, float accuracy);
}
