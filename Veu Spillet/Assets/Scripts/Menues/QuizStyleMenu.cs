using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuizStyleMenu : BaseMenu {

	public void Awake(){
		if (instance != null) {
			Destroy (this);
		} else {
			instance = this;
		}
		Hide ();
	}

	public static QuizStyleMenu instance;

	public override void Show ()
	{
		instance.gameObject.SetActive (true);
	}

	public override void Hide ()
	{
		instance.gameObject.SetActive (false);
	}

	public void ChoseAQuizStyle(bool isChallengingUser){
		QuizSession currentSession = new QuizSession(isChallengingUser);
		currentSession.challenger = DataContainer.currentLoggedUser;
		QuizCategoryMenu.Show (currentSession);
		Hide ();
	}
}