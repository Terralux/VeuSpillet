﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DatabaseClassifications;

public class CreateUserMenu : BaseMenu {
	public static CreateUserMenu instance;

	public InputField username;
	public InputField password;

	public Toggle hasAdminRights;

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

	public void CreateUser(){
		User newUser = new User (0, 0, username.text, password.text, hasAdminRights.isOn);
		DatabaseSaver.instance.SaveUser (newUser);
		username.text = "";
		password.text = "";
		StartCoroutine (EnableFeedback());
	}

	private IEnumerator EnableFeedback(){
		feedbackPanel.SetActive (true);
		yield return new WaitForSeconds (2f);
		feedbackPanel.SetActive (false);
	}

}