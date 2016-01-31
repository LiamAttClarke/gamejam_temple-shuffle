using UnityEngine;
using System.Collections;

public class Platform : MonoBehaviour {

    public Sprite platformUp;
    public Sprite platformDown;
    bool active = false;

    private SpriteRenderer sr;

    void Awake()
    {
        PolygonCollider2D pc = gameObject.GetComponent<PolygonCollider2D>();
        if (pc == null)
        {
            Debug.Log("manually-created polygon collider intended to exist is missing on: " + gameObject.name);
        }
        sr = gameObject.GetComponent<SpriteRenderer>();
        if (platformUp == null)
        {
            Debug.Log("platformUp sprite missing on: " + gameObject.name);
        }
        if (platformDown == null)
        {
            Debug.Log("platformDown sprite missing on " + gameObject.name);
        }
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
