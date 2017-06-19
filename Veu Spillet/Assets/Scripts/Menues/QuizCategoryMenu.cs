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
		contentButton = Resources.Load ("Category Button") as GameObject;
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

		SetupQuizzes.instance.LoadQuizzes (category.id);

		currentSession.category = category;
		QuizMenu.Show (currentSession);
		Clear ();
		Hide ();
	}

	private static void InstantiateCategoryButtons(){
		foreach (Category cat in SetupCategories.categories) {
			GameObject go = Instantiate (contentButton, instance.contentTarget.transform);
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