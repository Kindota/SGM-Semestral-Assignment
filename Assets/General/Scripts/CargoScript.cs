﻿using UnityEngine;
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

    float cargLoadingTime = 9f;
    private float currentActionTimer = 0f;

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
        float distance = locationScript.distanceToTarget();
        Debug.Log("Distance: " + distance);
        if (distance < 50 && !cargoComplete.isPlaying)
        {
            cargoTransfer();
        }

        Debug.Log("Action timer: " + currentActionTimer);
        if (distance > 50 && currentActionTimer > 0f)
        {
            currentActionTimer = 0f;
            cargoError.Play();
            Debug.Log("Aborted:");
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

        currentActionTimer += Time.deltaTime;

        if (currentActionTimer >= cargLoadingTime)
        {
            hasCargo = !hasCargo;
            cargoComplete.Play();
            currentActionTimer = 0;
        }
    }
}
