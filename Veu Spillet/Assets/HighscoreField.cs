using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HighscoreField : MonoBehaviour {

	public Text nameField;
	public Text correctAnswers;

	void Awake(){
		transform.parent.parent.gameObject.SetActive (false);
	}

	public void UpdateField (string name, string correct) {
		nameField.text = name;
		correctAnswers.text = correct;
	}
}