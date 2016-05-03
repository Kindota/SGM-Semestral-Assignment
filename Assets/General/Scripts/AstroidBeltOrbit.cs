using UnityEngine;
using System.Collections;

public class AstroidBeltOrbit : MonoBehaviour {
    public GameObject[] asteroidBelts = new GameObject[3];
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update ()
    {
        asteroidBelts[0].transform.eulerAngles += new Vector3(0, 1.50f, 0) * Time.deltaTime;
        asteroidBelts[1].transform.eulerAngles += new Vector3(0, 1, 0) * Time.deltaTime;
        asteroidBelts[2].transform.eulerAngles += new Vector3(0, 0.50f, 0) * Time.deltaTime;
    }
}
