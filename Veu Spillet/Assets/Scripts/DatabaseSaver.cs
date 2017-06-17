using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DatabaseClassifications;

public class DatabaseSaver : MonoBehaviour {

	public static DatabaseSaver instance; 

	void Awake(){
		if (instance != null) {
			Destroy (this);
		} else {
			instance = this;
		}
	}

	public void SaveSession(QuizSession currentSession){
		StartCoroutine (SaveToDatabase (currentSession));
	}

	public IEnumerator SaveToDatabase(QuizSession currentSession){

		// Check for other quizResults
		// Sort the other quizResults or write new quizResults
		WWWForm myForm = new WWWForm();
		myForm.AddField ("quizID", currentSession.quiz.quizID);
		myForm.AddField ("userID", DataContainer.currentLoggedUser.userID);

		WWW www = new WWW ("http://veu-spillet.dk/Prototype/loadQuizResult.php", myForm);
		yield return www;

		string dataString = www.text;
		string[] quizResultData = dataString.Split('|');

		List<QuizResult> quizResults = new List<QuizResult> ();

		string[] segmentedCategoryData;
		for (int i = 0; i < quizResultData.Length-1; i++) {
			segmentedCategoryData = quizResultData [i].Split (',');

			string[] questionIDs = segmentedCategoryData [3].Split (' ');
			int[] questionIDsInts = new int[questionIDs.Length];

			string[] answers = segmentedCategoryData [1].Split (' ');
			int[] answersInts = new int[questionIDs.Length];

			for (int j = 0; j < questionIDs.Length; j++) {
				questionIDsInts [j] = int.Parse (questionIDs [j]);
				answersInts [j] = int.Parse (answers [j]);
			}

			quizResults.Add (new QuizResult (int.Parse (segmentedCategoryData [0]), answersInts, questionIDsInts, int.Parse (segmentedCategoryData [2])));
		}

		if (quizResults.Count > 0) {
			string collectiveAnswers = "";
			string collectiveQuestionIDs = "";

			/// foreach new question 
			/*
			for (int i = 0; i < quizResults.Count; i++) {
				if(true) {
					collectiveAnswers += currentSession.answers[i] + " ";
				}
			}
			*/
		} else {
			myForm = new WWWForm ();
			myForm.AddField ("quizID", currentSession.quiz.quizID);

			string collectiveAnswers = "";
			string collectiveQuestionIDs = "";

			for (int i = 0; i < currentSession.answers.Count; i++) {
				collectiveAnswers += currentSession.answers [i] + " ";
				collectiveQuestionIDs += currentSession.questions [i].questionID + " ";
			}

			myForm.AddField ("answers", collectiveAnswers);
			myForm.AddField ("userID", DataContainer.currentLoggedUser.userID);
			myForm.AddField ("questionIDs", collectiveQuestionIDs);

			www = new WWW ("http://veu-spillet.dk/Prototype/saveQuizResults.php", myForm);
			yield return www;
			Debug.Log (www.text);
		}
	}
}
