using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FlagQuestionMenu : BaseMenu {

	public static FlagQuestionMenu instance;
	public static int questionID;

	public InputField commentField;

	void Awake () {
		if (instance) {
			Destroy (this);
		} else {
			instance = this;
		}
		Hide ();
	}

	#region implemented abstract members of BaseMenu

	public override void Show (){
		instance.commentField.text = "";
		instance.gameObject.SetActive (true);
	}

	public override void Hide (){
		instance.gameObject.SetActive (false);
	}

	#endregion

	public void Flag(){
		SetupQuestions.instance.ReportQuestion (questionID, DataContainer.currentLoggedUser.userName, commentField.text);
		Hide ();
	}
}