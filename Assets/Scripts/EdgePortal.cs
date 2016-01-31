using UnityEngine;
using System.Collections;

public class EdgePortal : MonoBehaviour {

	public EdgePortal OppositePortal { get; private set; }

	public void Init(EdgePortal oppositePortal) {
		OppositePortal = oppositePortal;
	}

	void OnTriggerExit2D(Collider2D other) {
		if (other.tag == "Player") {
			// if on other side of portal, teleport to oppositePortal
			Vector3 portalToPlayer = other.transform.position - transform.position;
			float side = Vector3.Dot (transform.up, portalToPlayer);
			if (side > 0) {
				other.transform.position = OppositePortal.transform.position + portalToPlayer;
			}
		}
	}
}
