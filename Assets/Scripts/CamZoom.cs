using UnityEngine;
using System.Collections;

public class CamZoom : MonoBehaviour {

    bool camIsZooming = false;
    float moveSpeed = .5f;
    float zoomMarginFactor = 1.5f; 

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	}



    public void ZoomTo(Transform target)
    {
        StartCoroutine("ZoomIn", target);
    }

    private IEnumerator ZoomIn(Transform target)
    {
        camIsZooming = true;

        Vector3 targetPosition = Vector3.zero;
        float targetScale = 1f; 

        float startCameraScale = Camera.main.orthographicSize;
        Map map = target.GetComponent<Map>();
        Tile tile = target.GetComponent<Tile>();
        if (map != null)
        {
            targetPosition = new Vector3(map.WorldBounds.center.x, map.WorldBounds.center.y, transform.position.z);
            float worldSize = map.WorldBounds.extents.y;
            float margin = (map.TileWidth * zoomMarginFactor - map.TileWidth);
            targetScale =  worldSize + margin;
        }
        else if (tile != null)
        {
            targetPosition = new Vector3(target.position.x, target.position.y, transform.position.z);
            targetScale = tile.Width * zoomMarginFactor;
        }
        Debug.Log("scale " + targetScale);

        float timePercent = 0f;

        while (transform.position != targetPosition)
        {
            transform.position = Vector3.MoveTowards(transform.position, targetPosition, moveSpeed);

            timePercent += moveSpeed / Vector3.Distance(targetPosition, target.position);
            float scale = Mathf.Lerp(startCameraScale, targetScale, timePercent);
            Camera.main.orthographicSize = scale;
            Debug.Log(scale);

            yield return null;
        }
        camIsZooming = false;
    }

}
