using UnityEngine;
using System.Collections;

public enum Direction { Up, Down, Left, Right }

public class InputController : MonoBehaviour {
	
    public float tileMoveThreshold = 0.25f;

    public enum inputMode { NONE, PLAYER, MAP }
    public inputMode mode;
	
	Player player;
	Rigidbody2D playerRb;
    Map map;

    bool isTileMoving = false;
    bool isInputReleased = false;

	void Awake() {
        //map = GameObject.Find("Map").GetComponent<Map>();
        mode = inputMode.PLAYER;
    }

    void Start()
    {
        player = GameObject.Find("Player").GetComponent<Player>();
        playerRb = player.GetComponent<Rigidbody2D>();
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
    string str = "";
	void PlayerUpdate()
	{
		if (player != null) {
			float x = Input.GetAxis("Horizontal");
			float y = Input.GetAxis("Vertical");

            playerRb.AddForce(player.moveForce * new Vector3(x, y, 0));
            //str += x + "\t";
            //Debug.Log(str);
		}
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
