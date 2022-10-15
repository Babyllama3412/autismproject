using UnityEngine;
using System.Collections;

//<summary>
//Ball movement controlls and simple third-person-style camera
//</summary>
public class RollerBall : MonoBehaviour {
	public float rollSpeed = 10;

	public bool allowRoll;
	private Rigidbody mRigidBody = null;
	private bool mFloorTouched = false;

	void Start () {
		mRigidBody = GetComponent<Rigidbody> ();
	}

	void FixedUpdate () {
		if (mRigidBody != null && allowRoll) {
			if (Input.GetButton ("Horizontal")) {
				mRigidBody.AddTorque(Vector3.back * Input.GetAxis("Horizontal")*rollSpeed);
			}
			if (Input.GetButton ("Vertical")) {
				mRigidBody.AddTorque(Vector3.right * Input.GetAxis("Vertical")*rollSpeed);
			}
		}
	}

	public void AllowRoll(bool active)
	{
		allowRoll = active;
	}
}
