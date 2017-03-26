using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActivateState : MonoBehaviour {

	public void ActivateMainMenuState(){
		Toolbox.FindRequiredComponent<EventSystem> ().OnLoggedIn ();
	}

	public void ActivateLoginState(){
		Toolbox.FindRequiredComponent<EventSystem> ().OnLoggedOut ();
	}

	public void ActivateQuizState(){
		Toolbox.FindRequiredComponent<EventSystem> ().OnSelectedQuizMenu ();
	}

	public void ActivateResultsState(){
		Toolbox.FindRequiredComponent<EventSystem> ().OnSelectedResultsMenu ();
	}
}