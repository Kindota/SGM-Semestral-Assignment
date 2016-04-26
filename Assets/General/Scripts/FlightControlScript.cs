using UnityEngine;
using System.Collections;

public class FlightControllScript : MonoBehaviour {

    private Rigidbody rigidBody;
    // Use this for initialization
    void Start () {
        rigidBody = gameObject.GetComponent<Rigidbody>();
    }
	
	// Update is called once per frame
	void Update () {
        LinearMovement();
        RotationalMovement();
	}

    /// <summary>
    /// method that handles rotationof the player plane
    /// </summary>
    private void RotationalMovement()
    {
        float pitch = Input.GetAxis("Pitch");
        float roll = Input.GetAxis("Roll");
        float yaw = Input.GetAxis("Yaw");

        rigidBody.AddRelativeTorque(Vector3.left * Time.deltaTime * roll, ForceMode.Acceleration);
        rigidBody.AddRelativeTorque(Vector3.forward * Time.deltaTime * pitch, ForceMode.Acceleration);
        rigidBody.AddRelativeTorque(Vector3.up * Time.deltaTime * yaw, ForceMode.Acceleration);
    }

    /// <summary>
    /// method that handles Linear movement of the player plane
    /// </summary>
    private void LinearMovement()
    {
        float forward = Input.GetAxis("Forward");
        float strafe = Input.GetAxis("Strafe");
        float vertical = Input.GetAxis("Vertical");

        rigidBody.AddRelativeForce(Vector3.left * Time.deltaTime * strafe, ForceMode.Acceleration);
        rigidBody.AddRelativeForce(Vector3.forward * Time.deltaTime * forward, ForceMode.Acceleration);
        rigidBody.AddRelativeForce(Vector3.up * Time.deltaTime * vertical, ForceMode.Acceleration);
    }
}
