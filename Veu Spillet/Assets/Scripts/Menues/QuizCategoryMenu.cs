using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DatabaseClassifications;

public class QuizCategoryMenu : BaseMenu {

	private static QuizSession currentSession;
	public GameObject contentTarget;
	private static GameObject contentButton;

	public void Awake(){
		if (instance != null) {
			Destroy (this);
		} else {
			instance = this;
		}
		if (Application.isMobilePlatform) {
			contentButton = Resources.Load ("Category Button Mobile") as GameObject;
		} else {
			contentButton = Resources.Load ("Category Button") as GameObject;
		}
		Hide ();
	}

	public static QuizCategoryMenu instance;

	public static void Show (QuizSession newCurrentSession)
	{
		InstantiateCategoryButtons ();
		currentSession = newCurrentSession;
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

	public void ChoseACategory(Category category){
		Debug.Log ("Received Category");

		if (category.name != null) {
			SetupQuizzes.instance.LoadQuizzes (category.id);

			currentSession.category = category;

			QuizMenu.Show (currentSession);
		} else {
			if (currentSession.isChallengingUser) {
				QuizUserMenu.Show (currentSession);
			} else {
				QuizGameMenu.Show (currentSession);
			}
		}

		Clear ();
		Hide ();
	}

	private static void InstantiateCategoryButtons(){
		GameObject go = Instantiate (contentButton, instance.contentTarget.transform);
		go.GetComponentInChildren<Text> ().text = "Blandet";

		foreach (Category cat in SetupCategories.categories) {
			go = Instantiate (contentButton, instance.contentTarget.transform);
			go.GetComponentInChildren<Text> ().text = cat.name;
			go.GetComponentInChildren<CategoryContainer> ().myCategory = cat;
		}
	}

	public void Clear(){
		foreach (Transform t in instance.contentTarget.transform) {
			if (t != instance.contentTarget.transform) {
				Destroy (t.gameObject);
			}
		}
	}
}