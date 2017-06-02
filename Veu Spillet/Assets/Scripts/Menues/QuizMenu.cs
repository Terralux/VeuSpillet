using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuizMenu : BaseMenu {

	public static QuizSession currentSession;

	public void Awake(){
		if (instance != null) {
			Destroy (this);
		} else {
			instance = this;
		}
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
}