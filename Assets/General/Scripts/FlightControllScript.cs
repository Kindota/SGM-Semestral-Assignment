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
    private bool buttonZLastValue;
    private float zeroInValue;
    // Use this for initialization
    void Start () {
        buttonZLastValue = false;
        zeroInValue = 0;
        ioThread = new Thread(Poll);
        arduino = new SerialPort("COM3", 9600);
        arduino.DtrEnable = true;
        keepReading = true;
        arduinoData = new ArduinoData();
        rigidBody = gameObject.GetComponent<Rigidbody>();
        ioThread.Start();
    }
	
	// Update is called once per frame
	void Update () {
        LinearMovement();
        RotationalMovement();
        DummyButtonCheck();

    }

    private void DummyButtonCheck()
    {
        if (Input.GetKeyDown(KeyCode.Joystick1Button4) || Input.GetKeyDown(KeyCode.Joystick1Button5))
        {
            rigidBody.drag = 5;
            rigidBody.angularDrag = 5;
        } 
        else if (Input.GetKeyUp(KeyCode.Joystick1Button4) || Input.GetKeyUp(KeyCode.Joystick1Button5))
        {
            rigidBody.drag = 0;
            rigidBody.angularDrag = 0.05f;
        }
    }

    /// <summary>
    /// method that handles rotationof the player plane
    /// </summary>
    private void RotationalMovement()
    {
        float pitch = Input.GetAxis("Pitch");
        float roll = Input.GetAxis("Roll");
        float yaw = Input.GetAxis("Yaw");
        //Debug.Log("Pitch " + pitch + " roll " + roll + " yaw " + yaw);
        rigidBody.AddRelativeTorque(Vector3.forward * Time.deltaTime * roll * 25, ForceMode.Acceleration);
        rigidBody.AddRelativeTorque(Vector3.right * Time.deltaTime * pitch * 25, ForceMode.Acceleration);
        rigidBody.AddRelativeTorque(Vector3.up * Time.deltaTime * yaw * 25, ForceMode.Acceleration);
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
        rigidBody.AddRelativeForce(Vector3.up * Time.deltaTime * arduinoData.arduinoAnalogY * 100, ForceMode.Acceleration);
        rigidBody.AddRelativeForce(Vector3.left * Time.deltaTime * arduinoData.arduinoAnalogX * -100, ForceMode.Acceleration);
        if (arduinoData.arduinoButtonZ)
        {
            if (!buttonZLastValue)
            {
                zeroInValue = arduinoData.arduinoPitch;
                buttonZLastValue = true;
            }
            float throttleValue = (arduinoData.arduinoPitch - zeroInValue);
            rigidBody.AddRelativeForce(Vector3.forward * Time.deltaTime * throttleValue * 50, ForceMode.Acceleration);
            Debug.Log(arduinoData.arduinoPitch - zeroInValue);
        }
        else
        {
            buttonZLastValue = false;
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
                //Debug.Log(input);
                string[] segments = input.Split(' ');
                arduinoData.arduinoButtonZ = Convert.ToBoolean(Convert.ToInt32(segments[1]));
                arduinoData.arduinoButtonC = Convert.ToBoolean(Convert.ToInt32(segments[3]));
                arduinoData.arduinoPitch = Convert.ToSingle(segments[7]);
                arduinoData.arduinoRoll = Convert.ToSingle(segments[5]);
                arduinoData.arduinoAnalogX = Convert.ToSingle(segments[9]) - 125f;
                arduinoData.arduinoAnalogY = Convert.ToSingle(segments[11]) - 130f;
                if (arduinoData.arduinoAnalogX < 5 && arduinoData.arduinoAnalogX > -5) arduinoData.arduinoAnalogX = 0;
                if (arduinoData.arduinoAnalogY < 5 && arduinoData.arduinoAnalogY > -5) arduinoData.arduinoAnalogY = 0;
            }
        }
        arduino.Close();
        Debug.Log("Ending Poll Thread");
    }
    #endregion
}
