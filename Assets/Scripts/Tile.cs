using UnityEngine;
using System.Collections;

public enum TileType { Empty, Alter, Path }

public class Tile {
	
    public int MapIndexX { get; private set; }
    public int MapIndexY { get; private set; }
    public TileType Type { get; private set; }
    public GameObject tileGameObject { get; private set; }

    public Tile(TileType tileType, GameObject tile, int mapIndexX, int mapIndexY) {
		tileGameObject = tile;
        SetMapPosition(mapIndexX, mapIndexY);
    }

    public void SetMapPosition(int x, int y) {
        MapIndexX = x;
        MapIndexY = y;
        tileGameObject.transform.position = new Vector3(x, y, 0);
    }
}
