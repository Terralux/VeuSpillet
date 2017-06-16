using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DatabaseClassifications;

public class QuizUserMenu : BaseMenu {

	private static QuizSession currentSession;
	public GameObject contentTarget;
	private static GameObject contentButton;

	public void Awake(){
		if (instance != null) {
			Destroy (this);
		} else {
			instance = this;
		}
		contentButton = Resources.Load ("User Button") as GameObject;
		Hide ();
	}

	public static QuizUserMenu instance;

	public static void Show (QuizSession newCurrentSession)
	{
		currentSession = newCurrentSession;
		InstantiateUserButtons ();
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

	public void ChoseADefender(User user){
		currentSession.defender = user;
		QuizGameMenu.Show (currentSession);
		Clear ();
		Hide ();
	}

	public static void InstantiateUserButtons(){
		foreach (User user in SetupUsers.users) {
			if (user.userID != DataContainer.currentLoggedUser.userID) {
				GameObject go = Instantiate (contentButton, instance.contentTarget.transform);
				go.GetComponentInChildren<Text> ().text = user.userName;
				go.GetComponentInChildren<UserContainer> ().myUser = user;
			}
		}
	}

	private void Clear(){
		foreach (Transform t in instance.contentTarget.transform) {
			if (t != instance.contentTarget.transform) {
				Destroy (t.gameObject);
			}
		}
	}
}