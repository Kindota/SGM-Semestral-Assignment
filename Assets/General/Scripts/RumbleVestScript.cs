﻿using UnityEngine;
using System.Collections;
using System.IO.Ports;

public class RumbleVestScript : MonoBehaviour {

    private SerialPort rumbleVest;
    private Rigidbody rigidBody;
	// Use this for initialization
	void Start () {
        rigidBody = gameObject.GetComponent<Rigidbody>();
        rumbleVest = new SerialPort("\\\\.\\COM15", 9600);
        rumbleVest.Open();
	}
	
	// Update is called once per frame
	void Update () {
        int strenght = (int)((rigidBody.velocity.normalized.magnitude/2f) * 255);
        SetVest(strenght);
        Debug.Log(strenght);
	}

    private void SetVest(int both)
    {
        SetVest(both, both);
    }

    private void SetVest(int left, int right)
    {
        rumbleVest.Write("S" + left + " " + right + "E");
    }
}
