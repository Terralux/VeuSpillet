using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuizStyleMenu : BaseMenu {

	public override void Show ()
	{
		instance.gameObject.SetActive (true);
	}

	public override void Hide ()
	{
		instance.gameObject.SetActive (false);
	}
}