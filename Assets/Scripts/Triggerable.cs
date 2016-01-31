using UnityEngine;
using System.Collections;

public class Triggerable : MonoBehaviour {

    protected bool active = false; 

	void Awake () {

        PolygonCollider2D cc = gameObject.AddComponent<PolygonCollider2D>();
        cc.isTrigger = true;
        Debug.Log("parent");
	}	
}
