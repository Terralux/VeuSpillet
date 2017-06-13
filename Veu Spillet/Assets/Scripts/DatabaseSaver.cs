using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DatabaseSaver : MonoBehaviour {

	public static IEnumerator SaveToDatabase(QuizSession currentSession){
		Debug.Log ("Is Saving");
		WWWForm myForm = new WWWForm();
		myForm.AddField ("quizID", currentSession.quiz.quizID);

		string collectiveAnswers = "";
		string collectiveQuestionIDs = "";

		for (int i = 0; i < currentSession.answers.Count; i++) {
			collectiveAnswers += currentSession.answers[i] + " ";
			collectiveQuestionIDs += currentSession.questions [i].questionID + " ";
		}
		myForm.AddField ("answers", collectiveAnswers);
		myForm.AddField ("userID", DataContainer.currentLoggedUser.userID);
		myForm.AddField ("questionIDs", collectiveQuestionIDs);

		WWW www = new WWW ("http://veu-spillet.dk/Prototype/saveQuizResults.php", myForm);
		yield return www;
		Debug.Log (www.text);
	}
}
