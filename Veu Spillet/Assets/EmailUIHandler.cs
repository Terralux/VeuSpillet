using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EmailUIHandler : MonoBehaviour {

	private InputField emailField;

	[Tooltip("This is the feedback panel used to tell the user that an invitation was sent")]
	public GameObject feedbackPanel;

	void Awake(){
		emailField = GetComponentInChildren<InputField>();
	}

	public void AddUserToGame(){
		EmailHandler.SendEmail(emailField.text);
		emailField.text = "";

		StartCoroutine (ShowFeedbackPanel());
	}

	IEnumerator ShowFeedbackPanel(){
		feedbackPanel.SetActive(true);
		yield return new WaitForSeconds(3f);
		feedbackPanel.SetActive(false);
	}
}