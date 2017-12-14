using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SelfDestruct : MonoBehaviour {
	public float delayBeforeDestruction = 3f;

	void Awake () {
		StartCoroutine (WaitForDestruction ());
	}

	IEnumerator WaitForDestruction () {
		yield return new WaitForSeconds (delayBeforeDestruction);
		Destroy (gameObject);
	}
}