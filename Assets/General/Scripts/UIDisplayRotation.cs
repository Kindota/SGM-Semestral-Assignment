using UnityEngine;
using System.Collections;

//script to get the rotation of the spaceship in the x-, y- and z axes, and display the float values in UI text elements
public class UIDisplayRotation : MonoBehaviour {
    public UnityEngine.UI.Text displayRotationX;
    public UnityEngine.UI.Text displayRotationY;
    public UnityEngine.UI.Text displayRotationZ;
    private Transform tf;

    void Start() {
        tf = GameObject.Find("cockpit").GetComponent<Transform>();
    }
	// Update is called once per frame
	void Update () {
        //get transform.rotation and store 
        displayRotationX.text = Mathf.Round(tf.rotation.eulerAngles.x).ToString();
        displayRotationY.text = Mathf.Round(tf.rotation.eulerAngles.y).ToString();
        displayRotationZ.text = Mathf.Round(tf.rotation.eulerAngles.z).ToString();
    }
}
