using UnityEngine;
using System.Collections;

public class Controls : MonoBehaviour {

    public AudioClip sound;
    public AudioClip powerEngine;
    public AudioClip slowDown;
    private AudioSource accelleration;
    private AudioSource deaccelleration;
    private AudioSource idle;
    private AudioSource stagnation;


    void Awake()
    {

        idle = gameObject.AddComponent<AudioSource>();
        idle.loop = true;
        idle.playOnAwake = true;
        idle.clip = sound;

        accelleration = gameObject.AddComponent<AudioSource>();
        deaccelleration = gameObject.AddComponent<AudioSource>();

    }

	// Use this for initialization
	void Start () {
        idle.Play();
    }

	
	// Update is called once per frame
	void Update () {

        if (!(accelleration.isPlaying || deaccelleration.isPlaying))
        {
            idle.PlayOneShot(sound, 0.03f);
            idle.loop = true;
        }
        if (Input.GetKey(KeyCode.UpArrow))
        {
            if (deaccelleration.isPlaying)
                deaccelleration.Stop();

            transform.position += Vector3.up * 0.7f * Time.deltaTime;

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
