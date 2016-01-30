using UnityEngine;
using System.Collections;

//just attach this script to player
public class Player : MonoBehaviour {

	// Use this for initialization
	void Start () {
        gameObject.tag = "Player";
        gameObject.AddComponent<Rigidbody2D>();
        gameObject.AddComponent<MeshCollider>();
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
