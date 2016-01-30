using UnityEngine;
using System.Collections;

//just attach this script to player
public class Player : MonoBehaviour {

    Rigidbody2D rb;
    PolygonCollider2D pc;

	// Use this for initialization
	void Start () {
        gameObject.tag = "Player";
        rb = gameObject.AddComponent<Rigidbody2D>();
        rb.gravityScale = 0;
        pc = gameObject.AddComponent<PolygonCollider2D>();
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
