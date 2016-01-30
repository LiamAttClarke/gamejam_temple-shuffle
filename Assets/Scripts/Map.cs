using UnityEngine;
using System.Collections;

public class Map : MonoBehaviour {
    public GameObject emptyTilePrefab;
    public GameObject alterTilePrefab;
    public GameObject[] tilePrefabs;

    public Tile[,] grid { get; set; }
    public Tile nullTile { get; private set; }

    const int WORLD_SIZE = 5; // must be odd

    void Awake() {
        InitMap();
    }

    // Use this for initialization
    void Start() {

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
                    grid[x, y] = new Tile(TileType.Alter, tileGameObject, x, y);
                } else if (x == emptyIndexX && y == emptyIndexY) { // empty tile
                    GameObject tileGameObject = Instantiate(emptyTilePrefab);
                    Tile tile = new Tile(TileType.Empty, tileGameObject, x, y);
                    grid[x, y] = tile;
                    nullTile = tile;
                } else { // all other tiles
                    int tilePrefabsIndex = Random.Range(0, tilePrefabs.Length);
                    GameObject tilePrefab = tilePrefabs[tilePrefabsIndex];
                    GameObject tileGameObject = Instantiate(tilePrefab);
                    grid[x, y] = new Tile(TileType.Path, tileGameObject, x, y);
                }
            }
        }
    }

    public void MoveTile(Direction direction) {
        Debug.Log("move");
        int tileOffsetX = 0;
        int tileOffsetY = 0;
        switch (direction) {
            case Direction.Up:
                if (nullTile.MapIndexY > 0) {
                    tileOffsetY = 1;
                }
                break;
            case Direction.Down:
                if (nullTile.MapIndexY < WORLD_SIZE - 1) {
                    tileOffsetY = -1;
                }
                break;
            case Direction.Left:
                if (nullTile.MapIndexX < WORLD_SIZE - 1) {
                    tileOffsetX = -1;
                }
                break;
            case Direction.Right:
                if (nullTile.MapIndexX > 0) {
                    tileOffsetX = 1;
                }
                break;
        }
        Tile tileToMove = grid[nullTile.MapIndexX - tileOffsetX, nullTile.MapIndexY - tileOffsetY];
        // move tile
        grid[nullTile.MapIndexX, nullTile.MapIndexY] = tileToMove;
        grid[tileToMove.MapIndexX, tileToMove.MapIndexY] = nullTile;
        // set tile index
        nullTile.SetMapPosition(nullTile.MapIndexX - tileOffsetX, nullTile.MapIndexY - tileOffsetY);
        tileToMove.SetMapPosition(tileToMove.MapIndexX + tileOffsetX, tileToMove.MapIndexY + tileOffsetY);
    }
}
