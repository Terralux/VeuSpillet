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
		if(currentSession.isChallengingUser){
			StartCoroutine (SaveChallengeToDatabase (currentSession));
		}else{
			StartCoroutine (SaveToDatabase (currentSession));
		}
	}

	public IEnumerator SaveChallengeToDatabase(QuizSession currentSession){
		
		WWWForm myForm = new WWWForm();
		myForm.AddField ("quizID", currentSession.quiz.quizID);
		myForm.AddField ("challengerID", currentSession.challenger.userID);
		myForm.AddField ("defenderID", currentSession.defender.userID);

		string questionIDs = "";
		string answers = "";
		int count = 0;

		foreach(Question q in currentSession.questions){
			questionIDs += q.questionID + " ";
			answers += currentSession.answers[count] + " ";
			count++;
		}

		myForm.AddField ("questionIDs", questionIDs);

		if(currentSession.challenger.userID == DataContainer.currentLoggedUser.userID){
			myForm.AddField ("challengerAnswers", answers);
			myForm.AddField ("isDefendersTurn", "1");
		}else{
			myForm.AddField ("defenderAnswers", answers);
			myForm.AddField ("isDefendersTurn", "0");
		}

		WWW www = new WWW ("http://veu-spillet.dk/Prototype/saveBattleData.php", myForm);
		yield return www;

		Debug.Log(www.text);
	}

	public IEnumerator SaveToDatabase(QuizSession currentSession){

		// Check for other quizResults
		// Sort the other quizResults or write new quizResults
		WWWForm myForm = new WWWForm();
		myForm.AddField ("quizID", currentSession.quiz.quizID);
		myForm.AddField ("userID", DataContainer.currentLoggedUser.userID);

		WWW www = new WWW ("http://veu-spillet.dk/Prototype/loadQuizResult.php", myForm);
		yield return www;

		QuizResult quizResults = new QuizResult ();

		if (www.text != "") {
			#region Loading previous Data
			string dataString = www.text;
			string[] quizResultData = dataString.Split ('|');

			string[] segmentedCategoryData;
			for (int i = 0; i < quizResultData.Length - 1; i++) {
				segmentedCategoryData = quizResultData [i].Split (',');

				string[] questionIDs = segmentedCategoryData [3].Split (' ');
				int[] questionIDsInts = new int[questionIDs.Length - 1];

				string[] answers = segmentedCategoryData [1].Split (' ');
				int[] answersInts = new int[questionIDs.Length - 1];

				for (int j = 0; j < questionIDs.Length - 1; j++) {
					questionIDsInts [j] = int.Parse (questionIDs [j]);
					answersInts [j] = int.Parse (answers [j]);
				}

				quizResults = new QuizResult (int.Parse (segmentedCategoryData [0]), answersInts, questionIDsInts, int.Parse (segmentedCategoryData [2]));
			}
			#endregion

			#region Sending new Data
			Debug.Log ("Currently updating");
			for(int j = 0; j < currentSession.questions.Length; j++){

				bool hasFoundCurrentQuestion = false;

				for (int i = 0; i < quizResults.questionIDs.Length; i++) {
					if (quizResults.questionIDs [i] == currentSession.questions [j].questionID) {
						hasFoundCurrentQuestion = true;
						quizResults.questionAnswers [i] = currentSession.answers [j];
					}
				}

				if (!hasFoundCurrentQuestion) {
					quizResults.AddQuestion (currentSession.questions [j].questionID, currentSession.answers [j]);
				}
			}

			myForm = new WWWForm ();
			myForm.AddField ("quizID", currentSession.quiz.quizID);

			string collectiveAnswers = "";
			string collectiveQuestionIDs = "";

			for (int i = 0; i < quizResults.questionAnswers.Length; i++) {
				collectiveAnswers += quizResults.questionAnswers [i] + " ";
				collectiveQuestionIDs += quizResults.questionIDs [i] + " ";
			}

			Debug.Log (collectiveAnswers + " : " + collectiveQuestionIDs);

			myForm.AddField ("answers", collectiveAnswers);
			myForm.AddField ("userID", DataContainer.currentLoggedUser.userID);
			myForm.AddField ("questionIDs", collectiveQuestionIDs);

			www = new WWW ("http://veu-spillet.dk/Prototype/saveQuizResults.php", myForm);
			yield return www;
			Debug.Log (www.text);
			#endregion
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
		}
	}

	public void SaveUser(User user){
		StartCoroutine (SaveUserToDatabase (user));
	}

	public IEnumerator SaveUserToDatabase(User user){
		WWWForm myForm = new WWWForm();
		myForm.AddField ("username", user.userName);
		myForm.AddField ("password", user.userPassword);
		myForm.AddField ("adminRights", user.isAdmin ? 1 : 0);

		WWW www = new WWW ("http://veu-spillet.dk/Prototype/saveUser.php", myForm);
		yield return www;

		Debug.Log (www.text);
	}

	public void SaveQuiz(Quiz quiz){
		StartCoroutine (SaveQuizToDatabase (quiz));
	}

	public IEnumerator SaveQuizToDatabase(Quiz quiz){
		WWWForm myForm = new WWWForm();
		myForm.AddField ("name", quiz.quizName);
		myForm.AddField ("categoryID", quiz.categoryID);

		WWW www = new WWW ("http://veu-spillet.dk/Prototype/saveQuiz.php", myForm);
		yield return www;

		Debug.Log (www.text);
	}

	public void SaveCategory(Category category){
		StartCoroutine (SaveCategoryToDatabase (category));
	}

	public IEnumerator SaveCategoryToDatabase(Category category){
		WWWForm myForm = new WWWForm();
		myForm.AddField ("name", category.name);

		WWW www = new WWW ("http://veu-spillet.dk/Prototype/saveCategory.php", myForm);
		yield return www;

		Debug.Log (www.text);
	}

	public void SaveQuestion(Question question){
		StartCoroutine (SaveQuestionToDatabase (question));
	}

	public IEnumerator SaveQuestionToDatabase(Question question) {
		WWWForm myForm = new WWWForm();
		myForm.AddField ("quizID", question.quizID);
		myForm.AddField ("question", question.question);
		myForm.AddField ("correct", question.answers[0]);
		myForm.AddField ("wrong1", question.answers[1]);
		myForm.AddField ("wrong2", question.answers[2]);
		myForm.AddField ("wrong3", question.answers[3]);

		WWW www = new WWW ("http://veu-spillet.dk/Prototype/saveQuestion.php", myForm);
		yield return www;

		Debug.Log (www.text);
	}
}
