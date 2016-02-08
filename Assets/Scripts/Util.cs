using UnityEngine;
using System.Collections;
using Mono;

public static class Util {

	public static SpriteRenderer SetTopOrder(Transform transform, SpriteRenderer target)
	{
		int sortingOrder = 0; 
		foreach (Transform child in transform)
		{
			if (!child.gameObject.activeInHierarchy) continue;
			SpriteRenderer sr = child.GetComponent<SpriteRenderer>();
			if (sr != null && sr.sortingOrder > sortingOrder)
			{
				sortingOrder = sr.sortingOrder;
			}
		}
		Debug.Log("highest order found: " + sortingOrder);
		if (target != null)
		{
			target.sortingOrder = sortingOrder;
		}
		return target;
	}

}
