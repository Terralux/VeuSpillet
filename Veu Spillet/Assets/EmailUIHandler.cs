using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EmailUIHandler : BaseMenu {

	public static EmailUIHandler instance;

	private InputField emailField;

	[Tooltip("This is the feedback panel used to tell the user that an invitation was sent")]
	public GameObject feedbackPanel;

	public void Awake(){
		if (instance != null) {
			Destroy (this);
		} else {
			instance = this;
		}
		emailField = GetComponentInChildren<InputField>();

		if(EmailHandler.instance == null){
			instance.gameObject.AddComponent<EmailHandler>();
		}

		Hide ();
	}

	public override void Show ()
	{
		instance.gameObject.SetActive (true);
	}

	public override void Hide ()
	{
		instance.gameObject.SetActive (false);
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