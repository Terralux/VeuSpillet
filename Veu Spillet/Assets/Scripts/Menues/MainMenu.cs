using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainMenu : BaseMenu {

	public GameObject adminOptions;

	public void Awake(){
		if (instance != null) {
			Destroy (this);
		} else {
			instance = this;
		}
		Hide ();
	}

	public static MainMenu instance;

	public override void Show ()
	{
		adminOptions.SetActive (DataContainer.currentLoggedUser.isAdmin);
		instance.gameObject.SetActive (true);
		BackToMenu.instance.Hide ();
		SetupQuizzes.instance.LoadAllQuizzes ();
		SetupResults.instance.Reload ();
		SetupQuestions.instance.LoadAllQuestions ();
	}

	public override void Hide ()
	{
		instance.gameObject.SetActive (false);
	}

	public void GoToQuizStyleSelection(){
		QuizStyleMenu.instance.Show ();
		instance.Hide ();
		BackToMenu.instance.Show();
	}

	public void GoToResults(){
		ResultsMenu.instance.Show ();
		instance.Hide ();
		BackToMenu.instance.Show();
	}

	public void GoToLoginMenu(){
		DataContainer.currentLoggedUser = new DatabaseClassifications.User ();
		instance.Hide ();
		BackToMenu.instance.Hide ();
		ChallengeButton.instance.Hide ();
	}

	public void GoToAdminMenu(){
		AdminMenu.instance.Show ();
		instance.Hide ();
		BackToMenu.instance.Show();
		ChallengeButton.instance.Hide ();
	}

	public void GoToCreateQuestion(){
		CreateQuestionMenu.instance.Show ();
		BackToMenu.instance.Show();
	}

	public void GoToSuggestQuestion(){
		CreateQuestionMenu.instance.Show ();
		BackToMenu.instance.Show();
	}
}