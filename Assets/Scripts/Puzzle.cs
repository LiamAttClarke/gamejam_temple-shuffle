using UnityEngine;
using System.Collections;
using System.Collections.Generic;

[ExecuteInEditMode]
public class Puzzle : MonoBehaviour {

    public bool updateAllPlatforms;
    public GameObject[] platforms;
    public enum Order { ORDER_ANY, ORDER_MATTERS }
    public Order order;
    public bool staysPressed;

    // Use this for initialization
    [ExecuteInEditMode]
    void Awake()
    {
        if (updateAllPlatforms)
        {
            UpdateAllPlatforms();
        }

        InitPlatforms();
    }

    //auto seek all Platform objects in 
    void UpdateAllPlatforms()
    {
        List<GameObject> platformsList = new List<GameObject>();
        foreach (Transform t in transform)
        {
            Platform platform = t.GetComponent<Platform>();
            if (platform != null)
            {
                platformsList.Add(platform.gameObject);
            }
        }
        platforms = platformsList.ToArray();
    }

    void InitPlatforms(){
        foreach (GameObject platform in platforms)
        {
            //platform.persists = staysPressed;

        }
    }
}
