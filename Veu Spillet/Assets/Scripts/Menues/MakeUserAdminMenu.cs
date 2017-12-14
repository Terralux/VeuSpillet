using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DatabaseClassifications;

public class MakeUserAdminMenu : BaseMenu {

	public static MakeUserAdminMenu instance;

	public static GameObject contentButton;
	public GameObject contentTarget;

	public GameObject feedbackPanel;

	public void Awake(){
		if (instance != null) {
			Destroy (this);
		} else {
			instance = this;
		}

		if (Application.isMobilePlatform) {
			contentButton = Resources.Load ("Empty Button Mobile") as GameObject;
		} else {
			contentButton = Resources.Load ("Empty Button") as GameObject;
		}

		Hide ();
	}

	public override void Show (){
		instance.gameObject.SetActive (true);
		BackToMenu.isCurrentlyInAdminSubMenu = true;
		InstantiateUserButtons ();
	}

	public override void Hide (){
		instance.gameObject.SetActive (false);
	}

	public void OnClick(int buttonIndex){
		User user = new User (SetupUsers.users [buttonIndex].userID, SetupUsers.users [buttonIndex].teamID, SetupUsers.users [buttonIndex].userName, SetupUsers.users [buttonIndex].userPassword, !SetupUsers.users [buttonIndex].isAdmin);
		SetupUsers.users [buttonIndex] = user;
		SetupUsers.instance.UpdateUser (user);

		UpdateButtonTexts ();
		StartCoroutine (WaitForFeedbackPanel ());
	}

	public void Return(){
		feedbackPanel.SetActive (false);
	}

	IEnumerator WaitForFeedbackPanel(){
		feedbackPanel.SetActive (true);
		yield return new WaitForSeconds (3f);
		feedbackPanel.SetActive (false);
	}

	private static void InstantiateUserButtons(){
		int count = 0;
		foreach (User user in SetupUsers.users) {
			if (user.userID != 1) {
				GameObject go = Instantiate (contentButton, instance.contentTarget.transform);
				go.GetComponentInChildren<Text> ().text = (user.isAdmin ? "(Admin) " : "") + user.userName;
				EmptyButtonContainer ebc = go.GetComponentInChildren<EmptyButtonContainer> ();
				ebc.myIndex = count;
				ebc.OnClickSendValue += instance.OnClick;
			}
			count++;
		}
	}

	public void UpdateButtonTexts(){
		for (int i = 0; i < instance.contentTarget.transform.childCount; i++) {
			if (instance.contentTarget.transform.GetChild (i) != instance.transform) {
				User user = SetupUsers.users [instance.contentTarget.transform.GetChild (i).GetComponentInChildren<EmptyButtonContainer> ().myIndex];
				instance.contentTarget.transform.GetChild (i).GetComponentInChildren<Text> ().text = (user.isAdmin ? "(Admin) " : "") + user.userName;
			}
		}
	}

	public static void Clear(){
		for (int i = 0; i < instance.contentTarget.transform.childCount; i++) {
			if (instance.contentTarget.transform.GetChild (i) != instance.transform) {
				instance.contentTarget.transform.GetChild (i).GetComponentInChildren<EmptyButtonContainer> ().OnClickSendValue -= instance.OnClick;
				Destroy (instance.contentTarget.transform.GetChild (i).gameObject);
			}
		}
	}
}