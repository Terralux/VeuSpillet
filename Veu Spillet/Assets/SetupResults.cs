using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DatabaseClassifications;

public class SetupResults : MonoBehaviour {

	public static SetupResults instance;

	public static List<QuizResult> results = new List<QuizResult>();

	void Awake(){
		if (instance != null) {
			Destroy (this);
		} else {
			instance = this;
		}
	}

	public void Reload(){
		StartCoroutine (Initiate ());
	}

	public static IEnumerator Initiate(){
		string URL = "http://veu-spillet.dk/Prototype/loadAllQuizResults.php/";

		WWWForm myForm = new WWWForm();
		myForm.AddField ("userID", DataContainer.currentLoggedUser.userID);

		WWW ItemsData = new WWW (URL, myForm);
		yield return ItemsData;

		Debug.Log (ItemsData.text);

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

		Debug.Log ("Done!");

		yield return new WaitForSeconds (0);
	}
}
