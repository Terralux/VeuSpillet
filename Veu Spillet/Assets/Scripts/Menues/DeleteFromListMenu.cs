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
	}

	public override void Hide ()
	{
		instance.gameObject.SetActive (false);
	}

	public void OnClick(int buttonIndex){
		switch (deletionIndex) {
		case 0:
			DatabaseDeleter.instance.DeleteUser(SetupUsers.users[buttonIndex]);
			break;
		case 1:
			DatabaseDeleter.instance.DeleteQuiz(SetupQuizzes.quizzes[buttonIndex]);
			break;
		case 2:
			DatabaseDeleter.instance.DeleteCategory(SetupCategories.categories[buttonIndex]);
			break;
		}
	}

	private static void InstantiateUserButtons(){
		int count = 0;
		foreach (User user in SetupUsers.users) {
			GameObject go = Instantiate (contentButton, instance.contentTarget.transform);
			go.GetComponentInChildren<Text> ().text = user.userName;
			go.GetComponentInChildren<EmptyButtonContainer> ().OnClickSendValue += instance.OnClick;
			count++;
		}
	}

	private static void InstantiateQuizButtons(){
		int count = 0;
		foreach (Quiz quiz in SetupQuizzes.quizzes) {
			GameObject go = Instantiate (contentButton, instance.contentTarget.transform);
			go.GetComponentInChildren<Text> ().text = quiz.quizName;
			go.GetComponentInChildren<EmptyButtonContainer> ().OnClickSendValue += instance.OnClick;
			count++;
		}
	}

	private static void InstantiateCategoryButtons(){
		int count = 0;
		foreach (Category cat in SetupCategories.categories) {
			GameObject go = Instantiate (contentButton, instance.contentTarget.transform);
			go.GetComponentInChildren<Text> ().text = cat.name;
			go.GetComponentInChildren<EmptyButtonContainer> ().OnClickSendValue += instance.OnClick;
			count++;
		}
	}

	public static void Clear(){
		foreach (Transform t in instance.contentTarget.GetComponentsInChildren<Transform>()) {
			if (t != instance.contentTarget.transform) {
				t.GetComponentInChildren<EmptyButtonContainer> ().OnClickSendValue -= instance.OnClick;
				Destroy (t.gameObject);
			}
		}
	}
}