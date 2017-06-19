using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DatabaseClassifications;

public class CreateCategoryMenu : BaseMenu {
	public static CreateCategoryMenu instance;

	public InputField categoryNameField;

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

	public void CreateCategory(){
		if (categoryNameField.text != "") {
			Category newCategory = new Category (0, categoryNameField.text);
			DatabaseSaver.instance.SaveCategory (newCategory);
			AdminMenu.instance.Show ();
			instance.Hide ();
			categoryNameField.text = "";
		}
	}
}