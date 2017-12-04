using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InputTest : MonoBehaviour {

	public Text potionTextFeedback;

	void Update () {
		if (Input.GetMouseButtonDown (0)) {
			//Brug våben 1
		}

		if (Input.GetMouseButtonDown (1)) {
			//Brug våben 2
		}
	}

	void OnTriggerEnter(Collider col){
		if (col.CompareTag ("Pickup")) {
			potionTextFeedback.gameObject.SetActive (true);
			//You got a potion!
		}
	}
}