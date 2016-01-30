using UnityEngine;
using System.Collections;

public enum TileType { Alter, Path }

public class Tile : MonoBehaviour {

	static float tileSpeed = 0.1f;

    public int MapIndexX { get; private set; }
    public int MapIndexY { get; private set; }
    public TileType Type { get; private set; }
    
	Vector3 targetPosition;
	float width;

    BoxCollider2D bc;

	void Awake() {
        bc = gameObject.AddComponent<BoxCollider2D>();
        bc.isTrigger = true;
	}

	public void Init(TileType tileType, int mapIndexX, int mapIndexY, float tileWidth) {
		Type = tileType;
		width = tileWidth;
		SetMapPosition (mapIndexX, mapIndexY, false);
	}

    public void SetMapPosition(int x, int y, bool slide) {
        MapIndexX = x;
        MapIndexY = y;
		targetPosition = new Vector3 (x * width, y * width, 0);
		if (slide) {
			StartCoroutine("MoveToTarget");
		} else {
			transform.position = targetPosition;		
		}
	}
	
	IEnumerator MoveToTarget () {
		while (transform.position != targetPosition) {
			transform.position = Vector3.MoveTowards(transform.position,  targetPosition, tileSpeed);
			yield return null;
		}
	}
}
