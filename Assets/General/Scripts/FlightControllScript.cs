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

struct SwitchBoardData
{
    public bool a;
    public bool b;
    public bool c;

    public SwitchBoardData(bool a, bool b, bool c)
    {
        this.a = a;
        this.b = b;
        this.c = c;
    }
}

public class FlightControllScript : MonoBehaviour {

    private bool keepReading;
    #region Arduinoreader
    private Thread ioThread;
    private SerialPort arduino;
    private ArduinoData arduinoData;
    #endregion
    #region Switchboard
    private Thread switchBoardThread;
    private SerialPort switchBoard;
    private SwitchBoardData switchBoardData;
    #endregion
    private Rigidbody rigidBody;
    private bool buttonZLastValue;
    private float zeroInValue;
    private bool dummyMode;
    // Use this for initialization
    void Start () {
        dummyMode = false;
        buttonZLastValue = false;
        zeroInValue = 0;
        ioThread = new Thread(Poll);
        switchBoardThread = new Thread(PollSwitchboard);
        switchBoard = new SerialPort("\\\\.\\COM16", 9600);
        arduino = new SerialPort("COM3", 9600);
        arduino.DtrEnable = true;
        keepReading = true;
        arduinoData = new ArduinoData();
        switchBoardData = new SwitchBoardData(false, false, false);
        rigidBody = gameObject.GetComponent<Rigidbody>();
        ioThread.Start();
        switchBoardThread.Start();
    }
	
	// Update is called once per frame
	void Update () {
        if (switchBoardData.a)
        {
            LinearMovement();
            RotationalMovement();
        }
        DummyButtonCheck();
        DummyModeCheck();
    }

    private void DummyButtonCheck()
    {
        if (Input.GetKeyDown(KeyCode.Joystick1Button4) || Input.GetKeyDown(KeyCode.Joystick1Button5))
        {
            rigidBody.drag = 3;
            rigidBody.angularDrag = 3;
        } 
        else if (Input.GetKeyUp(KeyCode.Joystick1Button4) || Input.GetKeyUp(KeyCode.Joystick1Button5))
        {
            if (dummyMode)
            {
                rigidBody.drag = 1f;
                rigidBody.angularDrag = 1f;
            }
            else
            {
                rigidBody.drag = 0.05f;
                rigidBody.angularDrag = 0.05f;
            }
        }
    }

    private void DummyModeCheck()
    {
        if (switchBoardData.b && !dummyMode)
        {
            rigidBody.drag = 1f;
            rigidBody.angularDrag = 1f;
            dummyMode = true;
        } else if (!switchBoardData.b && dummyMode)
        {
            rigidBody.drag = 0.05f;
            rigidBody.angularDrag = 0.05f;
            dummyMode = false;
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
        rigidBody.AddRelativeForce(Vector3.up * Time.deltaTime * arduinoData.arduinoAnalogY * 25, ForceMode.Acceleration);
        rigidBody.AddRelativeForce(Vector3.left * Time.deltaTime * arduinoData.arduinoAnalogX * -25, ForceMode.Acceleration);
        if (arduinoData.arduinoButtonZ)
        {
            if (!buttonZLastValue)
            {
                zeroInValue = arduinoData.arduinoPitch;
                buttonZLastValue = true;
            }
            float throttleValue = (arduinoData.arduinoPitch - zeroInValue);
            rigidBody.AddRelativeForce(Vector3.forward * Time.deltaTime * throttleValue * 15, ForceMode.Acceleration);
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

    private void PollSwitchboard()
    {
        Debug.Log("Starting Switchboard");
        switchBoard.Open();
        switchBoard.ReadLine();
        while (keepReading && switchBoard.IsOpen)
        {
            string input = switchBoard.ReadLine();
            string[] segments = input.Split(' ');
            switchBoardData.a = Convert.ToBoolean(Convert.ToInt32(segments[0]));
            switchBoardData.b = Convert.ToBoolean(Convert.ToInt32(segments[1]));
            switchBoardData.c = Convert.ToBoolean(Convert.ToInt32(segments[2]));
        }
        switchBoard.Close();
        Debug.Log("Closing switchboard thread");
    }
    #endregion
}
