using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DatabaseClassifications;

public class QuizMenu : BaseMenu {

	public static QuizSession currentSession;
	public GameObject contentTarget;
	private static GameObject contentButton;

	public void Awake(){
		if (instance != null) {
			Destroy (this);
		} else {
			instance = this;
		}
		contentButton = Resources.Load ("Quiz Button") as GameObject;
		Debug.Log (contentButton);
		Hide ();
	}

	public static QuizMenu instance;

	public static void Show(QuizSession currentSession){
		QuizMenu.currentSession = currentSession;
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

	public void ChoseAQuiz(Quiz quiz){
		currentSession.quiz = quiz;
		QuizUserMenu.Show (currentSession);
		Hide ();
	}

	public static void InstantiateQuizButtons(){
		foreach (Quiz quiz in SetupQuizzes.quizzes) {
			GameObject go = Instantiate (contentButton, instance.contentTarget.transform);
			go.GetComponentInChildren<Text> ().text = quiz.quizName;
			go.GetComponentInChildren<QuizContainer> ().myQuiz = quiz;
		}
	}
}