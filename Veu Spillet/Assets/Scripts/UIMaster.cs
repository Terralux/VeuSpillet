﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIMaster : DataReceiver {

	private MenuStates myMenuState = MenuStates.LOGIN;

	public GameObject loginScreen;
	public GameObject loginFailed;
	public GameObject mainMenu;

	public GameObject quizMenu;
	public GameObject resultsMenu;

	void Awake(){
		Toolbox.FindRequiredComponent<EventSystem> ().OnLoggedIn += OnLoggedIn;
		Toolbox.FindRequiredComponent<EventSystem> ().OnFailedLogin += OnFailedLogin;

		Toolbox.FindRequiredComponent<EventSystem> ().OnSelectedQuizMenu += OnSelectedQuizMenu;
		Toolbox.FindRequiredComponent<EventSystem> ().OnSelectedResultsMenu += OnSelectedResultsMenu;
		StartCoroutine (DataLoader.LoadUsers (this));
	}

	public void OnLoggedIn(){
		loginScreen.SetActive (false);
		mainMenu.SetActive (true);
	}

	public void OnFailedLogin(){
		loginFailed.SetActive (true);
	}

	public void OnSelectedQuizMenu(){
		quizMenu.SetActive (true);
	}

	public void OnSelectedResultsMenu(){
		resultsMenu.SetActive (true);
	}

	#region implemented abstract members of DataReceiver
	public override void ReceiveCategories (Category[] categories)
	{
		Debug.Log ("Callback Successful!");
	}
	public override void ReceiveBattles (Battle[] battles)
	{
		Debug.Log ("Callback Successful!");
	}
	public override void ReceiveUsers (User[] users)
	{
		Debug.Log ("Callback Successful!");
	}
	public override void ReceiveQuestions (Question[] questions)
	{
		Debug.Log ("Callback Successful!");
	}
	public override void ReceiveQuizzes (Quiz[] quizzes)
	{
		Debug.Log ("Callback Successful!");
	}
	#endregion
}