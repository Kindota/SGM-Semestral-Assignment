using UnityEngine;
using System.Collections;

//script to find the distance between spaceship and target - uses distance vector

public class UITargetDistanceVector : MonoBehaviour {
    public GameObject spaceship;                //drag the spaceship gameobject to this script in unity
    public GameObject target;                   //drag the target gameobject to this script in unity
    public UnityEngine.UI.Text distanceText;    //drag UI text element to this script in unity
	
	// Update is called once per frame
    //Distance to the target object is calculated every frame 
	void Update () {
        distanceText.text = (Vector3.Distance(target.transform.position, spaceship.transform.position) / 1000).ToString() + " kilometers"; //make kilometers instead
	}
}
