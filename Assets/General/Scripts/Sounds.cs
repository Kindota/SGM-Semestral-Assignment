using UnityEngine;
using System.Collections;

public class Sounds : MonoBehaviour {

    public AudioClip speedUp;
    public AudioClip powerEngine;
    public AudioClip slowDown;
    public AudioClip ambient;
    private AudioSource accelleration;
    private AudioSource deaccelleration;
    private AudioSource engineSource;
    private AudioSource ambientSource;


    void Awake()
    {

        ambientSource = gameObject.AddComponent<AudioSource>();
        accelleration = gameObject.AddComponent<AudioSource>();
        deaccelleration = gameObject.AddComponent<AudioSource>();
        engineSource = gameObject.AddComponent<AudioSource>();

        ambientSource.loop = true;
        ambientSource.playOnAwake = true;
        ambientSource.clip = ambient;
        engineSource.clip = powerEngine;
        deaccelleration.clip = slowDown;
        accelleration.clip = speedUp;
    }

	// Use this for initialization
	void Start () {
        ambientSource.Play();
    }

	
	// Update is called once per frame
	void Update () {

        if (!(accelleration.isPlaying || deaccelleration.isPlaying))
        {
            ambientSource.PlayOneShot(ambient, 0.03f);
            ambientSource.loop = true;
        }
        if (Input.GetKey(KeyCode.UpArrow))
        {
            if (deaccelleration.isPlaying)
                deaccelleration.Stop();

            transform.position += Vector3.up * 1.5f * Time.deltaTime;

            if (!accelleration.isPlaying)
            {
                accelleration.PlayOneShot(powerEngine, 0.5f);
            }
        }
     
        if(Input.GetKeyUp(KeyCode.UpArrow))
        {
            if (accelleration.isPlaying)
                accelleration.Stop();

            deaccelleration.PlayOneShot(slowDown, 1f);
        }   
    }
}
