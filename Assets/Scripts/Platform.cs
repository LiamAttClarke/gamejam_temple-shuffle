using UnityEngine;
using System.Collections;

public class Platform : MonoBehaviour {

    public Sprite platformUp;
    public Sprite platformDown;
    private SpriteRenderer sr;
    bool active;
    public bool persists;

    enum Kinds { POPS_BACK_UP, STICKS_DOWN }
    Kinds kind = Kinds.POPS_BACK_UP;

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
        if (player != null)
        {
            active = false;
            sr.sprite = platformUp;
        }
    }

}
