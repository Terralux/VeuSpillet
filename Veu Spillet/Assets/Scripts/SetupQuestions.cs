using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DatabaseClassifications;

public class SetupQuestions : MonoBehaviour {

	public static List<Question> questions = new List<Question>();
	public static SetupQuestions instance;

	void Awake(){
		if (instance != null) {
			Destroy (this);
		} else {
			instance = this;
		}
	}

	public void LoadQuestions(int quizID){
		questions.Clear ();
		StartCoroutine (Initiate (quizID));
	}

	public static IEnumerator Initiate(int quizID){
		string URL = "http://veu-spillet.dk/Prototype/loadAllQuestionsFromQuiz.php/?qid=" + quizID;
		WWW ItemsData = new WWW (URL);
		yield return ItemsData;
		string dataString = ItemsData.text;
		string[] questionData = dataString.Split('|');

		string[] segmentedQuestionsData;
		for (int i = 0; i < questionData.Length-1; i++) {
			segmentedQuestionsData = questionData [i].Split (',');

			Debug.Log (questionData [i]);

			questions.Add (new Question (int.Parse (segmentedQuestionsData [0]), segmentedQuestionsData [2], segmentedQuestionsData [3], segmentedQuestionsData [4], segmentedQuestionsData [5], segmentedQuestionsData [6]));
		}

		Debug.Log ("Got Questions");

		QuizGameMenu.InitiateQuiz (questions.ToArray ());

		yield return new WaitForSeconds (0);
	}
}
