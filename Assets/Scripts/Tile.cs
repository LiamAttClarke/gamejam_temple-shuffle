using UnityEngine;
using System.Collections;

public enum TileType { Void, Alter, Path }

public class Tile : MonoBehaviour {

	static float tileSpeed = 0.5f;
    static float shadowFadeSpeed = 0.01f;

    public int MapIndexX { get; private set; }
    public int MapIndexY { get; private set; }
    public TileType Type { get; private set; }
	public float Width { get; private set;}
    public GameObject shadow { get; set; }
    public bool IsDiscovered { get; private set; }
    
	Vector3 targetPosition;
	Map map;
    Door[] doors;

    BoxCollider2D bc;

	void Awake() {
		Width = GetBounds().size.x;
        bc = gameObject.AddComponent<BoxCollider2D>();
        bc.isTrigger = true;

	}

    void Start() {
        doors = transform.GetComponentsInChildren<Door>();
    }

	public void Init(TileType tileType, int mapIndexX, int mapIndexY) {
		Type = tileType;
		SetMapPosition (mapIndexX, mapIndexY, false);
		map = GameObject.Find ("Map").GetComponent<Map> ();

		//shadow
		shadow = (GameObject)Instantiate(Resources.Load("Prefabs/Shadow"), transform.position, Quaternion.identity);
		shadow.transform.parent = transform;
		shadow.name = "Shadow";
		shadow.GetComponent<SpriteRenderer>().sortingOrder = 10;
		shadow.transform.localScale = GetBounds().size;
	}

    public void SetMapPosition(int x, int y, bool slide) {
        MapIndexX = x;
        MapIndexY = y;
        targetPosition = new Vector3 (x * Width - Width / 2, y * Width - Width / 2, 0);
		if (slide) {
			StartCoroutine("MoveToTarget");
		} else {
			transform.position = targetPosition;
		}
	}

	public Bounds GetBounds() {
        var renderer = GetComponent<Renderer>();
        var combinedBounds = renderer.bounds;
		if (transform.localScale.x != 1 || transform.localScale.y != 1 ) {
			Debug.LogWarning("Warning, does not support parent being scaled to non-1 size. " + renderer.name + " set to " + renderer.transform.localScale);
		}
		var renderers = GetComponentsInChildren<Renderer>();
        foreach (var render in renderers) {
			if (render != renderer) combinedBounds.Encapsulate(render.bounds);
        }
        return combinedBounds; 
	}
	
	IEnumerator MoveToTarget () {
		map.IsMapMoving = true;
		while (transform.position != targetPosition) {
			transform.position = Vector3.MoveTowards(transform.position,  targetPosition, tileSpeed);
			yield return null;
		}
		map.IsMapMoving = false;
	}

    public void UpdateDoors() {
        if (doors == null) return;
        foreach (Door door in doors) {
            door.UpdateDoorState();
        }
    }

    public void RevealTile() {
        StartCoroutine("FadeOut");
    }

    IEnumerator FadeOut() {
        if (!IsDiscovered) {
            var renderer = shadow.GetComponent<SpriteRenderer>();
            Material sharedMat = renderer.material;
            renderer.material = new Material(sharedMat);
            while (renderer.material.color.a != 0) {
                Color prevColor = renderer.material.color;
                renderer.material.color = new Color(prevColor.r, prevColor.g, prevColor.b, prevColor.a - shadowFadeSpeed);
                yield return null;
            }
        }
        IsDiscovered = true;
    }
}
