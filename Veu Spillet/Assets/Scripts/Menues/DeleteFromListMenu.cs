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
		case 3:
			//Load Questions
			InstantiateQuestionButtons ();
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
		case 3:
			DatabaseDeleter.instance.DeleteQuestion (SetupQuestions.questions[buttonIndex]);
			break;
		}
	}

	private static void InstantiateUserButtons(){
		int count = 0;
		foreach (User user in SetupUsers.users) {
			GameObject go = Instantiate (contentButton, instance.contentTarget.transform);
			go.GetComponentInChildren<Text> ().text = user.userName;
			EmptyButtonContainer ebc = go.GetComponentInChildren<EmptyButtonContainer> ();
			ebc.myIndex = count;
			ebc.OnClickSendValue += instance.OnClick;
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

	private static void InstantiateQuestionButtons(){
		int count = 0;
		foreach (Question question in SetupQuestions.questions) {
			GameObject go = Instantiate (contentButton, instance.contentTarget.transform);
			go.GetComponentInChildren<Text> ().text = question.question;
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

		DeleteFromListMenu.instance.Hide ();
		AdminMenu.instance.Show ();
	}
}