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

	void Awake(){
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
		InstantiateQuizButtons ();
		BackToMenu.isCurrentlyInAdminSubMenu = true;
	}

	public override void Hide ()
	{
		Clear ();
		instance.gameObject.SetActive (false);
	}

	public void OnClick(int quizButton){
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
		Question newQuestion = new Question (quiz.quizID, 0, question.text, correctAnswer.text, wrongAnswer1.text, wrongAnswer2.text, wrongAnswer3.text);
		DatabaseSaver.instance.SaveQuestion (newQuestion);
	}

	public static void Clear(){
		instance.quiz = new Quiz ();

		for (int i = 0; i < instance.contentTarget.transform.childCount; i++) {
			if (instance.contentTarget.transform.GetChild (i) != instance.transform) {
				instance.contentTarget.transform.GetChild (i).GetComponentInChildren<EmptyButtonContainer> ().OnClickSendValue -= instance.OnClick;
				Destroy (instance.contentTarget.transform.GetChild (i).gameObject);
			}
		}

		instance.question.text = "";
		instance.correctAnswer.text = "";
		instance.wrongAnswer1.text = "";
		instance.wrongAnswer2.text = "";
		instance.wrongAnswer3.text = "";
	}
}