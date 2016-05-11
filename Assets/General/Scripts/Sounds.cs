using UnityEngine;
using System.Collections;

public class Sounds : MonoBehaviour {

    public AudioClip powerEngine;
    public AudioClip ambient;
    private AudioSource engineSource;
    private AudioSource ambientSource;
    private Rigidbody rb;
    private float velocity;
    
	// Use this for initialization
	void Start () {
        
        rb = GameObject.Find("cockpit").GetComponent<Rigidbody>();

        ambientSource = gameObject.AddComponent<AudioSource>();
        engineSource = gameObject.AddComponent<AudioSource>();
        ambientSource.clip = ambient;
        engineSource.clip = powerEngine;

        ambientSource.PlayOneShot(ambient, 1);
        ambientSource.loop = true;
        ambientSource.playOnAwake = true;
         
    }

	
	// Update is called once per frame
	void Update () {

        float velocityX = Mathf.Round(rb.velocity.x);
        float velocityY = Mathf.Round(rb.velocity.y);
        float velocityZ = Mathf.Round(rb.velocity.z);
        velocity = Mathf.Sqrt((velocityX * velocityX) + (velocityY * velocityY) + (velocityZ * velocityZ));

        if (!engineSource.isPlaying)
                 engineSource.Play();

        engineSource.volume = velocity / 100;

    }

    void OnGUI()
    {
        GUI.Label(new Rect(10, 10, 200, 100), "Velocity : " + velocity);
    }
}
