using UnityEngine;
using System.Collections;

public class ButtonTestingScript : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	    if (Input.anyKeyDown)
        {
            foreach (KeyCode item in System.Enum.GetValues(typeof(KeyCode)))
            {
                if (Input.GetKeyDown(item))
                {
                    Debug.Log(item.ToString());
                }
            }
        }
	}
}
