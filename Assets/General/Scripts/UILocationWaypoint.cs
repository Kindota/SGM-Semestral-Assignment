using UnityEngine;

public class UILocationWaypoint : MonoBehaviour
{
    public Camera cam;                          //drag the main camera to the script in the inspector
    public GameObject spaceship;                //drag the spaceship gameobject to the script in the inspector
    private Renderer rend1, rend2, rend3;       //button renderers used to change the color
    private GameObject arrow;                   //arrow prefab
    private Transform target;                   //transform of target space stations       
    public UnityEngine.UI.Text distanceText;    //UI text for distance to target space station


    void Start ()
    {
        arrow = GameObject.Find("Arrow");
        rend1 = GameObject.Find("Location1").GetComponent<Renderer>();
        rend2 = GameObject.Find("Location2").GetComponent<Renderer>();
        rend3 = GameObject.Find("Location3").GetComponent<Renderer>();
    }

    void Update() 
    {
        RaycastHit hit;                                                             //to store data from collisions
        Ray ray = new Ray(cam.transform.position, cam.transform.forward);           //define ray
        Physics.Raycast(ray, out hit, 50.0f);                                       //shoot ray

        try
        {
            if (hit.collider.tag == "Location1")
            {
                rend1.material.color = Color.cyan;                                  //
                rend2.material.color = Color.white;                                 //render button colors
                rend3.material.color = Color.white;                                 //
                target = GameObject.Find("Platform1").GetComponent<Transform>();    //get transform from platform1
                distanceText.text = (Vector3.Distance(target.position,
                                        spaceship.transform.position) / 1000).ToString("F2") + " km"; //distance vector to display distance in kilometers
            }
            else if (hit.collider.tag == "Location2")
            {
                rend1.material.color = Color.white;
                rend2.material.color = Color.cyan;
                rend3.material.color = Color.white;
                target = GameObject.Find("Platform2").GetComponent<Transform>();
                distanceText.text = (Vector3.Distance(target.position,
                                        spaceship.transform.position) / 1000).ToString("F2") + " km"; //distance vector to display distance in kilometers
            }
            else if (hit.collider.tag == "Location3")
            {
                rend1.material.color = Color.white;
                rend2.material.color = Color.white;
                rend3.material.color = Color.cyan;
                target = GameObject.Find("Platform3").GetComponent<Transform>();
                distanceText.text = (Vector3.Distance(target.position,
                                        spaceship.transform.position) / 1000).ToString("F2") + " km"; //distance vector to display distance in kilometers
            }
            arrow.transform.LookAt(target.position);    //points the arrow prefab in the direction of the target space station
        }
        catch (System.NullReferenceException) { }
    }
}
