using UnityEngine;
using System.Collections;

public class AsteroidRotation : MonoBehaviour
{
    int RorationAngleX;
    int RorationAngleY;
    int RorationAngleZ;
    // Use this for initialization
    void Start ()
    {
        RorationAngleX = Random.Range(4, 10);
        RorationAngleY = Random.Range(4, 10);
        RorationAngleZ = Random.Range(4, 10);
    }
	
	// Update is called once per frame
	void Update ()
    {
        transform.eulerAngles += new Vector3(RorationAngleX, RorationAngleY, 0) * Time.deltaTime;
    }
}
