using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIMaster : DataReceiver {

	private MenuStates myMenuState = MenuStates.LOGIN;

	public GameObject loginScreen;
	public GameObject loginFailed;
	public GameObject mainMenu;

	public GameObject CategoryButtonPrefab;
	public GameObject UserButtonPrefab;
	public Transform SoloQuizPanelCategories;
	public Transform ChallangeQuizPanelCategories;
	public Transform ChallangeQuizPanelUsers;

	public GameObject quizMenu;
	public GameObject resultsMenu;

	void Awake(){
		Toolbox.FindRequiredComponent<EventSystem> ().OnLoggedIn += OnLoggedIn;
		Toolbox.FindRequiredComponent<EventSystem> ().OnFailedLogin += OnFailedLogin;

		Toolbox.FindRequiredComponent<EventSystem> ().OnSelectedQuizMenu += OnSelectedQuizMenu;
		Toolbox.FindRequiredComponent<EventSystem> ().OnSelectedResultsMenu += OnSelectedResultsMenu;
		StartCoroutine (DataLoader.LoadUsers (this));
		StartCoroutine (DataLoader.LoadCategories (this));
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
		fillButtonDataCategories (categories);
	}
	public override void ReceiveBattles (Battle[] battles)
	{
		Debug.Log ("Callback Successful!");
	}
	public override void ReceiveUsers (User[] users)
	{
		Debug.Log ("Callback Successful!");
		fillButtonDataUsers (users);

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

	void fillButtonDataCategories(Category[] categories){
	 
		foreach (Category c in categories) {
			GameObject newCategoryButton = Instantiate (CategoryButtonPrefab) as GameObject;
			newCategoryButton.transform.SetParent (SoloQuizPanelCategories);
			newCategoryButton.GetComponentInChildren<Text> ().text = c.name;
			newCategoryButton.GetComponent<CategoryContainer> ().myCategory = c;
		} 

		foreach (Category c in categories) {
			GameObject newCategoryButton = Instantiate (CategoryButtonPrefab) as GameObject;
			newCategoryButton.transform.SetParent (ChallangeQuizPanelCategories);
			newCategoryButton.GetComponentInChildren<Text> ().text = c.name;
			newCategoryButton.GetComponent<CategoryContainer> ().myCategory = c;
		} 

	}

	void fillButtonDataUsers(User[] users){

		foreach (User u in users) {
			GameObject newUserButton = Instantiate (UserButtonPrefab) as GameObject;
			newUserButton.transform.SetParent (ChallangeQuizPanelUsers);
			newUserButton.GetComponentInChildren<Text> ().text = u.userName;
			newUserButton.GetComponent<UserContainer> ().myUser = u;
		}

	}
}