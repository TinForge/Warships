using UnityEngine;
#if UNITY_EDITOR
using UnityEditor;
#endif

///Handle movement for ship
public class MovementController : MonoBehaviour, iShipDisable
{
	//Components
	private Rigidbody rb;
	private BoatProbes bp;

	public Vector3 Velocity { get { return rb.velocity; } }
	public float Magnitude { get { return rb.velocity.magnitude; } }
	private bool Grounded { get { return transform.position.y < 7.5f; } }
	
	void Awake()
	{
		rb = GetComponent<Rigidbody>();
		bp = GetComponent<BoatProbes>();
	}

	public void Move(float forwards, float sideways)
	{
		bp.FixedUpdateEngine(forwards, sideways);
	}

	public void Disable()
	{
		Destroy(this);
	}

}
