using UnityEngine;
using System.Collections;

public class GameManager : MonoBehaviour {
	public GameObject alterPrefab;
	public GameObject[] tilePrefabs;

	public Tile[,] tiles { get; set; }

	const int WORLD_SIZE = 5;

	void Awake() {
		tiles = new Tile[WORLD_SIZE, WORLD_SIZE];
		int centerIndex = WORLD_SIZE / 2;
		int emptyIndexX;
		int emptyIndexY;
		do {
			emptyIndexX = Random.Range (0, WORLD_SIZE);
			emptyIndexY = Random.Range (0, WORLD_SIZE);
		} while (emptyIndexX != centerIndex && emptyIndexY != centerIndex);
		for (int y = 0; y < WORLD_SIZE; y++) {
			for (int x = 0; x < WORLD_SIZE; x++) {
				if (x == centerIndex && y == centerIndex) {
					Vector3 position = new Vector3(x, y, 0);
					GameObject tileGameObject = (GameObject)Instantiate (alterPrefab, position, Quaternion.identity);
					tiles[x, y] = new Tile(tileGameObject);
				} else if (x == emptyIndexX && y == emptyIndexY) {
					tiles[x,y] = null;
				} else {
					int tilePrefabsIndex = Random.Range (0, tilePrefabs.Length);
					GameObject tilePrefab = tilePrefabs[tilePrefabsIndex];
					Vector3 position = new Vector3(x, y, 0);
					GameObject tileGameObject = (GameObject)Instantiate (tilePrefab, position, Quaternion.identity);
					tiles[x, y] = new Tile(tileGameObject);
				}
			}
		}
	}

	// Use this for initialization
	void Start () {

	}
}
