using UnityEngine;
using System.Collections;

public class Map : MonoBehaviour {
    public GameObject alterTilePrefab;
    public GameObject[] tilePrefabs;

    public Tile[,] grid { get; set; }
    public Tile alterTile { get; private set; }

    const int WORLD_SIZE = 9; // must be odd
	Transform tran;

	int nullTileIndexX, nullTileIndexY;

    void Awake() {
		tran = transform;
    }

    // Use this for initialization
    void Start() {
		InitMap();
    }

    void InitMap() {
        grid = new Tile[WORLD_SIZE, WORLD_SIZE];
        int centerIndex = WORLD_SIZE / 2;
        int emptyIndexX;
        int emptyIndexY;
        do {
            emptyIndexX = Random.Range(0, WORLD_SIZE);
            emptyIndexY = Random.Range(0, WORLD_SIZE);
        } while (emptyIndexX == centerIndex && emptyIndexY == centerIndex);
        for (int y = 0; y < WORLD_SIZE; y++) {
            for (int x = 0; x < WORLD_SIZE; x++) {
                if (x == centerIndex && y == centerIndex) { // alter tile
                    GameObject tileGameObject = Instantiate(alterTilePrefab);
					tileGameObject.transform.parent = tran;
					Tile tile = tileGameObject.GetComponent<Tile>();
					tile.Init (TileType.Alter, x, y);
					grid[x, y] = tile;
					alterTile = tile;
                } else if (x == emptyIndexX && y == emptyIndexY) { // empty tile
                    grid[x, y] = null;
					nullTileIndexX = x;
					nullTileIndexY = y;
                } else { // all other tiles (randomized)
                    int tilePrefabsIndex = Random.Range(0, tilePrefabs.Length);
                    GameObject tilePrefab = tilePrefabs[tilePrefabsIndex];
                    GameObject tileGameObject = Instantiate(tilePrefab);
					tileGameObject.transform.parent = tran;
					Tile tile = tileGameObject.GetComponent<Tile>();
					grid[x, y] = tile;
					tile.Init (TileType.Path, x, y);
                }
            }
        }
    }

    public void MoveTile(Direction direction) {
        int tileOffsetX = 0;
        int tileOffsetY = 0;
        switch (direction) {
            case Direction.Up:
				if (nullTileIndexY > 0) {
                    tileOffsetY = 1;
                }
                break;
            case Direction.Down:
				if (nullTileIndexY < WORLD_SIZE - 1) {
                    tileOffsetY = -1;
                }
                break;
            case Direction.Left:
				if (nullTileIndexX < WORLD_SIZE - 1) {
                    tileOffsetX = -1;
                }
                break;
            case Direction.Right:
				if (nullTileIndexX > 0) {
                    tileOffsetX = 1;
                }
                break;
        }
		if (tileOffsetX != 0 || tileOffsetY != 0) {
			Tile tileToMove = grid[nullTileIndexX - tileOffsetX, nullTileIndexY - tileOffsetY];
			if (tileToMove != alterTile) {
				// move tile
				grid[nullTileIndexX, nullTileIndexY] = tileToMove;
				grid[tileToMove.MapIndexX, tileToMove.MapIndexY] = null;
				// set tile index
				nullTileIndexX = nullTileIndexX - tileOffsetX;
				nullTileIndexY = nullTileIndexY - tileOffsetY;
				tileToMove.SetMapPosition(tileToMove.MapIndexX + tileOffsetX, tileToMove.MapIndexY + tileOffsetY, true);
			}
		}
    }
}
