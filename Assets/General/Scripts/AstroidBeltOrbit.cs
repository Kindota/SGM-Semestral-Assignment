using UnityEngine;
using System.Collections;

public class AstroidBeltOrbit : MonoBehaviour {
    public GameObject[] asteroidBelts = new GameObject[9];
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update ()
    {
        asteroidBelts[0].transform.Rotate(0, 0.25f * Time.deltaTime, 0);
        asteroidBelts[1].transform.Rotate(0, 0.50f * Time.deltaTime, 0);
        asteroidBelts[2].transform.Rotate(0, 0.75f * Time.deltaTime, 0);
        asteroidBelts[3].transform.Rotate(0, -.25f * Time.deltaTime, 0);
        asteroidBelts[4].transform.Rotate(0, -.50f * Time.deltaTime, 0);
        asteroidBelts[5].transform.Rotate(0, -.75f * Time.deltaTime, 0);
        asteroidBelts[6].transform.Rotate(0, 0.25f * Time.deltaTime, 0);
        asteroidBelts[7].transform.Rotate(0, 0.50f * Time.deltaTime, 0);
        asteroidBelts[8].transform.Rotate(0, 0.75f * Time.deltaTime, 0);
    }
}
