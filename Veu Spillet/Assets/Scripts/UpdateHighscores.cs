using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DatabaseClassifications;

public class UpdateHighscores : MonoBehaviour {
	List<int> userIDs = new List<int>();
	List<int> correctAnswers = new List<int>();

	List<QuizResult> results = new List<QuizResult>();

	void Awake () {
		StartCoroutine(WaitForQuizResults());
	}

	private IEnumerator WaitForQuizResults(){
		string URL = "http://veu-spillet.dk/Prototype/loadAllQuizResults.php/";
		
		WWW ItemsData = new WWW (URL);
		yield return ItemsData;

		Debug.Log(ItemsData.text);

		results.Clear ();

		string dataString = ItemsData.text;
		string[] userData = dataString.Split('|');

		string[] segmentedUserData;
		for (int i = 0; i < userData.Length-1; i++) {
			segmentedUserData = userData [i].Split (',');

			string[] answerStrings = segmentedUserData[1].Split(' ');
			int[] answerInts = new int[answerStrings.Length];

			string[] questionIDStrings = segmentedUserData [3].Split (' ');
			int[] questionIDInts = new int[questionIDStrings.Length];

			for (int j = 0; j < answerStrings.Length - 1; j++) {
				answerInts [j] = int.Parse (answerStrings [j]);
				questionIDInts [j] = int.Parse (questionIDStrings [j]);
			}

			results.Add (new QuizResult (int.Parse (segmentedUserData [0]), questionIDInts, answerInts, int.Parse (segmentedUserData [2])));
		}

		yield return new WaitForSeconds (0);
		OrganizeHighscores();
	}
	
	void OrganizeHighscores () {
		foreach(QuizResult result in results){
			if(userIDs.Contains(result.userID)){
				foreach(int answer in result.questionAnswers){
					correctAnswers[userIDs.IndexOf(result.userID)] += answer == 0?1:0;
				}
			}else{
				userIDs.Add(result.userID);
				correctAnswers.Add(0);
			}
		}

		int i, j;
		int N = userIDs.Count;

		for (j = N - 1; j > 0; j--) {
			for (i = 0; i < j; i++) {
				if (correctAnswers [i] < correctAnswers [i + 1])
					Exchange (i, i + 1);
			}
		}

		StartCoroutine(WaitForHighscoreUpdate());
	}

	public IEnumerator WaitForHighscoreUpdate(){
		string URL = "http://veu-spillet.dk/Prototype/updateHighscore.php/";

		WWWForm form = new WWWForm();

		for(int i = 0; i < (userIDs.Count > 6 ? 6 : userIDs.Count); i++){
			for(int j = 0; j < SetupUsers.users.Count; j++){
				if(SetupUsers.users[j].userID == userIDs[i]){
					form.AddField("name" + (i + 1).ToString(), SetupUsers.users[j].userName);
					form.AddField("correctAnswers" + (i + 1).ToString(), correctAnswers[i]);
				}
			}
		}

		WWW ItemsData = new WWW (URL, form);
		yield return ItemsData;
		Debug.Log(ItemsData.text);
		yield return new WaitForSeconds(180f);
		WaitForQuizResults();
	}

	public void Exchange (int m, int n)
	{
		int temporary;

		temporary = correctAnswers [m];
		correctAnswers [m] = correctAnswers [n];
		correctAnswers [n] = temporary;

		temporary = 0;

		temporary = userIDs [m];
		userIDs [m] = userIDs [n];
		userIDs [n] = temporary;
	}
}