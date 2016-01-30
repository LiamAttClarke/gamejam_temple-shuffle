using UnityEngine;
using System.Collections;

public class GameManager : MonoBehaviour {
	public GameObject alterPrefab;
	public GameObject[] tilePrefabs;

	public Tile[,] tiles { get; set; }

	const int WORLD_SIZE = 5;

	void Awake() {
		
	}

	// Use this for initialization
	void Start () {
		tiles = new Tile[WORLD_SIZE, WORLD_SIZE];
		int centerIndex = WORLD_SIZE / 2;
		for (int y = 0; y < WORLD_SIZE; y++) {
			for (int x = 0; x < WORLD_SIZE; x++) {
				GameObject tilePrefab;
				if (x == centerIndex && y == centerIndex) {
					tilePrefab = alterPrefab;
				} else {
					int tilePrefabsIndex = Random.Range (0, tilePrefabs.Length);
					tilePrefab = tilePrefabs[tilePrefabsIndex];
				}
				Vector3 position = new Vector3(x, y, 0);
				GameObject tileGameObject = (GameObject)Instantiate (tilePrefab, position, Quaternion.identity);
				tiles[x, y] = new Tile(tileGameObject);
			}
		}
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
