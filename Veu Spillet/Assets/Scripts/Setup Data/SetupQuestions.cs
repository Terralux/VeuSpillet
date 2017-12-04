using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DatabaseClassifications;

public class SetupQuestions : MonoBehaviour {

	public static List<Question> questions = new List<Question>();
	public static List<FlaggedQuestion> flaggedQuestionIDs = new List<FlaggedQuestion>();
	public static SetupQuestions instance;

	public static List<int> answers = new List<int> ();

	public static bool isReady = false;

	void Awake(){
		if (instance != null) {
			Destroy (this);
		} else {
			instance = this;
		}
	}

	public void LoadQuestions(int quizID){
		if (quizID < 0) {
			StartCoroutine (InitiateRandom (quizID));
		} else {
			StartCoroutine (Initiate (quizID));
		}
	}

	public static IEnumerator InitiateRandom(int quizID){
		string URL = "http://veu-spillet.dk/Prototype/loadAllQuestions.php/";
		WWW ItemsData = new WWW (URL);
		yield return ItemsData;

		questions.Clear ();

		string dataString = ItemsData.text;
		string[] questionData = dataString.Split('|');

		string[] segmentedQuestionsData;
		for (int i = 0; i < questionData.Length-1; i++) {
			segmentedQuestionsData = questionData [i].Split ('¤');

			questions.Add (new Question (int.Parse (segmentedQuestionsData [0]), int.Parse (segmentedQuestionsData [1]), segmentedQuestionsData [2], segmentedQuestionsData [3], segmentedQuestionsData [4], segmentedQuestionsData [5], segmentedQuestionsData [6]));
		}

		List<Question> selectedQuestions = new List<Question> ();

		if (questions.Count < 25) {
			selectedQuestions = questions;
		} else {
			for (int i = 0; i < 20; i++) {
				int random = Random.Range (0, questions.Count);

				while (selectedQuestions.Contains (questions [random])) {
					random = Random.Range (0, questions.Count);
				}

				selectedQuestions.Add (questions [random]);
			}
		}

		QuizGameMenu.InitiateQuiz (selectedQuestions.ToArray ());

		yield return new WaitForSeconds (0);
	}

	public static IEnumerator Initiate(int quizID){
		string URL = "http://veu-spillet.dk/Prototype/loadAllQuestionsFromQuiz.php/?qid=" + quizID;
		WWW ItemsData = new WWW (URL);
		yield return ItemsData;

		questions.Clear ();

		string dataString = ItemsData.text;
		string[] questionData = dataString.Split('|');

		string[] segmentedQuestionsData;
		for (int i = 0; i < questionData.Length-1; i++) {
			segmentedQuestionsData = questionData [i].Split ('¤');

			questions.Add (new Question (int.Parse (segmentedQuestionsData [0]), int.Parse (segmentedQuestionsData [1]), segmentedQuestionsData [2], segmentedQuestionsData [3], segmentedQuestionsData [4], segmentedQuestionsData [5], segmentedQuestionsData [6]));
		}

		QuizGameMenu.InitiateQuiz (questions.ToArray ());

		yield return new WaitForSeconds (0);
	}

	public void LoadAllQuestions(){
		StartCoroutine (LoadQuestions ());
	}

	public static IEnumerator LoadQuestions(){
		string URL = "http://veu-spillet.dk/Prototype/loadAllQuestions.php/";
		WWW ItemsData = new WWW (URL);
		yield return ItemsData;

		questions.Clear ();

		string dataString = ItemsData.text;
		string[] questionData = dataString.Split('|');

		string[] segmentedQuestionsData;
		for (int i = 0; i < questionData.Length-1; i++) {
			segmentedQuestionsData = questionData [i].Split ('¤');

			questions.Add (new Question (int.Parse (segmentedQuestionsData [0]), int.Parse (segmentedQuestionsData [1]), segmentedQuestionsData [2], segmentedQuestionsData [3], segmentedQuestionsData [4], segmentedQuestionsData [5], segmentedQuestionsData [6]));
		}

		yield return new WaitForSeconds (0);
	}

	public void LoadAllQuestionsForMixedQuiz(){
		isReady = false;
		StartCoroutine (LoadAllQuestionsForMixedQuizzes ());
	}

	public static IEnumerator LoadAllQuestionsForMixedQuizzes(){
		string URL = "http://veu-spillet.dk/Prototype/loadAllQuestionsForMixedQuiz.php/?uid=" + DataContainer.currentLoggedUser.userID;
		WWW ItemsData = new WWW (URL);
		yield return ItemsData;

		string dataString = ItemsData.text;
		string[] questionData = dataString.Split('|');

		string[] segmentedQuestionsData;

		answers.Clear ();

		List<int> questionIDs = new List<int> ();

		for (int i = 0; i < questionData.Length-1; i++) {
			segmentedQuestionsData = questionData [i].Split ('¤');

			string[] strings = segmentedQuestionsData [1].Split (' ');
			for(int j = 0; j < strings.Length - 1; j++){
				Debug.Log (strings[j]);
				answers.Add (int.Parse (strings[j]));
			}

			strings = segmentedQuestionsData [3].Split (' ');
			for(int j = 0; j < strings.Length - 1; j++){
				Debug.Log (strings[j]);
				questionIDs.Add (int.Parse (strings[j]));
			}
			//questions.Add (new Question (int.Parse (segmentedQuestionsData [0]), int.Parse (segmentedQuestionsData [1]), segmentedQuestionsData [2], segmentedQuestionsData [3], segmentedQuestionsData [4], segmentedQuestionsData [5], segmentedQuestionsData [6]));
		}

		URL = "http://veu-spillet.dk/Prototype/loadAllQuestions.php/";
		ItemsData = new WWW (URL);
		yield return ItemsData;

		questions.Clear ();

		dataString = ItemsData.text;
		questionData = dataString.Split('|');

		for (int i = 0; i < questionData.Length-1; i++) {
			segmentedQuestionsData = questionData [i].Split ('¤');

			if (questionIDs.Contains (int.Parse (segmentedQuestionsData [0]))) {
				questions.Add (new Question (int.Parse (segmentedQuestionsData [0]), int.Parse (segmentedQuestionsData [1]), segmentedQuestionsData [2], segmentedQuestionsData [3], segmentedQuestionsData [4], segmentedQuestionsData [5], segmentedQuestionsData [6]));
			}
		}

		isReady = true;
		yield return new WaitForSeconds (0);
	}

	public void LoadQuestionWithQuizID(int quizID){
		isReady = false;
		StartCoroutine (LoadQuestionsWithQuizID (quizID));
	}

	public static IEnumerator LoadQuestionsWithQuizID(int quizID){
		string URL = "http://veu-spillet.dk/Prototype/loadAllQuestionsFromQuiz.php/?qid=" + quizID;
		WWW ItemsData = new WWW (URL);
		yield return ItemsData;

		questions.Clear ();

		string dataString = ItemsData.text;
		string[] questionData = dataString.Split('|');

		string[] segmentedQuestionsData;
		for (int i = 0; i < questionData.Length-1; i++) {
			segmentedQuestionsData = questionData [i].Split ('¤');

			questions.Add (new Question (int.Parse (segmentedQuestionsData [0]), int.Parse (segmentedQuestionsData [1]), segmentedQuestionsData [2], segmentedQuestionsData [3], segmentedQuestionsData [4], segmentedQuestionsData [5], segmentedQuestionsData [6]));
		}

		isReady = true;
		yield return new WaitForSeconds (0);
	}

	public void ReportQuestion(int id, string username, string comment){
		StartCoroutine(Report(id, username, comment));
	}

	public static IEnumerator Report(int questionID, string username, string comment){
		string URL = "http://veu-spillet.dk/Prototype/flagQuestion.php/?qid=" + questionID;
		WWWForm form = new WWWForm ();

		form.AddField ("username", username);
		form.AddField ("comment", comment);

		WWW ItemsData = new WWW (URL, form);
		yield return ItemsData;

		Debug.Log (ItemsData.text);
	}

	public void LoadReportedQuestionIDs(){
		StartCoroutine(LoadFlaggedQuestionIDs());
	}

	public static IEnumerator LoadFlaggedQuestionIDs(){
		string URL = "http://veu-spillet.dk/Prototype/loadAllFlaggedQuestions.php/";
		WWW ItemsData = new WWW (URL);
		yield return ItemsData;

		flaggedQuestionIDs.Clear ();

		string dataString = ItemsData.text;
		string[] questionData = dataString.Split('|');


		string[] segmentedQuestionsData;
		for (int i = 0; i < questionData.Length - 1; i++) {
			segmentedQuestionsData = questionData [i].Split ('¤');
			flaggedQuestionIDs.Add (new FlaggedQuestion (int.Parse (segmentedQuestionsData [0]), segmentedQuestionsData [1], segmentedQuestionsData [2]));
		}

		isReady = true;
		yield return new WaitForSeconds (0);
	}
}