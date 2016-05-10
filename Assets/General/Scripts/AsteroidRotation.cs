using UnityEngine;
using System.Collections;

public class AsteroidRotation : MonoBehaviour
{
    public float rotationMulitplyer = 1;
    float RotationAngleX;
    float RotationAngleY;
    float RotationAngleZ;
    // Use this for initialization
    void Start ()
    {
        RotationAngleX = Random.Range(7, 15) * rotationMulitplyer;
        RotationAngleY = Random.Range(7, 15) * rotationMulitplyer;
        RotationAngleZ = Random.Range(7, 15) * rotationMulitplyer;
    }
	
	// Update is called once per frame
	void Update ()
    {
        transform.Rotate(RotationAngleX * Time.deltaTime, RotationAngleY * Time.deltaTime, RotationAngleZ * Time.deltaTime);
    }
}
