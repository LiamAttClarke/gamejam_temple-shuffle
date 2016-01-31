using UnityEngine;
using System.Collections;

public enum TileType { Alter, Path }

public class Tile : MonoBehaviour {

	static float tileSpeed = 0.5f;

    public int MapIndexX { get; private set; }
    public int MapIndexY { get; private set; }
    public TileType Type { get; private set; }
	public float Width { get; private set;}
    
	Vector3 targetPosition;
	Map map;


    BoxCollider2D bc;

	void Awake() {
		Width = GetBounds().size.x;
        bc = gameObject.AddComponent<BoxCollider2D>();
        bc.isTrigger = true;
	}

	public void Init(TileType tileType, int mapIndexX, int mapIndexY) {
		Type = tileType;
		SetMapPosition (mapIndexX, mapIndexY, false);
		map = GameObject.Find ("Map").GetComponent<Map> ();
	}

    public void SetMapPosition(int x, int y, bool slide) {
        MapIndexX = x;
        MapIndexY = y;
		targetPosition = new Vector3 (x * Width, y * Width, 0);
		if (slide) {
			StartCoroutine("MoveToTarget");
		} else {
			transform.position = targetPosition;		
		}
	}

	public Bounds GetBounds() {
        var renderer = GetComponent<Renderer>();
        var combinedBounds = renderer.bounds;
        var renderers = GetComponentsInChildren<Renderer>();
        foreach (var render in renderers) {
            if (render != renderer) combinedBounds.Encapsulate(render.bounds);
        }
        return combinedBounds;
	}
	
	IEnumerator MoveToTarget () {
		map.IsMapMoving = true;
		while (transform.position != targetPosition) {
			transform.position = Vector3.MoveTowards(transform.position,  targetPosition, tileSpeed);
			yield return null;
		}
		map.IsMapMoving = false;
	}
}
