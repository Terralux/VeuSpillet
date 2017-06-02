using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainMenu : BaseMenu {

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
		instance.gameObject.SetActive (true);
	}

	public override void Hide ()
	{
		instance.gameObject.SetActive (false);
	}

	public void GoToQuizStyleSelection(){
		QuizStyleMenu.instance.Show ();
		instance.Hide ();
	}

	public void GoToResults(){
		ResultsMenu.instance.Show ();
		instance.Hide ();
	}

	public void GoToLoginMenu(){
		DataContainer.currentLoggedUser = new DatabaseClassifications.User ();
		instance.Hide ();
	}
}