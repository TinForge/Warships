using UnityEngine;

public class RNG : MonoBehaviour
{
	public static int Damage(int damage, float deviation)
	{
		return Mathf.RoundToInt(damage * Random.Range(1 - deviation, 1 + deviation));
	}

}