using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FlaggedQuestionContainer : MonoBehaviour {

	public delegate void VoidEvent(int value);
	public VoidEvent OnClickSendValue;

	public Text questionText;
	public Text commentText;
	public Text userText;

	[HideInInspector]
	public int myIndex;

	public void Init(string question, string username, string comment){
		questionText.text = question;
		commentText.text = comment;
		userText.text = username;
	}

	public void OnClick(){
		if (OnClickSendValue != null) {
			OnClickSendValue (myIndex);
		}
	}
}