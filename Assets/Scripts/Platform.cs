using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Platform : MonoBehaviour {

    public Sprite platformUp;
    public Sprite platformDown;
    private SpriteRenderer sr;
	public bool active { get; private set; }
    public bool persists;
	public int id;

    public enum Kind { STICKY, POPS_BACK_UP }
    public Kind kind = Kind.STICKY;

    void Awake()
    {
        //checks collider
        PolygonCollider2D pc = gameObject.GetComponent<PolygonCollider2D>();
        if (pc == null)
        {
            Debug.Log("manually-created polygon collider intended to exist is missing on: " + gameObject.name);
        }

        //checks sprites
        sr = gameObject.GetComponent<SpriteRenderer>();
        if (platformUp == null)
        {
            Debug.Log("platformUp sprite missing on: " + gameObject.name);
        }
        if (platformDown == null)
        {
            Debug.Log("platformDown sprite missing on " + gameObject.name);
        }

        //checks state
        active = false;
        sr.sprite = platformUp;

		//check parent Tile has Puzzle class
		Tile tile = transform.parent.GetComponent<Tile>();
		if (tile == null) {
			Debug.Log("Parent gameobject needs to be Tile for piece: " + gameObject.name);
			return;
		}
		Puzzle puzzle = transform.parent.GetComponent<Puzzle>();
		if (puzzle == null)
		{
			Debug.Log("Puzzle is required on parent tile: " + gameObject.name);
			return;
		}
        
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        Player player = other.GetComponent<Player>();
        if (player != null)
        {
            active = true;
            sr.sprite = platformDown;
        }
    }

    void OnTriggerExit2D(Collider2D other)
    {
        Player player = other.GetComponent<Player>();
        if (player != null && kind == Kind.POPS_BACK_UP)
        {
            active = false;
            sr.sprite = platformUp;
        }
    }

}
