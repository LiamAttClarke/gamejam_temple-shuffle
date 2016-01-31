using UnityEngine;
using System.Collections;

public class CamZoom : MonoBehaviour {

    bool camIsZooming = false;
    float moveSpeed = 4f;
    float zoomMarginFactor = 1.5f; 

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
        
        float targetScale;
        Map map = target.GetComponent<Map>();
        Tile tile = target.GetComponent<Tile>();
        if (map != null) {
            targetScale = map.TileWidth * map.worldSize + (map.TileWidth * zoomMarginFactor - map.TileWidth);
        }
        else if (tile != null) {
            targetScale = tile.Width * zoomMarginFactor;
        }


        while (transform.position != targetPosition) {
            transform.position = Vector3.MoveTowards(transform.position, targetPosition, moveSpeed);

            //lerp scale here
            //how does i lerp
            //Mathf.Lerp(,,1/moveSpeed)

            yield return null;
        }
        camIsZooming = false;
    }

}
