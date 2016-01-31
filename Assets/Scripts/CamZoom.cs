using UnityEngine;
using System.Collections;

public class CamZoom : MonoBehaviour {

    public bool IsZooming { get; private set; }
    float moveSpeed = .5f;
    float zoomMarginFactor = 1.1f;
    Map map;

    void Awake() {
        IsZooming = false;
    }

	// Use this for initialization
	void Start () {
        map = GameObject.Find("Map").GetComponent<Map>();
        Vector3 camStartPos = map.alterTile.transform.position;
        camStartPos.z = -10f;
        transform.position = camStartPos;
    }

    public void ZoomTo(Transform target)
    {
        StartCoroutine("ZoomIn", target);
    }

    private IEnumerator ZoomIn(Transform target)
    {
        IsZooming = true;

        Vector3 targetPosition = Vector3.zero;
        float targetScale = 1f; 

        float startCameraScale = Camera.main.orthographicSize;
        Map map = target.GetComponent<Map>();
        Tile tile = target.GetComponent<Tile>();
        if (map != null)
        {
            targetPosition = new Vector3(map.WorldBounds.center.x, map.WorldBounds.center.y, transform.position.z);
            //float worldSize = map.WorldBounds.extents.y;
            //float margin = (map.TileWidth * zoomMarginFactor - map.TileWidth);
            targetScale = map.WorldBounds.size.x / 2;
        }
        else if (tile != null)
        {
            targetPosition = new Vector3(target.position.x, target.position.y, transform.position.z);
            targetScale = tile.Width * zoomMarginFactor;
        }
        //Debug.Log("scale " + targetScale);

        float timePercent = 0f;

        while (transform.position != targetPosition || Camera.main.orthographicSize != targetScale)
        {
            transform.position = Vector3.MoveTowards(transform.position, targetPosition, moveSpeed);
            float zoomSpeed = moveSpeed / 10f;
            timePercent += zoomSpeed;
            float scale = Mathf.Lerp(startCameraScale, targetScale, timePercent);
            Camera.main.orthographicSize = scale;
            //Debug.Log(scale);

            yield return null;
        }
        IsZooming = false;
    }

}
