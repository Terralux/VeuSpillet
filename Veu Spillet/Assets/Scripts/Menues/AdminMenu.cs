using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AdminMenu : BaseMenu {

	public static AdminMenu instance;

	public void Awake(){
		if (instance != null) {
			Destroy (this);
		} else {
			instance = this;
		}
		Hide ();
	}

	public override void Show ()
	{
		instance.gameObject.SetActive (true);
		SetupUsers.instance.Reload ();
		SetupCategories.instance.Reload ();
		SetupQuizzes.instance.LoadAllQuizzes ();
		SetupQuestions.instance.LoadAllQuestions ();
		SetupQuestions.instance.LoadReportedQuestionIDs ();
	}

	public override void Hide ()
	{
		instance.gameObject.SetActive (false);
	}

	public void CreateUser(){
		CreateUserMenu.instance.Show();
		BackToMenu.isCurrentlyInAdminSubMenu = true;
	}

	public void CreateQuiz(){
		CreateQuizMenu.instance.Show();
		BackToMenu.isCurrentlyInAdminSubMenu = true;
	}

	public void CreateCategory(){
		CreateCategoryMenu.instance.Show();
		BackToMenu.isCurrentlyInAdminSubMenu = true;
	}

	public void DeleteUser(){
		DeleteFromListMenu.instance.Show (0);
		BackToMenu.isCurrentlyInAdminSubMenu = true;
	}

	public void DeleteQuiz(){
		DeleteFromListMenu.instance.Show (1);
		BackToMenu.isCurrentlyInAdminSubMenu = true;
	}

	public void DeleteCategory(){
		DeleteFromListMenu.instance.Show (2);
		BackToMenu.isCurrentlyInAdminSubMenu = true;
	}

	public void DeleteQuestion(){
		DeleteQuestionMenu.instance.Show ();
		BackToMenu.isCurrentlyInAdminSubMenu = true;
	}

	public void ChangeQuestion(){
		ChangeQuestionMenu.instance.Show();
		BackToMenu.isCurrentlyInAdminSubMenu = true;
	}
}