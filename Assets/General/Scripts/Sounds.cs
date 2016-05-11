using UnityEngine;
using System.Collections;

public class Sounds : MonoBehaviour {

    public AudioClip powerEngine;
    public AudioClip ambient;
    private AudioSource engineSource;
    private AudioSource ambientSource;
    private float engineVolume;
    private float maxVolume;
    private float velocity;
    private Rigidbody rb;
    private float accelerating;
    
	// Use this for initialization
	void Start () {
        ambientSource = gameObject.AddComponent<AudioSource>();
        engineSource = gameObject.AddComponent<AudioSource>();

        ambientSource.loop = true;
        ambientSource.playOnAwake = true;

        ambientSource.clip = ambient;
        engineSource.clip = powerEngine;
        ambientSource.Play();
        engineVolume = 0;
        maxVolume = 20f;
        velocity = 0;
        rb = GameObject.Find("cockpit").GetComponent<Rigidbody>(); 
    }

	
	// Update is called once per frame
	void Update () {

          float velocityX = Mathf.Round(rb.velocity.x);
          float velocityY = Mathf.Round(rb.velocity.y);
          float velocityZ = Mathf.Round(rb.velocity.z);
          velocity = Mathf.Sqrt(velocityX * velocityX + velocityY * velocityY + velocityZ * velocityZ);
        
        if (velocity > accelerating)
        {
            if (!engineSource.isPlaying)
                engineSource.Play();

            else
                FadeIn();
        }

        else {
            if (engineSource.isPlaying)
               FadeOut();
        }
    }

    void FadeIn()
    {
        if (engineVolume <= maxVolume)
            engineVolume += 1f * Time.deltaTime;

        engineSource.volume = engineVolume;
    }

    void FadeOut()
    {
        if (engineVolume > 0.0f && engineVolume < 1.5f)
            engineSource.Stop();
        else
            engineVolume -= 5f * Time.deltaTime;

        engineSource.volume = engineVolume;
    }

    void OnGUI()
    {
        GUI.Label(new Rect(10, 10, 200, 100), "Velocity : " + velocity);
    }
}
