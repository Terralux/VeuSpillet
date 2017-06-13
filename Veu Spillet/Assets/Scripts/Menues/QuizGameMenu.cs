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

	public static void AnsweredQuestion(int buttonIndex){
		currentSession.StoreAnswer (buttonIndex);		
		(instance as QuizGameMenu).SetupQuestionUI (currentSession.GetNextQuestion ());
	}
}