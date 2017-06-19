using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DatabaseClassifications;

public class CreateUserMenu : BaseMenu {
	public static CreateUserMenu instance;

	public InputField username;
	public InputField password;

	public Toggle hasAdminRights;

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

	public void CreateUser(){
		User newUser = new User (0, 0, username.text, password.text, hasAdminRights.isOn);
		DatabaseSaver.instance.SaveUser (newUser);
		AdminMenu.instance.Show ();
		instance.Hide ();
		username.text = "";
		password.text = "";
	}

}