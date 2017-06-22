using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DatabaseClassifications;

public class SetupQuestions : MonoBehaviour {

	public static List<Question> questions = new List<Question>();
	public static SetupQuestions instance;

	public static bool isReady = false;

	void Awake(){
		if (instance != null) {
			Destroy (this);
		} else {
			instance = this;
		}
	}

	public void LoadQuestions(int quizID){
		StartCoroutine (Initiate (quizID));
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
}
