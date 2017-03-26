using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class DataLoader {

	public static IEnumerator LoadCategories(DataReceiver dr){
		Debug.Log ("Began loading data");
		WWW www = new WWW("http://veuspillet.dk/demo/loadCat.php");
		yield return www;

		Debug.Log ("Finished loading data");
		string[] categoryDataStrings = www.text.Split ('|');
		Category[] categories = new Category[categoryDataStrings.Length];

		string[] tempStrings;

		for (int i = 0; i < categoryDataStrings.Length - 1; i++) {
			tempStrings = categoryDataStrings [i].Split (',');
			categories [i] = new Category (int.Parse (tempStrings [0]), tempStrings [1]);

			Debug.Log (categories [i].id + " : " + categories [i].name);
		}
		dr.ReceiveCategories (categories);
	}

	public static IEnumerator LoadBattles(DataReceiver dr){
		WWW www = new WWW("http://veuspillet.dk/getbattle.php");
		yield return www;

		string[] battleDataStrings = www.text.Split ('|');
		Battle[] battles = new Battle[battleDataStrings.Length];

		string[] tempStrings;

		for (int i = 0; i < battleDataStrings.Length - 1; i++) {
			tempStrings = battleDataStrings [i].Split (',');
			battles [i] = new Battle (int.Parse (tempStrings [0]), int.Parse (tempStrings [1]), int.Parse (tempStrings [2]), int.Parse (tempStrings [3]), int.Parse(tempStrings[4]));

			Debug.Log (battles [i].battleID + " : Challenger - " + battles[i].challengerID + " against - " + battles[i].defenderID + " in " + battles[i].quizID);
		}
		dr.ReceiveBattles (battles);
	}

	public static IEnumerator LoadQuizzesFromCategory(DataReceiver dr, int categoryID){
		WWW www = new WWW("http://veuspillet.dk/demo2/loadAllQuizzesFromCategory.php/?cid=" + categoryID);
		yield return www;

		string[] quizDataStrings = www.text.Split ('|');
		Quiz[] quizzes = new Quiz[quizDataStrings.Length];

		string[] tempStrings;

		for (int i = 0; i < quizDataStrings.Length - 1; i++) {
			tempStrings = quizDataStrings [i].Split (',');
			quizzes [i] = new Quiz (int.Parse (tempStrings [0]), tempStrings [1]);//, int.Parse (tempStrings [2]), int.Parse (tempStrings [3]), int.Parse(tempStrings[4]));

			Debug.Log (quizzes [i].quizID + " : Challenger - " + quizzes[i].quizName + " against - " + quizzes[i].categoryID + " in " + quizzes[i].userID + " : " + quizzes[i].level);
		}
		dr.ReceiveQuizzes (quizzes);
	}

	public static IEnumerator LoadQuestionsFromQuiz(DataReceiver dr, int quizID){
		WWW www = new WWW ("http://veuspillet.dk/getAllQuestionsFromQuiz.php/?qid=" + quizID);
		yield return www;

		string[] questionDataStrings = www.text.Split ('|');
		Question[] questions = new Question[questionDataStrings.Length];

		string[] tempStrings;

		for (int i = 0; i < questionDataStrings.Length - 1; i++) {
			tempStrings = questionDataStrings [i].Split (',');
			questions [i] = new Question (int.Parse (tempStrings [0]), tempStrings [1], tempStrings [2], tempStrings [3], tempStrings [4], tempStrings [5]);

			Debug.Log (questions [i].questionID + " : Challenger - " + questions[i].correctAnswer + " against - " + questions[i].wrongAnswer1 + " in " + questions[i].wrongAnswer2 + " : " + questions[i].wrongAnswer3);
		}
		dr.ReceiveQuestions (questions);
	}

	public static IEnumerator LoadUsers(DataReceiver dr){
		WWW www = new WWW ("http://veuspillet.dk/getAllUsers.php");
		yield return www;

		string[] userDataStrings = www.text.Split ('|');
		User[] users = new User[userDataStrings.Length];

		string[] tempStrings;

		for (int i = 0; i < userDataStrings.Length - 1; i++) {
			tempStrings = userDataStrings [i].Split (',');
			users [i] = new User (int.Parse (tempStrings [0]), int.Parse (tempStrings [1]), tempStrings [2], tempStrings[3]);

			Debug.Log (users[i].userID + users[1].teamID + users[i].userName + users[i].userPassword);
		}
		dr.ReceiveUsers (users);
	}
}