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
	}

	public override void Hide ()
	{
		instance.gameObject.SetActive (false);
	}

	public void CreateUser(){
		CreateUserMenu.instance.Show();
	}

	public void CreateQuiz(){
		CreateQuizMenu.instance.Show();
	}

	public void CreateCategory(){
		CreateCategoryMenu.instance.Show();
	}

	public void DeleteUser(){
		DeleteFromListMenu.instance.Show (0);
	}

	public void DeleteQuiz(){
		DeleteFromListMenu.instance.Show (1);
	}

	public void DeleteCategory(){
		DeleteFromListMenu.instance.Show (2);
	}

	public void DeleteQuestion(){
		DeleteFromListMenu.instance.Show (3);
	}
}