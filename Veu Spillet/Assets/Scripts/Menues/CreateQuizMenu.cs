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
		contentButton = Resources.Load ("Empty Button") as GameObject;
		Hide ();
	}

	public override void Show ()
	{
		instance.gameObject.SetActive (true);
		InstantiateCategoryButtons ();
		BackToMenu.isCurrentlyInAdminSubMenu = true;
	}

	public override void Hide ()
	{
		Clear ();
		instance.gameObject.SetActive (false);
	}

	public void OnClick(int categoryIndex){
		category = SetupCategories.categories [categoryIndex];
	}

	private static void InstantiateCategoryButtons(){
		int count = 0;
		Debug.Log (SetupCategories.categories.Count);
		foreach (Category cat in SetupCategories.categories) {
			GameObject go = Instantiate (contentButton, instance.contentTarget.transform);
			go.GetComponentInChildren<Text> ().text = cat.name;
			EmptyButtonContainer ebc = go.GetComponentInChildren<EmptyButtonContainer> ();
			ebc.myIndex = count;
			ebc.OnClickSendValue += instance.OnClick;
			count++;
		}
	}

	public void CreateQuiz(){
		if (category.id > 0) {
			Quiz newQuiz = new Quiz (0, username.text, category.id);
			DatabaseSaver.instance.SaveQuiz (newQuiz);
		}
	}

	public static void Clear(){
		instance.category = new Category ();
		for (int i = 0; i < instance.contentTarget.transform.childCount; i++) {
			if (instance.contentTarget.transform.GetChild (i) != instance.transform) {
				instance.contentTarget.transform.GetChild (i).GetComponentInChildren<EmptyButtonContainer> ().OnClickSendValue -= instance.OnClick;
				Destroy (instance.contentTarget.transform.GetChild (i).gameObject);
			}
		}
	}

}