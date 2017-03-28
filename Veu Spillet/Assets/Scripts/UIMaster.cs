using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIMaster : DataReceiver {

	public GameObject loginScreen;
	public GameObject loginFailed;
	public GameObject mainMenu;

	public GameObject CategoryButtonPrefab;
	public GameObject UserButtonPrefab;
	public GameObject quizButtonPrefab;
	public GameObject resultPostPanelPrefab;
	public GameObject battleButtonPrefab;

	public Transform SoloQuizPanelCategories;
	public Transform ChallangeQuizPanelCategories;
	public Transform ChallangeQuizPanelUsers;
	public Transform DefendingQuizPanel;

	public GameObject quizMenu;
	public GameObject pickQuizMenu;

	public Transform quizSelector;

	public GameObject quizMain;

	public Text questionField;
	public Text answerField1;
	public Text answerField2;
	public Text answerField3;
	public Text answerField4;

	public Transform resultsContentPanel;
	public GameObject resultsMenu;

	private Question[] questions;
	private List<int> questionsAnswers = new List<int> ();

	private int questionIndex = 0;

	void Awake(){
		Toolbox.FindRequiredComponent<EventSystem> ().OnLoggedIn += OnLoggedIn;
		Toolbox.FindRequiredComponent<EventSystem> ().OnFailedLogin += OnFailedLogin;

		Toolbox.FindRequiredComponent<EventSystem> ().OnSelectedQuizMenu += OnSelectedQuizMenu;
		Toolbox.FindRequiredComponent<EventSystem> ().OnSelectedResultsMenu += OnSelectedResultsMenu;
		Toolbox.FindRequiredComponent<EventSystem> ().OnPickedAQuizFormat += OnPickedAQuizFormat;

		Toolbox.FindRequiredComponent<EventSystem> ().OnBeganQuiz += OnBeganQuiz;

		//int[] tempIDs = new int[30];
		//tempIDs [7] = 3;
		//StartCoroutine (DataSaver.SaveQuizBattleResult (new QuizBattleResult (337, tempIDs, tempIDs, tempIDs, 1, 2)));
		//DataLoader.LoadBattleResult (this, 337);
		//StartCoroutine(DataSaver.SaveBattle(new Battle(1,2,3,4,5)));
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
		StartCoroutine(DataLoader.LoadBattles (this));
	}

	public void OnSelectedResultsMenu(){
		mainMenu.SetActive (false);
		quizMain.SetActive (false);
		resultsMenu.SetActive (true);
		StartCoroutine (DataLoader.LoadBattleResult (this, DataContainer.currentBattleID));
	}

	public void OnPickedAQuizFormat(Category c, User u){
		DataContainer.selectedCategory = c;
		DataContainer.opponentUser = u;
		StartCoroutine(DataLoader.LoadQuizzesFromCategory (this, c.id));
	}

	public void OnBeganQuiz(){
		quizMenu.SetActive (false);
		StartCoroutine(DataLoader.LoadQuestionsFromQuiz (this, DataContainer.selectedQuiz.quizID));
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
		FillWithQuizBattleButtons (battles);
	}
	public override void ReceiveUsers (User[] users)
	{
		mainMenu.SetActive (false);
		quizMenu.SetActive (true);
		fillButtonDataUsers (users);
	}
	public override void ReceiveQuestions (Question[] questions)
	{
		if (DataContainer.currentBattleID != 0) {
			StartCoroutine (DataSaver.SaveBattle (new Battle (1, DataContainer.currentLoggedUser.userID, DataContainer.opponentUser.userID, DataContainer.selectedQuiz.quizID, 0)));
		} else {
			if (DataContainer.currentBattle.challengerID == DataContainer.currentLoggedUser.userID) {
				DataContainer.currentBattleID = DataContainer.currentBattle.battleID;
			}
		}

		this.questions = questions;

		pickQuizMenu.SetActive (false);
		quizMain.SetActive (true);

		SetupQuestions ();
	}
	public override void ReceiveQuizzes (Quiz[] quizzes)
	{
		quizMenu.SetActive (false);
		pickQuizMenu.SetActive (true);

		foreach (Quiz q in quizzes) {
			GameObject newQuizButton = Instantiate (quizButtonPrefab) as GameObject;
			newQuizButton.transform.SetParent (quizSelector);
			newQuizButton.GetComponentInChildren<Text> ().text = q.quizName;
			newQuizButton.GetComponent<QuizContainer> ().myQuiz = q;
		}
	}
	public override void ReceiveBattleResult(QuizBattleResult battleResult, Question[] questions){
		foreach (Question q in questions) {
			GameObject newResultsPost = Instantiate (resultPostPanelPrefab) as GameObject;
			newResultsPost.transform.SetParent (resultsContentPanel);
			newResultsPost.GetComponent<ResultPostFiller> ().FillPost (battleResult, q);
		}
	}
	#endregion

	void SetupQuestions(){
		if (questionIndex < questions.GetLength (0) && questionIndex < 30) {
			questionField.text = questions [questionIndex].question;
			answerField1.text = questions [questionIndex].correctAnswer;

			answerField1.transform.parent.GetComponent<Button> ().onClick.RemoveAllListeners ();
			answerField1.transform.parent.GetComponent<Button> ().onClick.AddListener (() => {
				QuestionAnswer (0);
			});

			answerField2.text = questions [questionIndex].wrongAnswer1;

			answerField2.transform.parent.GetComponent<Button> ().onClick.RemoveAllListeners ();
			answerField2.transform.parent.GetComponent<Button> ().onClick.AddListener (() => {
				QuestionAnswer (1);
			});

			answerField3.text = questions [questionIndex].wrongAnswer2;

			answerField3.transform.parent.GetComponent<Button> ().onClick.RemoveAllListeners ();
			answerField3.transform.parent.GetComponent<Button> ().onClick.AddListener (() => {
				QuestionAnswer (2);
			});

			answerField4.text = questions [questionIndex].wrongAnswer3;

			answerField4.transform.parent.GetComponent<Button> ().onClick.RemoveAllListeners ();
			answerField4.transform.parent.GetComponent<Button> ().onClick.AddListener (() => {
				QuestionAnswer (3);
			});

			questionIndex++;
		} else {

			int[] questionIDs = new int[questions.Length];
			for (int i = 0; i < questions.Length; i++) {
				questionIDs[i] = questions [i].questionID;
			}

			if (DataContainer.currentBattleID != 0) {
				if (DataContainer.currentBattle.challengerID == DataContainer.currentLoggedUser.userID) {
					StartCoroutine (DataSaver.SaveQuizBattleResponse (questionsAnswers.ToArray (), true));
				} else {
					StartCoroutine (DataSaver.SaveQuizBattleResult (new QuizBattleResult (DataContainer.currentBattleID, 
						questionIDs, questionsAnswers.ToArray (), DataContainer.currentLoggedUser.userID, DataContainer.opponentUser.userID)));
				}
			} else {
				StartCoroutine (DataSaver.SaveQuizBattleResponse (questionsAnswers.ToArray (), false));
			}
			//StartCoroutine (DataSaver.SaveBattle (new Battle (10, DataContainer.currentLoggedUser.userID, DataContainer.opponentUser.userID, DataContainer.selectedQuiz.quizID, 0)));

			Toolbox.FindRequiredComponent<EventSystem> ().OnSelectedResultsMenu ();
		}
	}

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

	void FillWithQuizBattleButtons(Battle[] battles){
		foreach (Battle b in battles) {
			if (b.battleID > 0) {
				GameObject newQuizButton = Instantiate (battleButtonPrefab) as GameObject;
				newQuizButton.transform.SetParent (DefendingQuizPanel);
				newQuizButton.GetComponentInChildren<Text> ().text = b.battleID.ToString ();
				newQuizButton.GetComponent<BattleContainer> ().myBattle = b;
			}
		}
	}

	public void QuestionAnswer(int index){
		questionsAnswers.Add (index);
		SetupQuestions ();
	}
}