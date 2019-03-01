using UnityEngine;
#if UNITY_EDITOR
using UnityEditor;
#endif

///Handle movement for ship
public class EngineController : MonoBehaviour
{
	//Components
	private Rigidbody rb;
	private BoatProbes bp;

	public Vector3 Velocity { get { return rb.velocity; } }
	public float Speed { get { return rb.velocity.magnitude; } }
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

}

/* This copy goes in BoatProbes in case it gets an update
 public void FixedUpdateEngine(float forwards, float sideways)
    {
        Vector3 forcePosition = RB.position;
        RB.AddForceAtPosition(transform.forward * _enginePower * forwards, forcePosition, ForceMode.Acceleration);
		
        Vector3 rotVec = transform.up + _turningHeel * transform.forward;
        RB.AddTorque(rotVec * _turnPower * sideways, ForceMode.Acceleration);
    }

	public void SetBuoyancy(float ratio)
	{
		_forceMultiplier = Mathf.Lerp(_baseForceMultiplier / 2, _baseForceMultiplier, ratio);
		if (ratio == 0) {
			_forceMultiplier = 0.25f;
			RB.drag = Mathf.Sqrt(RB.mass) /50; //Some arbitrary calculation
		}
	}
	*/
