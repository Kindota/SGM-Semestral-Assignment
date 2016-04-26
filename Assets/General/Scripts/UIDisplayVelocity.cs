using UnityEngine;
using System.Collections;

public class UIDisplayVelocity : MonoBehaviour {
    public UnityEngine.UI.Text displayVelocityX;
    public UnityEngine.UI.Text displayVelocityY;
    public UnityEngine.UI.Text displayVelocityZ;
    private Rigidbody rb;

    // Use this for initialization
    void Start () {
        rb = GameObject.Find("Cube").GetComponent<Rigidbody>();
	}
	
	// Update is called once per frame
	void Update () {
        //Only works if gameobject can modify its velocities in the x-, y- and z axes
        //get velocities, update floats, update display
        displayVelocityX.text = rb.velocity.x.ToString();
        displayVelocityX.text = rb.velocity.y.ToString();
        displayVelocityX.text = rb.velocity.z.ToString();
    }
}
