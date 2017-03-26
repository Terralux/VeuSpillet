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
	public GameObject pickQuizMenu;

	public Transform quizSelector;

	public GameObject quizMain;

	public Text questionField;
	public Text answerField1;
	public Text answerField2;
	public Text answerField3;
	public Text answerField4;

	public GameObject resultsMenu;


	private int questionIndex = 0;

	void Awake(){
		Toolbox.FindRequiredComponent<EventSystem> ().OnLoggedIn += OnLoggedIn;
		Toolbox.FindRequiredComponent<EventSystem> ().OnFailedLogin += OnFailedLogin;

		Toolbox.FindRequiredComponent<EventSystem> ().OnSelectedQuizMenu += OnSelectedQuizMenu;
		Toolbox.FindRequiredComponent<EventSystem> ().OnSelectedResultsMenu += OnSelectedResultsMenu;
		Toolbox.FindRequiredComponent<EventSystem> ().OnPickedAQuizFormat += OnPickedAQuizFormat;

		Toolbox.FindRequiredComponent<EventSystem> ().OnBeganQuiz += OnBeganQuiz;
	}

	public void OnLoggedIn(){
		loginScreen.SetActive (false);
		mainMenu.SetActive (true);
	}

	public void OnFailedLogin(){
		loginFailed.SetActive (true);
	}

	public void OnSelectedQuizMenu(){
		StartCoroutine(DataLoader.LoadUsers (this));
		StartCoroutine(DataLoader.LoadCategories (this));
	}

	public void OnSelectedResultsMenu(){
		mainMenu.SetActive (false);
		resultsMenu.SetActive (true);
	}

	public void OnPickedAQuizFormat(Category c, User u){
		DataContainer.c = c;
		DataContainer.u = u;
		StartCoroutine(DataLoader.LoadQuizzesFromCategory (this, c.id));
	}

	public void OnBeganQuiz(){
		StartCoroutine(DataLoader.LoadQuestionsFromQuiz (this, DataContainer.q.quizID));
	}

	#region implemented abstract members of DataReceiver
	public override void ReceiveCategories (Category[] categories)
	{
		mainMenu.SetActive (false);
		quizMenu.SetActive (true);
		fillButtonDataCategories (categories);
	}
	public override void ReceiveBattles (Battle[] battles)
	{
		Debug.Log ("Callback Successful!");
	}
	public override void ReceiveUsers (User[] users)
	{
		mainMenu.SetActive (false);
		quizMenu.SetActive (true);
		fillButtonDataUsers (users);
	}
	public override void ReceiveQuestions (Question[] questions)
	{
		pickQuizMenu.SetActive (false);
		quizMain.SetActive (true);

		questionField.text = questions [questionIndex].question;
		answerField1.text = questions [questionIndex].correctAnswer;
		answerField1.transform.parent.GetComponent<Button>().onClick.AddListener (() => {
			QuestionAnswer (0);
		});
		answerField2.text = questions [questionIndex].wrongAnswer1;
		answerField2.transform.parent.GetComponent<Button>().onClick.AddListener (() => {
			QuestionAnswer (1);
		});
		answerField3.text = questions [questionIndex].wrongAnswer2;
		answerField3.transform.parent.GetComponent<Button>().onClick.AddListener (() => {
			QuestionAnswer (2);
		});
		answerField4.text = questions [questionIndex].wrongAnswer3;
		answerField4.transform.parent.GetComponent<Button>().onClick.AddListener (() => {
			QuestionAnswer (3);
		});

		questionIndex++;
	}
	public override void ReceiveQuizzes (Quiz[] quizzes)
	{
		quizMenu.SetActive (false);
		pickQuizMenu.SetActive (true);

		foreach (Quiz q in quizzes) {
			GameObject newQuizButton = Instantiate (CategoryButtonPrefab) as GameObject;
			newQuizButton.transform.SetParent (SoloQuizPanelCategories);
			newQuizButton.GetComponentInChildren<Text> ().text = q.quizName;
			newQuizButton.AddComponent<QuizContainer> ().myQuiz = q;
		}
		Toolbox.FindRequiredComponent<EventSystem> ().OnBeganQuiz ();
	}
	#endregion

	void fillButtonDataCategories(Category[] categories){

		foreach (Category c in categories) {
			GameObject newCategoryButton = Instantiate (CategoryButtonPrefab) as GameObject;
			newCategoryButton.transform.SetParent (SoloQuizPanelCategories);
			newCategoryButton.GetComponentInChildren<Text> ().text = c.name;
			newCategoryButton.GetComponent<CategoryContainer> ().myCategory = c;

			GameObject newCategoryButton2 = Instantiate (CategoryButtonPrefab) as GameObject;
			newCategoryButton2.transform.SetParent (ChallangeQuizPanelCategories);
			newCategoryButton2.GetComponentInChildren<Text> ().text = c.name;
			newCategoryButton2.GetComponent<CategoryContainer> ().myCategory = c;
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

	public void QuestionAnswer(int index){
		Debug.Log (index);
	}
}