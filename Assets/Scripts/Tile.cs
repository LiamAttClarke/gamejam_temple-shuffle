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
		Width = GetSize ();
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

	float GetSize() {
		return GetComponent<Collider2D> ().bounds.size.x;
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
