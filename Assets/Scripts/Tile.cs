using UnityEngine;
using System.Collections;

public enum TileType { Alter, Path }

public class Tile : MonoBehaviour {

	static float tileSpeed = 0.1f;

    public int MapIndexX { get; private set; }
    public int MapIndexY { get; private set; }
    public TileType Type { get; private set; }
    
	Vector3 targetPosition;

	void Awake() {

	}


	public void Init(TileType tileType, int mapIndexX, int mapIndexY) {
		Type = tileType;
		SetMapPosition (mapIndexX, mapIndexY, false);
	}

    public void SetMapPosition(int x, int y, bool slide) {
        MapIndexX = x;
        MapIndexY = y;
		targetPosition = new Vector3 (x, y, 0);
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
