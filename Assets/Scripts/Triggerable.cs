using UnityEngine;
using System.Collections;

public class Triggerable : MonoBehaviour {

	void Awake () {

        CircleCollider2D cc = gameObject.AddComponent<CircleCollider2D>();
        cc.isTrigger = true;
	
	}	
}
