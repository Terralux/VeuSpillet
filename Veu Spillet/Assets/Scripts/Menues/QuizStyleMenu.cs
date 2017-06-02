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
		QuizCategoryMenu.Show (new QuizSession (isChallengingUser));
		Hide ();
	}
}