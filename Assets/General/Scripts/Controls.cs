using UnityEngine;
using System.Collections;

public class Controls : MonoBehaviour {

    public AudioClip sound;
    private AudioSource source;

    void Awake()
    {
        source = GetComponent<AudioSource>();
    }

	// Use this for initialization
	void Start () {
        

    }
	
	// Update is called once per frame
	void Update () {
        source.PlayOneShot(sound, 0.05f);
        if (Input.GetKey(KeyCode.UpArrow))
        {
            transform.position += Vector3.up * 0.7f * Time.deltaTime;
        }
        
    }
}
