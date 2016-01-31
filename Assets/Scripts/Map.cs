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

	Transform tran;

	int nullTileIndexX, nullTileIndexY;

    void Awake() {
		tran = transform;
		if (worldSize % 2 == 0) {
			throw new UnityException("World size must be odd.");
		}
    }

    // Use this for initialization
    void Start() {
		InitMap();
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
		GameObject edgePortalPrefab = (GameObject)Resources.Load ("Prefabs/EdgePortal");
		// portals
		TileWidth = grid[0, 0].Width;
		float halfTileWidth = TileWidth * 0.5f;
		float offset = 1.3f;
		float mapEdge = (worldSize * TileWidth) - halfTileWidth;
		Quaternion down = Quaternion.Euler(0, 0, 180f);
		Quaternion right = Quaternion.Euler(0, 0, 270f);
		Quaternion left = Quaternion.Euler(0, 0, 90f);
		for (int x = 0; x < worldSize; x++) {
			// horizontal
			float posX = (x * TileWidth);
			Vector3 portalPos1 = new Vector3(posX - offset, mapEdge, 0);
			Vector3 portalPos2 = new Vector3(posX + offset, mapEdge, 0);
			Vector3 portalPos3 = new Vector3(posX - offset, -halfTileWidth, 0);
			Vector3 portalPos4 = new Vector3(posX + offset, -halfTileWidth, 0);
			GameObject portalTopLeft = (GameObject)Instantiate(edgePortalPrefab, portalPos1, Quaternion.identity);
			GameObject portalTopRight = (GameObject)Instantiate(edgePortalPrefab, portalPos2, Quaternion.identity);
			GameObject portalBottomLeft = (GameObject)Instantiate(edgePortalPrefab, portalPos3, down);
			GameObject portalBottomRight = (GameObject)Instantiate(edgePortalPrefab, portalPos4, down);
			// parent objects
			portalTopLeft.transform.parent = tran;
			portalTopRight.transform.parent = tran;
			portalBottomLeft.transform.parent = tran;
			portalBottomRight.transform.parent = tran;
			// opposite portals
			portalTopLeft.GetComponent<EdgePortal>().Init(portalBottomLeft.GetComponent<EdgePortal>());
			portalTopRight.GetComponent<EdgePortal>().Init(portalBottomRight.GetComponent<EdgePortal>());
			portalBottomLeft.GetComponent<EdgePortal>().Init(portalTopLeft.GetComponent<EdgePortal>());
			portalBottomRight.GetComponent<EdgePortal>().Init(portalTopRight.GetComponent<EdgePortal>());
		}
		for (int y = 0; y < worldSize; y++) {
			// vertical
			float posY = (y * TileWidth);
			Vector3 portalPos1 = new Vector3(-halfTileWidth, posY + offset, 0);
			Vector3 portalPos2 = new Vector3(-halfTileWidth, posY - offset, 0);
			Vector3 portalPos3 = new Vector3(mapEdge, posY + offset, 0);
			Vector3 portalPos4 = new Vector3(mapEdge, posY - offset, 0);
			GameObject portalLeftTop = (GameObject)Instantiate(edgePortalPrefab, portalPos1, left);
			GameObject portalLeftBottom = (GameObject)Instantiate(edgePortalPrefab, portalPos2, left);
			GameObject portalRightTop = (GameObject)Instantiate(edgePortalPrefab, portalPos3, right);
			GameObject portalRightBottom = (GameObject)Instantiate(edgePortalPrefab, portalPos4, right);
			// parent objects
			portalLeftTop.transform.parent = tran;
			portalLeftBottom.transform.parent = tran;
			portalRightTop.transform.parent = tran;
			portalRightBottom.transform.parent = tran;
			// opposite portals
			portalLeftTop.GetComponent<EdgePortal>().Init(portalRightTop.GetComponent<EdgePortal>());
			portalLeftBottom.GetComponent<EdgePortal>().Init(portalRightBottom.GetComponent<EdgePortal>());
			portalRightTop.GetComponent<EdgePortal>().Init(portalLeftTop.GetComponent<EdgePortal>());
			portalRightBottom.GetComponent<EdgePortal>().Init(portalLeftBottom.GetComponent<EdgePortal>());
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
}
