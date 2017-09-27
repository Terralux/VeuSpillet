using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DatabaseClassifications;

public class DeleteQuestionMenu : BaseMenu {

	public static DeleteQuestionMenu instance;

	public static GameObject contentButton;
	public GameObject contentTarget;

	public Text errorText;

	public GameObject deleteWarningPanel;
	private int currentButtonDeletionIndex = -1;

	private Question selectedQuestion;

	public void Awake(){
		if (instance != null) {
			Destroy (this);
		} else {
			instance = this;
		}
		if (Application.isMobilePlatform) {
			contentButton = Resources.Load ("Empty Button Mobile") as GameObject;
		} else {
			contentButton = Resources.Load ("Empty Button") as GameObject;
		}
		Hide ();
	}

	public override void Show ()
	{
		instance.gameObject.SetActive (true);
		deleteWarningPanel.SetActive (false);
		BackToMenu.isCurrentlyInAdminSubMenu = true;
		Clear ();
		InstantiateQuizButtons ();
	}

	public override void Hide ()
	{
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

	public void OnSelectedQuestion(int questionIndex){
		if (currentButtonDeletionIndex >= 0) {
			instance.contentTarget.transform.GetChild (currentButtonDeletionIndex).GetComponent<Image> ().color = Color.white;
		}
		foreach (EmptyButtonContainer e in instance.contentTarget.transform.GetComponentsInChildren<EmptyButtonContainer>()) {
			if (e.myIndex == questionIndex) {
				e.GetComponent<Image> ().color = new Color (1f, 0.7f, 0f, 1f);
			}
		}
		deleteWarningPanel.SetActive (true);
		currentButtonDeletionIndex = questionIndex;
	}

	public void Delete(){
		DatabaseDeleter.instance.DeleteQuestion (SetupQuestions.questions [currentButtonDeletionIndex]);

		foreach (EmptyButtonContainer e in instance.contentTarget.transform.GetComponentsInChildren<EmptyButtonContainer>()) {
			if (e.myIndex == currentButtonDeletionIndex) {
				e.gameObject.SetActive (false);
			}
		}

		deleteWarningPanel.SetActive (false);
	}

	public void Return(){
		instance.contentTarget.transform.GetChild (currentButtonDeletionIndex).GetComponent<Image> ().color = Color.white;
		deleteWarningPanel.SetActive (false);
		currentButtonDeletionIndex = -1;
	}

	private static void InstantiateQuizButtons(){
		instance.ClearContentOnly ();
		int count = 0;
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
		instance.currentButtonDeletionIndex = 0;
	}

	public void Error(){
		errorText.gameObject.SetActive (true);
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