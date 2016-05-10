using UnityEngine;
using System.Collections;
using System.IO.Ports;

public class RumbleVestScript : MonoBehaviour {

    private SerialPort rumbleVest;
	// Use this for initialization
	void Start () {
        rumbleVest = new SerialPort("\\\\.\\COM15", 9600);
        rumbleVest.Open();
	}
	
	// Update is called once per frame
	void Update () {
        int strenght =(int) Input.GetAxis("Rumble") * 255;
        Debug.Log(strenght);
        rumbleVest.Write("S" + strenght + " " + strenght + "E");
	}
}
