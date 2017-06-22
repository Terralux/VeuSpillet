using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DatabaseClassifications;

public class DeleteFromListMenu : BaseMenu {

	public static DeleteFromListMenu instance;

	public static GameObject contentButton;
	public GameObject contentTarget;

	private int deletionIndex;

	public Text errorText;

	public GameObject deleteWarningPanel;
	private int currentButtonDeletionIndex = -1;

	public void Awake(){
		if (instance != null) {
			Destroy (this);
		} else {
			instance = this;
		}
		contentButton = Resources.Load ("Empty Button") as GameObject;
		Hide ();
	}

	public void Show(int index){
		instance.Show ();

		deletionIndex = index;

		switch (index) {
		case 0:
			//Load Users
			InstantiateUserButtons();
			break;
		case 1:
			//Load Quizzes
			InstantiateQuizButtons();
			break;
		case 2:
			//Load Categories
			InstantiateCategoryButtons();
			break;
		}
	}

	public override void Show ()
	{
		instance.gameObject.SetActive (true);
		deleteWarningPanel.SetActive (false);
		BackToMenu.isCurrentlyInAdminSubMenu = true;
	}

	public override void Hide ()
	{
		instance.gameObject.SetActive (false);
	}

	public void OnClick(int buttonIndex){
		if (currentButtonDeletionIndex >= 0) {
			instance.contentTarget.transform.GetChild (currentButtonDeletionIndex).GetComponent<Image> ().color = Color.white;
		}
		instance.contentTarget.transform.GetChild (buttonIndex).GetComponent<Image> ().color = new Color (1f, 0.7f, 0f, 1f);

		deleteWarningPanel.SetActive (true);
		currentButtonDeletionIndex = buttonIndex;
	}

	public void Delete(){
		switch (deletionIndex) {
		case 0:
			DatabaseDeleter.instance.DeleteUser(SetupUsers.users[currentButtonDeletionIndex]);
			break;
		case 1:
			DatabaseDeleter.instance.DeleteQuiz(SetupQuizzes.quizzes[currentButtonDeletionIndex]);
			break;
		case 2:
			DatabaseDeleter.instance.DeleteCategory(SetupCategories.categories[currentButtonDeletionIndex]);
			break;
		}

		instance.contentTarget.transform.GetChild (currentButtonDeletionIndex).gameObject.SetActive (false);
		deleteWarningPanel.SetActive (false);
	}

	public void Return(){
		deleteWarningPanel.SetActive (false);
		instance.contentTarget.transform.GetChild (currentButtonDeletionIndex).GetComponent<Image> ().color = new Color (1f, 0.7f, 0f, 1f);
		currentButtonDeletionIndex = -1;
	}

	private static void InstantiateUserButtons(){
		int count = 0;
		foreach (User user in SetupUsers.users) {
			if (user.userID != 1) {
				GameObject go = Instantiate (contentButton, instance.contentTarget.transform);
				go.GetComponentInChildren<Text> ().text = user.userName;
				EmptyButtonContainer ebc = go.GetComponentInChildren<EmptyButtonContainer> ();
				ebc.myIndex = count;
				ebc.OnClickSendValue += instance.OnClick;
			}
			count++;
		}
	}

	private static void InstantiateQuizButtons(){
		int count = 0;
		foreach (Quiz quiz in SetupQuizzes.quizzes) {
			GameObject go = Instantiate (contentButton, instance.contentTarget.transform);
			go.GetComponentInChildren<Text> ().text = quiz.quizName;
			EmptyButtonContainer ebc = go.GetComponentInChildren<EmptyButtonContainer> ();
			ebc.myIndex = count;
			ebc.OnClickSendValue += instance.OnClick;
			count++;
		}
	}

	private static void InstantiateCategoryButtons(){
		int count = 0;
		foreach (Category cat in SetupCategories.categories) {
			GameObject go = Instantiate (contentButton, instance.contentTarget.transform);
			go.GetComponentInChildren<Text> ().text = cat.name;
			EmptyButtonContainer ebc = go.GetComponentInChildren<EmptyButtonContainer> ();
			ebc.myIndex = count;
			ebc.OnClickSendValue += instance.OnClick;
			count++;
		}
	}

	public void Error(){
		errorText.gameObject.SetActive (true);
	}

	public static void Clear(){
		for (int i = 0; i < instance.contentTarget.transform.childCount; i++) {
			if (instance.contentTarget.transform.GetChild (i) != instance.transform) {
				instance.contentTarget.transform.GetChild (i).GetComponentInChildren<EmptyButtonContainer> ().OnClickSendValue -= instance.OnClick;
				Destroy (instance.contentTarget.transform.GetChild (i).gameObject);
			}
		}
		instance.currentButtonDeletionIndex = -1;
	}
}