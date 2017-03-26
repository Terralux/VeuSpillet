using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class DataSaver {

	public static IEnumerator SaveBattle(Battle battleToSave){
		WWWForm myForm = new WWWForm();
		myForm.AddField ("battleID", battleToSave.battleID);
		myForm.AddField ("challengerID", battleToSave.challengerID);
		myForm.AddField ("defenderID", battleToSave.defenderID);
		myForm.AddField ("quizID", battleToSave.quizID);
		myForm.AddField ("stat", battleToSave.stat);

		WWW www = new WWW ("http://veuspillet.dk/saveBattleData.php", myForm);
		yield return www;

		Debug.Log (www.text);
	}

	public static IEnumerator SaveQuizResults(QuizResult quizResultsToSave){
		string questionIDs = "";
		string answers = "";

		for (int i = 0; i < quizResultsToSave.questionIDs.Length; i++) {
			questionIDs += quizResultsToSave.questionIDs[i] + ",";

			if (quizResultsToSave.questionAnswers [i] != 0) {
				answers += quizResultsToSave.questionAnswers [i] + ",";
			} else {
				answers += ",";
			}
		}

		questionIDs += "|";
		answers += "|";

		WWWForm myForm = new WWWForm();
		myForm.AddField ("questionIDs", questionIDs);
		myForm.AddField ("answers", answers);
		myForm.AddField ("userID", quizResultsToSave.userID);

		WWW www = new WWW ("http://veuspillet.dk/saveQuizResultData.php", myForm);
		yield return www;

		Debug.Log (www.text);
	}

	public static IEnumerator SaveQuizBattleResult(QuizBattleResult quizBattleResultToSave){

		string questionIDs = "";
		string challengerAnswers = "";
		string defenderAnswers = "";

		for (int i = 0; i < quizBattleResultToSave.questionIDs.Length; i++) {
			questionIDs += quizBattleResultToSave.questionIDs[i] + ",";

			if (quizBattleResultToSave.questionAnswersChallenger [i] != 0) {
				challengerAnswers += quizBattleResultToSave.questionAnswersChallenger [i] + ",";
			} else {
				challengerAnswers += ",";
			}

			if (quizBattleResultToSave.questionAnswersDefender [i] != 0) {
				defenderAnswers += quizBattleResultToSave.questionAnswersDefender [i] + ",";
			} else {
				defenderAnswers += ",";
			}
		}

		questionIDs += "|";
		challengerAnswers += "|";
		defenderAnswers += "|";

		WWWForm myForm = new WWWForm();
		myForm.AddField ("questionIDs", questionIDs);
		myForm.AddField ("challengerAnswers", challengerAnswers);
		myForm.AddField ("defenderAnswers", defenderAnswers);
		myForm.AddField ("challengerID", quizBattleResultToSave.challengerID);
		myForm.AddField ("defenderID", quizBattleResultToSave.defenderID);

		WWW www = new WWW ("http://veuspillet.dk/saveQuizBattleResultData.php", myForm);
		yield return www;

		Debug.Log (www.text);
	}
}