using UnityEngine;
using System.Collections;

public enum Direction { Up, Down, Left, Right }

public class InputController : MonoBehaviour {
	
	public enum inputMode { NONE, PLAYER, MAP }
	public inputMode mode;
	
	Player player;
	Rigidbody2D playerRb;
	Map map;

	void Awake() {
		map = GameObject.Find("Map").GetComponent<Map>();
		mode = inputMode.MAP;
	}
	
	void Start()
	{
		player = GameObject.Find("Player").GetComponent<Player>();
		playerRb = player.GetComponent<Rigidbody2D>();
	}
	
	void Update () {
		if (mode == inputMode.PLAYER) {
			PlayerUpdate ();
		} else if (mode == inputMode.MAP) {
			TileUpdate();
		}    
	}
	//string str = "";
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
		if (true) {
			if (Input.GetKeyDown(KeyCode.RightArrow)) 
			{
				// right
				map.MoveTile(Direction.Right);
			} 
			else if (Input.GetKeyDown(KeyCode.LeftArrow)) 
			{
				// left
				map.MoveTile(Direction.Left);
			} 
			else if (Input.GetKeyDown(KeyCode.UpArrow)) 
			{
				// up
				map.MoveTile(Direction.Up);
			} 
			else if (Input.GetKeyDown(KeyCode.DownArrow)) 
			{
				// down
				map.MoveTile(Direction.Down);
			}
		}	
	}
}
