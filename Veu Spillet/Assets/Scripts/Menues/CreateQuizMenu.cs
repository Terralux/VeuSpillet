using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DatabaseClassifications;

public class CreateQuizMenu : BaseMenu {
	public static CreateQuizMenu instance;

	public InputField username;
	private Category category;

	public static GameObject contentButton;
	public GameObject contentTarget;

	void Awake(){
		if (instance != null) {
			Destroy (this);
		} else {
			instance = this;
		}
		Hide ();
	}

	public override void Show ()
	{
		instance.gameObject.SetActive (true);
	}

	public override void Hide ()
	{
		instance.gameObject.SetActive (false);
	}

	public void OnClick(int categoryIndex){
		category = SetupCategories.categories [categoryIndex];
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

	public void CreateQuiz(){
		Quiz newQuiz = new Quiz (0, username.text, category.id);
		DatabaseSaver.instance.SaveQuiz (newQuiz);
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