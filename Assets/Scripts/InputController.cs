using UnityEngine;
using System.Collections;

public class InputController : MonoBehaviour {

    enum inputMode { PLAYER, TILE }
    inputMode mode = inputMode.PLAYER;

    Player player;
    Rigidbody2D playerRb;
    float moveForce = 10f;

	// Use this for initialization
	void Start () {
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();
        if (player == null)
        {
            Debug.Log("Player not found.");
        }
        playerRb = player.GetComponent<Rigidbody2D>();
        if (playerRb)
        {
            Debug.Log("Rigidbody not added to player.");
        }
	}
	
	// Update is called once per frame
	void Update () {
	
        PlayerUpdate();
        TileUpdate();

    }

    void setInputMode()
    {

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

    }
}
