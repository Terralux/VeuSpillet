using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DatabaseClassifications;

public class QuizGameMenu : BaseMenu {

	public Text question;
	public Text[] answers;

	public static QuizSession currentSession;

	private string[] correctOrderQuestions;

	public void Awake(){
		if (instance != null) {
			Destroy (this);
		} else {
			instance = this;
		}
		Hide ();
	}

	public static QuizGameMenu instance;

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

		correctOrderQuestions = new string[]{ answers [0], answers [1], answers [2], answers [3] };

		for (int i = 0; i < 20; i++) {
			int rand = Random.Range (0, answers.Length);

			string temp = answers [i%4];
			answers [i%4] = answers [rand];
			answers [rand] = temp;
		}

		for (int i = 0; i < answers.Length; i++) {
			(instance as QuizGameMenu).answers [i].text = answers [i];
		}

		(instance as QuizGameMenu).question.text = currentSession.GetCurrentQuestion ();
	}

	public void AnsweredQuestion(int buttonIndex){

		int convertedAnswerIndex = 0;

		if (answers [buttonIndex].text == correctOrderQuestions [0]) {
			answers [buttonIndex].color = Color.green;
		} else {
			answers [buttonIndex].color = Color.red;
		}

		for (int i = 0; i < answers.Length; i++) {
			if (answers[i].text == correctOrderQuestions[0]) {
				answers [i].color = Color.green;
			}
			if (answers [buttonIndex].text == correctOrderQuestions [i]) {
				convertedAnswerIndex = i;
			}
		}

		currentSession.StoreAnswer (convertedAnswerIndex);
		StartCoroutine (WaitForNextQuestion ());
	}

	IEnumerator WaitForNextQuestion(){
		for (int i = 0; i < answers.Length; i++) {
			answers [i].GetComponent<Button> ().interactable = false;
		}

		yield return new WaitForSeconds (0f);

		//yield return new WaitForSeconds (1.5f);

		for (int i = 0; i < answers.Length; i++) {
			answers [i].GetComponent<Button> ().interactable = true;
			answers [i].color = Color.white;
		}

		if (currentSession.hasMoreQuestions) {
			(instance as QuizGameMenu).SetupQuestionUI (currentSession.GetNextQuestion ());
		} else {
			QuizResultsMenu.Show (currentSession);
			Hide ();
			Clear ();
		}
	}

	public void Clear(){
		question.text = "";

		for (int i = 0; i < answers.Length; i++) {
			answers [i].text = "";
		}
	}
}