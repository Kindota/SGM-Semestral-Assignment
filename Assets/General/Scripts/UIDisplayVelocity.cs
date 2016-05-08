using UnityEngine;
using System.Collections;

// Script to get the velocity of the spaceship in the x-, y- and z axes, and display the float values in UI text elements
public class UIDisplayVelocity : MonoBehaviour {
    public UnityEngine.UI.Text displayVelocityX;
    public UnityEngine.UI.Text displayVelocityY;
    public UnityEngine.UI.Text displayVelocityZ;
    private Rigidbody rb;

    // Use this for initialization
    void Start () {
        rb = GameObject.Find("cockpit").GetComponent<Rigidbody>(); //replace cube with spaceship
	}
	
	// Update is called once per frame
	void Update () {
        displayVelocityX.text = rb.velocity.x.ToString();
        displayVelocityX.text = rb.velocity.y.ToString();
        displayVelocityX.text = rb.velocity.z.ToString();
    }
}
