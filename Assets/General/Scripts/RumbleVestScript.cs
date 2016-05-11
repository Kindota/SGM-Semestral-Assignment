using UnityEngine;
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
        //int strenght = (int)((rigidBody.velocity.normalized.magnitude/2f) * 255);
        //int strenght = (int)((rigidBody.velocity.magnitude*2f) * 127.5f);
        int strenght = (int) (rigidBody.velocity.magnitude * 10f);
        if (strenght > 255) strenght = 255;
        if (strenght < 0) strenght = 0;
        SetVest(strenght);
        Debug.Log(strenght);
	}

    private void SetVest(int both)
    {
        SetVest(both, both);
    }

    void OnApplicationQuit()
    {
        SetVest(0);
        rumbleVest.Close();
    }

    private void SetVest(int left, int right)
    {
        if (rumbleVest.IsOpen)
        {
            rumbleVest.Write("S" + left + " " + right + "E");
        }
    }
}
