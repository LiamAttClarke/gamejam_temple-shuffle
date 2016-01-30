using UnityEngine;
using System.Collections;

public enum Direction { Up, Down, Left, Right }

public class InputController : MonoBehaviour {
	
    public float moveForce = 10f;
    public float tileMoveThreshold = 0.25f;

    enum inputMode { NONE, PLAYER, MAP }
	inputMode mode { get; set; }
	
	Player player;
	Rigidbody2D playerRb;
    Map map;

    bool isTileMoving = false;
    bool isInputReleased = false;

	void Awake() {
        map = GameObject.Find("Map").GetComponent<Map>();
        player = GameObject.Find("Player").GetComponent<Player>();
        playerRb = player.GetComponent<Rigidbody2D>();
        mode = inputMode.MAP;
    }
	
	void Update () {
        float axisX = Mathf.Abs(Input.GetAxis("Horizontal"));
        float axisY = Mathf.Abs(Input.GetAxis("Vertical"));
        if (axisX < tileMoveThreshold && axisY < tileMoveThreshold) {
            isInputReleased = true;
        }
		if (mode == inputMode.PLAYER) {
			PlayerUpdate ();
		} else if (mode == inputMode.MAP) {
			TileUpdate();
		}    
	}
	
	void PlayerUpdate()
	{
		if (player != null) {
			float x = Input.GetAxis("Horizontal");
			float y = Input.GetAxis("Vertical");
			
			playerRb.AddForce(moveForce * new Vector3(x, y, 0));
		}
		
		////////////////////
	}
	
	void TileUpdate()
	{
        if (!isTileMoving && isInputReleased) {
            float axisX = Input.GetAxis("Horizontal");
            float axisY = Input.GetAxis("Vertical");
            if (axisX > tileMoveThreshold) {
                // right
                map.MoveTile(Direction.Right);
                isInputReleased = false;
            } else if (axisX < -tileMoveThreshold) {
                // left
                map.MoveTile(Direction.Left);
                isInputReleased = false;
            } else if (axisY > tileMoveThreshold) {
                // up
                map.MoveTile(Direction.Up);
                isInputReleased = false;
            } else if (axisY < -tileMoveThreshold) {
                // down
                map.MoveTile(Direction.Down);
                isInputReleased = false;
            }
        }	
	}
}
