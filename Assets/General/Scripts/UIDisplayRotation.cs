using UnityEngine;
using System.Collections;

//script to get the rotation of the spaceship in the x-, y- and z axes, and display the float values in UI text elements
public class UIDisplayRotation : MonoBehaviour {
    public UnityEngine.UI.Text displayRotationX;
    public UnityEngine.UI.Text displayRotationY;
    public UnityEngine.UI.Text displayRotationZ;
    private Transform tf;

    // Use this for initialization
    void Start () {
        tf = GameObject.Find("Cube").GetComponent<Transform>(); //replace cube with spaceship
	}
	
	// Update is called once per frame
	void Update () {
        //get transform.rotation and store
        displayRotationX.text = tf.rotation.x.ToString();
        displayRotationY.text = tf.rotation.x.ToString();
        displayRotationZ.text = tf.rotation.x.ToString();
    }
}
