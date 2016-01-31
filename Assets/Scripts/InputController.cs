﻿using UnityEngine;
using System.Collections;

public enum Direction { Up, Down, Left, Right }

public class InputController : MonoBehaviour {
	
	public enum inputMode { PLAYER, MAP, MENU }
    private inputMode mode;
	
	Player player;
	Rigidbody2D playerRb;
	Map map;
    bool fire1happening = false;
    //bool debugOverrideSet = false;
	
	void Awake() {
        GameObject mapGO = GameObject.Find("Map");
        if (mapGO!=null) {
            map = mapGO.GetComponent<Map>();
		}
        mode = inputMode.MAP;
        Debug.Log("Mode: " + mode);
	}
	
	void Start()
	{
		player = GameObject.Find("Player").GetComponent<Player>();
		playerRb = player.GetComponent<Rigidbody2D>();
	}
	
	void Update () {
        ModeUpdate();
        if (mode == inputMode.PLAYER)
        {
            PlayerUpdate();
        }
        else if (mode == inputMode.MAP)
        {
            TileUpdate();
        }
	}

    void ModeUpdate()
    {
        //debug master mode switch
        if (Input.GetKeyDown(KeyCode.Q))
        {
            ToggleMode();
            //debugOverrideSet = true;
            //Debug.Log("debug on");
        }

        //gameplay mode switch /*
        float fire1 = Input.GetAxis("Fire1"); //Input.GetAxis lasts several frames
        if (fire1 != 0 && !fire1happening)
        {
            fire1happening = true;
            if (player != null)
            {
                if (player.isInShuffler)
                {
                    if (map != null && !map.IsMapMoving)
                    {
                        ToggleMode();
                        //debugOverrideSet = false;
                        //Debug.Log("debug off");
                    }
                    else
                    {
                        ToggleMode();
                        //debugOverrideSet = false;
                        //Debug.Log("debug off");
                    }
                }
            }
        }
        if (fire1 == 0)
        {
            fire1happening = false;
        }
        //if (player != null && !player.isInShuffler && !debugOverrideSet)
        //{
        //    mode = inputMode.PLAYER;
        //}
    }

    void PlayerUpdate()
	{
		if (player != null) {
			float x = Input.GetAxis("Horizontal");
			float y = Input.GetAxis("Vertical");
			
			playerRb.AddForce(player.moveForce * new Vector3(x, y, 0));
		}
	}
	
	void TileUpdate()
	{
        if (map == null) return;

		if (!map.IsMapMoving) {
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

    void ToggleMode()
    {
        if (mode == inputMode.MAP)
        {
            mode = inputMode.PLAYER;
            //Debug.Log("P");
        }
        else if (mode == inputMode.PLAYER)
        {
            mode = inputMode.MAP;
            //Debug.Log("M");
        }
        Debug.Log("Mode: " + mode);
    }
}
