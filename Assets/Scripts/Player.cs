using UnityEngine;
using System.Collections;

//just attach this script to player

//how to tweak:
//rb.drag below slows player down
//Edit > Project > Input > Horizontal | Vertical > Gravity = ++## slows down trail off of input 
//recommend at least 3:1 ratio of moveForce to drag for snappy controls.  3 * moveForce = 1 * drag.
public class Player : MonoBehaviour {

    public float moveForce = 40f;

    Rigidbody2D rb;
    Collider2D pc;

	void Awake () {
        gameObject.tag = "Player";
        gameObject.name = "Player";
        rb = gameObject.AddComponent<Rigidbody2D>();
        rb.gravityScale = 0;
        pc = gameObject.AddComponent<CircleCollider2D>();
        rb.freezeRotation = true;
        rb.drag = 15f;
	}
	
	void Update () {
	
	}
}
