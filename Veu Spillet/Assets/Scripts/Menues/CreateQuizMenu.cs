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

	public GameObject feedbackPanel;

	private int categoryIndex;

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
		if (this.categoryIndex >= 0) {
			instance.contentTarget.transform.GetChild (this.categoryIndex).GetComponent<Image> ().color = Color.white;
		}
		instance.contentTarget.transform.GetChild (categoryIndex).GetComponent<Image> ().color = new Color (1f, 0.7f, 0f, 1f);

		this.categoryIndex = categoryIndex;
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
			StartCoroutine (EnableFeedback());
			ClearFieldsOnly ();
		}
	}

	public void ClearFieldsOnly (){
		instance.category = new Category ();

		if (instance.contentTarget.transform.childCount > categoryIndex && categoryIndex >= 0) {
			instance.contentTarget.transform.GetChild (categoryIndex).GetComponent<Image> ().color = Color.white;
		}

		instance.username.text = "";
		instance.categoryIndex = -1;
	}

	public static void Clear(){
		for (int i = 0; i < instance.contentTarget.transform.childCount; i++) {
			if (instance.contentTarget.transform.GetChild (i) != instance.transform) {
				instance.contentTarget.transform.GetChild (i).GetComponentInChildren<EmptyButtonContainer> ().OnClickSendValue -= instance.OnClick;
				Destroy (instance.contentTarget.transform.GetChild (i).gameObject);
			}
		}
		instance.ClearFieldsOnly ();
	}

	private IEnumerator EnableFeedback(){
		feedbackPanel.SetActive (true);
		yield return new WaitForSeconds (2f);
		feedbackPanel.SetActive (false);
	}

}