using UnityEngine;
using System.Collections;

public class CargoScript : MonoBehaviour {

    public UILocationWaypoint locationScript;
    public AudioClip cargoLoadingClip;
    public AudioClip cargoUnloadingClip;
    public AudioClip cargoErrorClip;
    public AudioClip cargoCompleteClip;
    AudioSource cargoLoading;
    AudioSource cargoUnloading;
    AudioSource cargoError;
    AudioSource cargoComplete;

    float cargLoadingTime = 15f;
    float currentActionTimer = 0f;

    bool hasCargo = false;
    // Use this for initialization
    void Start ()
    {
        cargoLoading = gameObject.AddComponent<AudioSource>();
        cargoUnloading = gameObject.AddComponent<AudioSource>();
        cargoError = gameObject.AddComponent<AudioSource>();
        cargoComplete = gameObject.AddComponent<AudioSource>();

        cargoLoading.clip = cargoLoadingClip;
        cargoUnloading.clip = cargoUnloadingClip;
        cargoError.clip = cargoErrorClip;
        cargoComplete.clip = cargoCompleteClip;
    }
	
	// Update is called once per frame
	void Update () {
        Debug.Log(locationScript.distanceToTarget());

        if (locationScript.distanceToTarget() < 0.2f)
        {
            cargoTransfer();
        }

        if(locationScript.distanceToTarget() > 0.2f && currentActionTimer != 0)
        {
            currentActionTimer = 0;
            cargoError.Play();
        }
	}

    void cargoTransfer()
    {
        if(currentActionTimer == 0)
        {
            if (hasCargo)
            {
                cargoUnloading.Play();
            }
            else
            {
                cargoLoading.Play();
            }
        }

        if (currentActionTimer >= cargLoadingTime)
        {
            hasCargo = !hasCargo;
            cargoComplete.Play();
        }

        currentActionTimer += Time.deltaTime;
    }
}
