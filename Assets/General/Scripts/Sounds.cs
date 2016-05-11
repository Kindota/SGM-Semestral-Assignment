using UnityEngine;
using System.Collections;

public class Sounds : MonoBehaviour {

    public AudioClip powerEngine;
    public AudioClip ambient;
    private AudioSource engineSource;
    private AudioSource ambientSource;
    private Rigidbody rb;
    private float velocity;
    private float rotation;
    private Transform tf;

    void Awake()
    {
        ambientSource = GetComponent<AudioSource>();
    }

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
        tf = GameObject.Find("cockpit").GetComponent<Transform>();
    }

	
	// Update is called once per frame
	void Update () {

        float velocityX = Mathf.Round(rb.velocity.x);
        float velocityY = Mathf.Round(rb.velocity.y);
        float velocityZ = Mathf.Round(rb.velocity.z);
        velocity = Mathf.Sqrt((velocityX * velocityX) + (velocityY * velocityY) + (velocityZ * velocityZ));

        float rotationX = tf.rotation.eulerAngles.x;
        float rotationY = tf.rotation.eulerAngles.y;
        float rotationZ= tf.rotation.eulerAngles.z;
        rotation = Mathf.Sqrt((rotationX * rotationX) + (rotationY * rotationY) + (rotationZ * rotationZ));

        if (!engineSource.isPlaying)
                 engineSource.Play();

        if(velocity != 0)
            engineSource.volume = velocity / 100;

        else
            engineSource.volume = rotation / 100;

    }

    void OnGUI()
    {
        GUI.Label(new Rect(10, 10, 200, 100), "Velocity : " + velocity);
    }
}
