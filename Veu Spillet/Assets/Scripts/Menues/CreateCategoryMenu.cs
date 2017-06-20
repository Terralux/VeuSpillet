using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DatabaseClassifications;

public class CreateCategoryMenu : BaseMenu {
	public static CreateCategoryMenu instance;

	public InputField categoryNameField;

	public GameObject feedbackPanel;

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
		BackToMenu.isCurrentlyInAdminSubMenu = true;
	}

	public override void Hide ()
	{
		instance.gameObject.SetActive (false);
	}

	public void CreateCategory(){
		if (categoryNameField.text != "") {
			Category newCategory = new Category (0, categoryNameField.text);
			DatabaseSaver.instance.SaveCategory (newCategory);
			categoryNameField.text = "";
			StartCoroutine (EnableFeedback());
		}
	}

	private IEnumerator EnableFeedback(){
		feedbackPanel.SetActive (true);
		yield return new WaitForSeconds (2f);
		feedbackPanel.SetActive (false);
	}
}