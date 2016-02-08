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

    public enum Kind { STICKY, SPRINGY, TIMEY }
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

		//check parent has Puzzle class
		Puzzle puzzle = transform.parent.GetComponent<Puzzle>();
		if (puzzle == null)
		{
			Debug.Log("Parent gameobject needs to have Puzzle added for platform: |" + gameObject.name +"| to work");
			return;
		}
        
    }

    void OnTriggerEnter2D(Collider2D other)
    {
		Physical physical = other.GetComponent<Physical>();
		if (physical != null)
        {
            active = true;
            sr.sprite = platformDown;
        }
    }

    void OnTriggerExit2D(Collider2D other)
    {
		Physical[] physicals = GameObject.FindObjectsOfType<Physical>();
		foreach (Physical physical in physicals)
		{
			if (kind == Kind.SPRINGY && other.gameObject == physical.gameObject && physical.GetType().IsSubclassOf(typeof(Physical)))
			{
				//only works for any one physical leaving, not when any stay and one leaves
				active = false;
				sr.sprite = platformUp;
			}
		}
    }

}
