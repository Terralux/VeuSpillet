using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DatabaseClassifications;

public class SetupQuizzes : MonoBehaviour {
	
	public static List<Quiz> quizzes = new List<Quiz>();
	public static SetupQuizzes instance;

	void Awake(){
		if (instance != null) {
			Destroy (this);
		} else {
			instance = this;
		}
	}

	public void LoadQuizzes(int categoryID){
		StartCoroutine (Initiate (categoryID));
	}

	public void LoadAllQuizzes(){
		StartCoroutine (LoadAll ());
	}

	public static IEnumerator Initiate(int categoryID){
		string URL = "http://veu-spillet.dk/Prototype/loadAllQuizzesFromCategory.php/?cid=" + categoryID;
		WWW ItemsData = new WWW (URL);
		yield return ItemsData;

		quizzes.Clear ();

		string dataString = ItemsData.text;
		string[] quizData = dataString.Split('|');

		string[] segmentedQuizData;
		for (int i = 0; i < quizData.Length-1; i++) {
			segmentedQuizData = quizData [i].Split (',');

			quizzes.Add (new Quiz (int.Parse (segmentedQuizData [0]), segmentedQuizData [1], categoryID));
		}

		Debug.Log ("Got quizzes");

		QuizMenu.InstantiateQuizButtons ();

		yield return new WaitForSeconds (0);
	}

	public static IEnumerator LoadAll(){
		string URL = "http://veu-spillet.dk/Prototype/loadAllQuizzes.php/";
		WWW ItemsData = new WWW (URL);
		yield return ItemsData;

		quizzes.Clear ();

		string dataString = ItemsData.text;
		string[] quizData = dataString.Split('|');

		string[] segmentedQuizData;
		for (int i = 0; i < quizData.Length-1; i++) {
			segmentedQuizData = quizData [i].Split (',');

			quizzes.Add (new Quiz (int.Parse (segmentedQuizData [0]), segmentedQuizData [1], int.Parse(segmentedQuizData [2])));
		}

		yield return new WaitForSeconds (0);
	}
}