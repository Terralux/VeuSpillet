using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DatabaseClassifications;

public class QuizGameMenu : BaseMenu {

	public Text question;
	public Text answer1;
	public Text answer2;
	public Text answer3;
	public Text answer4;

	public static QuizSession currentSession;

	public void Awake(){
		if (instance != null) {
			Destroy (this);
		} else {
			instance = this;
		}
		Hide ();
	}

	public static BaseMenu instance;

	public static void Show (QuizSession currentSession){
		QuizGameMenu.currentSession = currentSession;
		SetupQuestions.instance.LoadQuestions (currentSession.quiz.quizID);
		instance.Show ();
	}

	public override void Show ()
	{
		instance.gameObject.SetActive (true);
	}

	public override void Hide ()
	{
		instance.gameObject.SetActive (false);
	}

	public static void InitiateQuiz(Question[] questions){
		currentSession.questions = questions;
		(instance as QuizGameMenu).SetupQuestionUI (currentSession.StartQuiz ());
	}

	public void SetupQuestionUI(string[] answers){
		(instance as QuizGameMenu).answer1.text = answers [0];
		(instance as QuizGameMenu).answer2.text = answers [1];
		(instance as QuizGameMenu).answer3.text = answers [2];
		(instance as QuizGameMenu).answer4.text = answers [3];

		(instance as QuizGameMenu).question.text = currentSession.GetCurrentQuestion ();
	}

	public void AnsweredQuestion(int buttonIndex){

		if (currentSession.GetCurrentAnswer () == buttonIndex) {
			switch (buttonIndex) {
			case 0:
				answer1.color = Color.green;
				break;
			case 1:
				answer2.color = Color.green;
				break;
			case 2:
				answer3.color = Color.green;
				break;
			case 3:
				answer4.color = Color.green;
				break;
			}
		} else {
			switch (buttonIndex) {
			case 0:
				answer1.color = Color.red;
				break;
			case 1:
				answer2.color = Color.red;
				break;
			case 2:
				answer3.color = Color.red;
				break;
			case 3:
				answer4.color = Color.red;
				break;
			}

			switch (currentSession.GetCurrentAnswer ()) {
			case 0:
				answer1.color = Color.green;
				break;
			case 1:
				answer2.color = Color.green;
				break;
			case 2:
				answer3.color = Color.green;
				break;
			case 3:
				answer4.color = Color.green;
				break;
			}
		}

		currentSession.StoreAnswer (buttonIndex);
		StartCoroutine (WaitForNextQuestion ());
	}

	IEnumerator WaitForNextQuestion(){
		answer1.GetComponent<Button> ().interactable = false;
		answer2.GetComponent<Button> ().interactable = false;
		answer3.GetComponent<Button> ().interactable = false;
		answer4.GetComponent<Button> ().interactable = false;

		yield return new WaitForSeconds (2f);

		answer1.GetComponent<Button> ().interactable = true;
		answer2.GetComponent<Button> ().interactable = true;
		answer3.GetComponent<Button> ().interactable = true;
		answer4.GetComponent<Button> ().interactable = true;

		answer1.color = Color.white;
		answer2.color = Color.white;
		answer3.color = Color.white;
		answer4.color = Color.white;

		if (currentSession.hasMoreQuestions) {
			(instance as QuizGameMenu).SetupQuestionUI (currentSession.GetNextQuestion ());
		} else {
			QuizResultsMenu.Show (currentSession);
			Hide ();
			Clear ();
		}
	}

	private void Clear(){
		question.text = "";
		answer1.text = "";
		answer2.text = "";
		answer3.text = "";
		answer4.text = "";
	}
}