using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuizUserMenu : BaseMenu {

	private static QuizSession currentSession;

	public void Awake(){
		if (instance != null) {
			Destroy (this);
		} else {
			instance = this;
		}
		Hide ();
	}

	public static QuizUserMenu instance;

	public static void Show (QuizSession newCurrentSession)
	{
		currentSession = newCurrentSession;
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