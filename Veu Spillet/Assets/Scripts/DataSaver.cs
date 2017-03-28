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
		DataContainer.currentBattleID = int.Parse (www.text);
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
			if (i == quizBattleResultToSave.questionIDs.Length - 1) {
				questionIDs += quizBattleResultToSave.questionIDs [i];

				challengerAnswers += quizBattleResultToSave.questionAnswersChallenger [i];
				defenderAnswers += quizBattleResultToSave.questionAnswersDefender [i];
			} else {
				questionIDs += quizBattleResultToSave.questionIDs [i] + ",";

				challengerAnswers += quizBattleResultToSave.questionAnswersChallenger [i] + ",";
				defenderAnswers += quizBattleResultToSave.questionAnswersDefender [i] + ",";
			}
		}

		Debug.Log (challengerAnswers);

		questionIDs += "|";
		challengerAnswers += "|";
		defenderAnswers += "|";

		WWWForm myForm = new WWWForm();
		myForm.AddField ("questionIDs", questionIDs);
		myForm.AddField ("challengerAnswers", challengerAnswers);
		myForm.AddField ("defenderAnswers", defenderAnswers);
		myForm.AddField ("challengerID", quizBattleResultToSave.challengerID);
		myForm.AddField ("defenderID", quizBattleResultToSave.defenderID);
		myForm.AddField ("battleID", quizBattleResultToSave.battleID);

		WWW www = new WWW ("http://veuspillet.dk/saveQuizBattleResultData.php", myForm);
		yield return www;

		Debug.Log (www.text);
	}

	public static IEnumerator SaveQuizBattleResponse(int[] questionAnswers, bool isChallenger){
		string answers = "";

		for (int i = 0; i < questionAnswers.Length; i++) {
			if (i == questionAnswers.Length - 1) {
				answers += questionAnswers [i];
			} else {
				answers += questionAnswers [i] + ",";
			}
		}

		answers += "|";

		WWWForm myForm = new WWWForm();

		myForm.AddField ("isChallenger", isChallenger ? 1 : 0);

		if (isChallenger) {
			myForm.AddField ("challengerAnswers", answers);
		} else {
			myForm.AddField ("defenderAnswers", answers);
		}
		WWW www = new WWW ("http://veuspillet.dk/saveQuizBattleResponseData.php", myForm);
		yield return www;

		Debug.Log (www.text);
	}

}