using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DatabaseClassifications;

public class CreateQuestionMenu : BaseMenu {
	public static CreateQuestionMenu instance;

	public InputField question;
	public InputField correctAnswer;
	public InputField wrongAnswer1;
	public InputField wrongAnswer2;
	public InputField wrongAnswer3;

	private Quiz quiz;

	public static GameObject contentButton;
	public GameObject contentTarget;

	public GameObject feedbackPanel;

	private int quizIndex;

	void Awake(){
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
		InstantiateQuizButtons ();
		BackToMenu.isCurrentlyInAdminSubMenu = true;
	}

	public override void Hide ()
	{
		Clear ();
		instance.gameObject.SetActive (false);
	}

	public void OnClick(int quizButton){
		if (quizIndex >= 0) {
			instance.contentTarget.transform.GetChild (quizIndex).GetComponent<Image> ().color = Color.white;
		}
		instance.contentTarget.transform.GetChild (quizButton).GetComponent<Image> ().color = new Color (1f, 0.7f, 0f, 1f);

		quizIndex = quizButton;
		quiz = SetupQuizzes.quizzes [quizButton];
	}

	private static void InstantiateQuizButtons(){
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

	public void CreateQuestion(){
		if (quiz.quizID != 0) {
			Question newQuestion = new Question (quiz.quizID, 0, question.text, correctAnswer.text, wrongAnswer1.text, wrongAnswer2.text, wrongAnswer3.text);
			DatabaseSaver.instance.SaveQuestion (newQuestion);
			StartCoroutine (EnableFeedback ());
			ClearFieldsOnly ();
		}
	}

	public static void Clear(){
		for (int i = 0; i < instance.contentTarget.transform.childCount; i++) {
			if (instance.contentTarget.transform.GetChild (i) != instance.transform) {
				instance.contentTarget.transform.GetChild (i).GetComponentInChildren<EmptyButtonContainer> ().OnClickSendValue -= instance.OnClick;
				Destroy (instance.contentTarget.transform.GetChild (i).gameObject);
			}
		}

		instance.ClearFieldsOnly ();
	}

	public void ClearFieldsOnly (){
		instance.quiz = new Quiz ();

		if (instance.contentTarget.transform.childCount > quizIndex) {
			instance.contentTarget.transform.GetChild (quizIndex).GetComponent<Image> ().color = Color.white;
		}

		instance.question.text = "";
		instance.correctAnswer.text = "";
		instance.wrongAnswer1.text = "";
		instance.wrongAnswer2.text = "";
		instance.wrongAnswer3.text = "";
	}

	private IEnumerator EnableFeedback(){
		feedbackPanel.SetActive (true);
		yield return new WaitForSeconds (2f);
		feedbackPanel.SetActive (false);
	}
}