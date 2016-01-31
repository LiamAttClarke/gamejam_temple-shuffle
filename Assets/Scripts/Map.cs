using UnityEngine;
using System.Collections;

public class Map : MonoBehaviour {
    public GameObject alterTilePrefab;
    public GameObject[] tilePrefabs;

    public Tile[,] grid { get; set; }
    public Tile alterTile { get; private set; }
	public int worldSize = 5; // must be odd & greater than 1
	public bool IsMapMoving { get; set; }
	public float TileWidth { get; private set; }
    public Bounds WorldBounds;

	Transform tran;

	int nullTileIndexX, nullTileIndexY;

    void Awake() {
		tran = transform;
		if (worldSize % 2 == 0) {
			throw new UnityException("World size must be odd.");
		}
        InitMap();
        GetMapSize();
    }

    void InitMap() {
		grid = new Tile[worldSize, worldSize];
		if (worldSize == 1) { // by request
			GameObject tileGameObject = Instantiate(alterTilePrefab);
			tileGameObject.transform.parent = tran;
			Tile tile = tileGameObject.GetComponent<Tile>();
			tile.Init (TileType.Alter, 0, 0);
			grid[0, 0] = tile;
			alterTile = tile;
			return;
		}
		int centerIndex = worldSize / 2;
        int emptyIndexX;
        int emptyIndexY;
        do {
			emptyIndexX = Random.Range(0, worldSize);
			emptyIndexY = Random.Range(0, worldSize);
        } while (emptyIndexX == centerIndex && emptyIndexY == centerIndex);
		// tiles
		for (int y = 0; y < worldSize; y++) {
			for (int x = 0; x < worldSize; x++) {
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
				if (nullTileIndexY < worldSize - 1) {
                    tileOffsetY = -1;
                }
                break;
            case Direction.Left:
				if (nullTileIndexX < worldSize - 1) {
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

    Bounds GetMapSize()
    {
        WorldBounds = new Bounds(Vector3.zero, Vector3.zero);
        foreach (Tile tile in grid){
            if (tile == null) continue;
            SpriteRenderer renderer = tile.GetComponent<SpriteRenderer>();
            if (WorldBounds.extents == Vector3.zero)
            {
                WorldBounds = renderer.bounds;
            }
            WorldBounds.Encapsulate(renderer.bounds);
         }
        Debug.Log(WorldBounds);
        return WorldBounds;
    }
}
