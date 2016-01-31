using UnityEngine;
using System.Collections;

public class CamZoom : MonoBehaviour {

    bool camIsZooming = false;
    float zoomSpeed = 4f;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    public IEnumerator ZoomTo(Transform target)
    {
        camIsZooming = true;
        Vector3 targetPosition = target.position;
        //float targetScale = ????

        while (transform.position != targetPosition) {
            transform.position = Vector3.MoveTowards(transform.position, targetPosition, zoomSpeed);

            //lerp scale here

            yield return null;
        }
        camIsZooming = false;
    }

}
