using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DatabaseClassifications;

public class ChangeQuestionMenu : BaseMenu {

	public static ChangeQuestionMenu instance;

	public static GameObject contentButton;
	public GameObject contentTarget;

	private int currentButtonDeletionIndex = -1;

	private Question selectedQuestion;

	public ChangeQuestionPanel changeQuestionPanel;

	private static bool isCurrentlyInFlaggedQuestions = false;

	public void Awake(){
		if (instance != null) {
			Destroy (this);
		} else {
			instance = this;
		}
		contentButton = Resources.Load ("Empty Button") as GameObject;
		Hide ();
	}

	public override void Show ()
	{
		instance.gameObject.SetActive (true);
		changeQuestionPanel.gameObject.SetActive(false);
		BackToMenu.isCurrentlyInAdminSubMenu = true;
		Clear ();
		InstantiateQuizButtons ();
	}

	public override void Hide ()
	{
		changeQuestionPanel.gameObject.SetActive(false);
		instance.gameObject.SetActive (false);
	}

	public void OnClick(int buttonIndex){
		if (currentButtonDeletionIndex >= 0) {
			instance.contentTarget.transform.GetChild (currentButtonDeletionIndex).GetComponent<Image> ().color = Color.white;
		}
		instance.contentTarget.transform.GetChild (buttonIndex).GetComponent<Image> ().color = new Color (1f, 0.7f, 0f, 1f);

		currentButtonDeletionIndex = buttonIndex;

		InstantiateQuestionButtons ();
	}

	public void ShowFlagged(int flagIndex){
		if (currentButtonDeletionIndex >= 0) {
			instance.contentTarget.transform.GetChild (currentButtonDeletionIndex).GetComponent<Image> ().color = Color.white;
		}

		instance.contentTarget.transform.GetChild (flagIndex).GetComponent<Image> ().color = new Color (1f, 0.7f, 0f, 1f);

		currentButtonDeletionIndex = flagIndex;

		InstantiateFlaggedQuestionButtons ();
	}

	public void OnSelectedQuestion(int questionIndex){
		if (currentButtonDeletionIndex >= 0) {
			instance.contentTarget.transform.GetChild (currentButtonDeletionIndex).GetComponent<Image> ().color = Color.white;
		}

		foreach (EmptyButtonContainer e in instance.contentTarget.transform.GetComponentsInChildren<EmptyButtonContainer>()) {
			if (e.myIndex == questionIndex) {
				e.GetComponent<Image> ().color = new Color (1f, 0.7f, 0f, 1f);
			}
		}

		currentButtonDeletionIndex = questionIndex;
		changeQuestionPanel.gameObject.SetActive(true);

		if(isCurrentlyInFlaggedQuestions){
			foreach(Question q in SetupQuestions.questions){
				if(q.questionID == SetupQuestions.flaggedQuestionIDs[questionIndex]){
					changeQuestionPanel.Init(q);
				}
			}
		}else{
			changeQuestionPanel.Init(SetupQuestions.questions[questionIndex]);
		}
	}

	private static void InstantiateQuizButtons(){
		instance.ClearContentOnly ();
		int count = 0;

		GameObject temp = Instantiate (contentButton, instance.contentTarget.transform);
		temp.GetComponentInChildren<Text> ().text = "Markerede spørgsmål";
		EmptyButtonContainer tempEbc = temp.GetComponentInChildren<EmptyButtonContainer> ();
		tempEbc.myIndex = count;
		tempEbc.OnClickSendValue += instance.ShowFlagged;

		foreach (Quiz quiz in SetupQuizzes.quizzes) {
			GameObject go = Instantiate (contentButton, instance.contentTarget.transform);
			go.GetComponentInChildren<Text> ().text = quiz.quizName;
			EmptyButtonContainer ebc = go.GetComponentInChildren<EmptyButtonContainer> ();
			ebc.myIndex = count;
			ebc.OnClickSendValue += instance.OnClick;
			count++;
		}
	}

	private static void InstantiateQuestionButtons(){
		instance.ClearContentOnly ();
		int count = 0;
		foreach (Question question in SetupQuestions.questions) {
			if (question.quizID == SetupQuizzes.quizzes [instance.currentButtonDeletionIndex].quizID) {
				GameObject go = Instantiate (contentButton, instance.contentTarget.transform);
				go.GetComponentInChildren<Text> ().text = question.question;
				EmptyButtonContainer ebc = go.GetComponentInChildren<EmptyButtonContainer> ();
				ebc.myIndex = count;
				ebc.OnClickSendValue += instance.OnSelectedQuestion;
			}
			count++;
		}

		isCurrentlyInFlaggedQuestions = false;
		instance.currentButtonDeletionIndex = 0;
	}

	private static void InstantiateFlaggedQuestionButtons(){
		instance.ClearContentOnly ();
		int count = 0;
		foreach (int i in SetupQuestions.flaggedQuestionIDs) {
			foreach(Question q in SetupQuestions.questions){
				if(q.questionID == i){
					GameObject go = Instantiate (contentButton, instance.contentTarget.transform);
					go.GetComponentInChildren<Text> ().text = q.question;
					EmptyButtonContainer ebc = go.GetComponentInChildren<EmptyButtonContainer> ();
					ebc.myIndex = count;
					ebc.OnClickSendValue += instance.OnSelectedQuestion;
				}
			}
			count++;
		}

		isCurrentlyInFlaggedQuestions = true;

		instance.currentButtonDeletionIndex = 0;
	}

	public void ClearContentOnly (){
		for (int i = 0; i < instance.contentTarget.transform.childCount; i++) {
			if (instance.contentTarget.transform.GetChild (i) != instance.transform) {
				instance.contentTarget.transform.GetChild (i).GetComponentInChildren<EmptyButtonContainer> ().OnClickSendValue -= instance.OnClick;
				Destroy (instance.contentTarget.transform.GetChild (i).gameObject);
			}
		}
	}

	public static void Clear(){
		instance.ClearContentOnly ();
		instance.currentButtonDeletionIndex = -1;
	}
}