using UnityEngine;

public interface IArmament
{
	void Track(Vector3 point, Transform origin);

	void Fire(Vector3 point, Transform owner);
}
