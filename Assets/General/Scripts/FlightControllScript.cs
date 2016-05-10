using UnityEngine;
using System;
using System.Threading;
using System.IO.Ports;

struct ArduinoData
{
    public float arduinoPitch;
    public float arduinoRoll;
    public float arduinoAnalogX;
    public float arduinoAnalogY;
    public bool arduinoButtonZ;
    public bool arduinoButtonC;

    public ArduinoData(float arduinoPitch, float arduinoRoll, float arduinoAnalogX, float arduinoAnalogY, bool arduinoButtonZ, bool arduinoButtonC)
    {
        this.arduinoPitch = arduinoPitch;
        this.arduinoRoll = arduinoRoll;
        this.arduinoAnalogX = arduinoAnalogX;
        this.arduinoAnalogY = arduinoAnalogY;
        this.arduinoButtonZ = arduinoButtonZ;
        this.arduinoButtonC = arduinoButtonC;
    }
}

public class FlightControllScript : MonoBehaviour {

    #region Arduinoreader
    private Thread ioThread;
    private SerialPort arduino;
    private bool keepReading;
    private ArduinoData arduinoData;
    #endregion
    private Rigidbody rigidBody;
    // Use this for initialization
    void Start () {
        ioThread = new Thread(Poll);
        arduino = new SerialPort("COM4", 9600);
        arduino.DtrEnable = true;
        keepReading = true;
        arduinoData = new ArduinoData();
        rigidBody = gameObject.GetComponent<Rigidbody>();
        //ioThread.Start();
    }
	
	// Update is called once per frame
	void Update () {
        LinearMovement();
        RotationalMovement();
	}

    /// <summary>
    /// method that handles rotationof the player plane
    /// </summary>
    private void RotationalMovement()
    {
        float pitch = Input.GetAxis("Pitch");
        float roll = Input.GetAxis("Roll");
        float yaw = Input.GetAxis("Yaw");
        Debug.Log("Pitch " + pitch + " roll " + roll + " yaw " + yaw);
        rigidBody.AddRelativeTorque(Vector3.forward * Time.deltaTime * roll * 250, ForceMode.Acceleration);
        rigidBody.AddRelativeTorque(Vector3.right * Time.deltaTime * pitch * 250, ForceMode.Acceleration);
        rigidBody.AddRelativeTorque(Vector3.up * Time.deltaTime * yaw * 250, ForceMode.Acceleration);
    }

    /// <summary>
    /// method that handles Linear movement of the player plane
    /// </summary>
    private void LinearMovement()
    {
        /*float forward = Input.GetAxis("Forward");
        float strafe = Input.GetAxis("Strafe");
        float vertical = Input.GetAxis("Vertical");*/

        /*rigidBody.AddRelativeForce(Vector3.left * Time.deltaTime * strafe, ForceMode.Acceleration);
        rigidBody.AddRelativeForce(Vector3.forward * Time.deltaTime * forward, ForceMode.Acceleration);
        rigidBody.AddRelativeForce(Vector3.up * Time.deltaTime * vertical, ForceMode.Acceleration);*/

        rigidBody.AddRelativeForce(Vector3.forward * Time.deltaTime * arduinoData.arduinoAnalogY * 25, ForceMode.Acceleration);
        if (arduinoData.arduinoButtonZ)
        {
           rigidBody.AddRelativeForce(Vector3.left * Time.deltaTime * arduinoData.arduinoRoll * 25, ForceMode.Acceleration);
            rigidBody.AddRelativeForce(Vector3.up * Time.deltaTime * arduinoData.arduinoPitch * 25, ForceMode.Acceleration);
        }
    }

    #region ArduinoPoll

    void OnApplicationQuit()
    {
        keepReading = false;
    }

    private void Poll()
    {
        Debug.Log("Starting Poll Thread");
        arduino.Open();
        arduino.ReadLine();
        while (keepReading)
        {
            if (arduino.IsOpen)
            {
                string input = arduino.ReadLine();
                Debug.Log(input);
                string[] segments = input.Split(' ');
                arduinoData.arduinoButtonZ = Convert.ToBoolean(Convert.ToInt32(segments[1]));
                arduinoData.arduinoButtonC = Convert.ToBoolean(Convert.ToInt32(segments[3]));
                arduinoData.arduinoPitch = Convert.ToSingle(segments[5]);
                arduinoData.arduinoRoll = Convert.ToSingle(segments[7]);
                arduinoData.arduinoAnalogX = Convert.ToSingle(segments[9]) - 125f;
                arduinoData.arduinoAnalogY = Convert.ToSingle(segments[11]) - 130f;
            }
        }
        arduino.Close();
        Debug.Log("Ending Poll Thread");
    }
    #endregion
}
