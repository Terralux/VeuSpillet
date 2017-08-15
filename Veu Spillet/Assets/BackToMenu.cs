using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackToMenu : MonoBehaviour {

	public static BackToMenu instance;

	public static bool isCurrentlyInAdminSubMenu = false;
	public GameObject feedbackPanel;

	void Awake(){
		if (instance != null) {
			Destroy (this);
		} else {
			instance = this;
		}
		Hide ();
	}

	public void Show(){
		instance.gameObject.SetActive (true);
	}

	public void Hide(){
		instance.gameObject.SetActive (false);
	}

	private void BackToAdminMenu(){
		DeleteFromListMenu.Clear ();
		DeleteFromListMenu.instance.Hide ();
		CreateUserMenu.instance.Hide ();
		CreateQuizMenu.instance.Hide ();
		CreateCategoryMenu.instance.Hide ();
		CreateQuestionMenu.instance.Hide ();
		AdminMenu.instance.Show ();
		MainMenu.instance.Hide ();
		DeleteQuestionMenu.Clear ();
		DeleteQuestionMenu.instance.Hide ();
		ChangeQuestionMenu.instance.Hide ();
	}

	public void BackToMainMenu(){
		feedbackPanel.SetActive (false);
		InformationMenu.instance.Hide ();
		QuizCategoryMenu.instance.Hide ();
		QuizCategoryMenu.instance.Clear ();
		QuizGameMenu.instance.Hide ();
		QuizGameMenu.instance.Clear ();
		QuizMenu.instance.Hide ();
		QuizMenu.instance.Clear ();
		QuizResultsMenu.instance.Hide ();
		QuizResultsMenu.instance.Clear ();
		QuizStyleMenu.instance.Hide ();
		QuizUserMenu.instance.Hide ();
		QuizUserMenu.instance.Clear ();
		ResultsMenu.instance.Hide ();
		ChallengeMenu.instance.Hide ();
		ChallengeMenu.instance.Clear ();
		EmailUIHandler.instance.Hide ();

		BackToAdminMenu ();

		if (!isCurrentlyInAdminSubMenu) {
			AdminMenu.instance.Hide ();
			MainMenu.instance.Show ();
			ChallengeButton.instance.Show ();
		} else {
			AdminMenu.instance.Show ();
			MainMenu.instance.Hide ();
			ChallengeButton.instance.Hide ();
			isCurrentlyInAdminSubMenu = false;
		}
	}
}