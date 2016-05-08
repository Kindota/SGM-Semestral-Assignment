using UnityEngine;

public class UILoadLevel : MonoBehaviour {
    public Camera cam;
    private Renderer rend;
	
    //redundant Start method to remove NullReferenceException for rend field
	void Start () {
        rend = GameObject.Find("Level1").GetComponent<Renderer>();
        rend.material.color = Color.white;
    }

	void Update () {
        RaycastHit hit;
        Ray reticleRay = new Ray(cam.transform.position, cam.transform.forward);

        if (Physics.Raycast(reticleRay, out hit, 100.0f)) {
            if (hit.collider.tag == "Level1") {
                rend = hit.collider.GetComponent<Renderer>();
                rend.material.color = Color.cyan;
                //TODO: if-statement to check for button press and load level
                //UnityEngine.SceneManagement.SceneManager.LoadScene(1);
            } else if (hit.collider.tag == "Level2") {
                rend = hit.collider.GetComponent<Renderer>();
                rend.material.color = Color.cyan;
                //TODO: if-statement to check for button press and load level
                //UnityEngine.SceneManagement.SceneManager.LoadScene(2);
            }
            else if (!(hit.collider.tag == "Level1" || hit.collider.tag == "Level2")) {
                rend.material.color = Color.white;
            }
        }   
    }
}
