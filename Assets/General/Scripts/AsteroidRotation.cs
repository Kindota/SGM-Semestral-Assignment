using UnityEngine;
using System.Collections;

public class AsteroidRotation : MonoBehaviour
{
    public float rotationMultiplyer = 1;
    float RotationAngleX;
    float RotationAngleY;
    float RotationAngleZ;
    // Use this for initialization
    void Start ()
    {
        RotationAngleX = Random.Range(2, 4) * rotationMultiplyer;
        RotationAngleY = Random.Range(2, 4) * rotationMultiplyer;
        RotationAngleZ = Random.Range(2, 4) * rotationMultiplyer;
    }
	
	// Update is called once per frame
	void Update ()
    {
        transform.Rotate(RotationAngleX * Time.deltaTime, RotationAngleY * Time.deltaTime, RotationAngleZ * Time.deltaTime);
    }
}
