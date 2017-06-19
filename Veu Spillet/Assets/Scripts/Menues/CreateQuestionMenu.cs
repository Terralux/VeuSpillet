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
	}

	public override void Hide ()
	{
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
		AdminMenu.instance.Show ();
		instance.Hide ();
		Clear ();
	}

	public static void Clear(){
		instance.quiz = new Quiz ();

		foreach (Transform t in instance.contentTarget.GetComponentsInChildren<Transform>()) {
			if (t != instance.contentTarget.transform) {
				t.GetComponentInChildren<EmptyButtonContainer> ().OnClickSendValue -= instance.OnClick;
				Destroy (t.gameObject);
			}
		}

		instance.question.text = "";
		instance.correctAnswer.text = "";
		instance.wrongAnswer1.text = "";
		instance.wrongAnswer2.text = "";
		instance.wrongAnswer3.text = "";
	}
}