using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DatabaseClassifications;

public class QuizCategoryMenu : BaseMenu, IShowWithArgument {

	private static QuizSession currentSession;

	public void Show (QuizSession newCurrentSession)
	{
		currentSession = newCurrentSession;
		Show ();
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