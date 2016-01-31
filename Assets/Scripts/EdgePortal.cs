using UnityEngine;
using System.Collections;

public class EdgePortal : MonoBehaviour {

	public EdgePortal OppositePortal { get; private set; }

	void Awake() {

	}

	// Use this for initialization
	void Start () {
	
	}

	public void Init(EdgePortal oppositePortal) {
		OppositePortal = oppositePortal;
	}
}
