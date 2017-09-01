using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DatabaseClassifications;

public class ChallengeSetup : MonoBehaviour {

	public static ChallengeSetup instance;

	public static List<Battle> battles = new List<Battle>();

	void Awake(){
		if(instance){
			Destroy(this);
		}else{
			instance = this;
		}
	}

	public void Load(){
		StartCoroutine(LoadChallenges());
	}

	IEnumerator LoadChallenges(){
		string URL = "http://veu-spillet.dk/Prototype/loadBattleData.php/";

		WWWForm myForm = new WWWForm();
		myForm.AddField ("userID", DataContainer.currentLoggedUser.userID);

		WWW ItemsData = new WWW (URL, myForm);
		yield return ItemsData;

		battles.Clear ();

		string dataString = ItemsData.text;
		string[] userData = dataString.Split('|');

		string[] segmentedUserData;
		for (int i = 0; i < userData.Length-1; i++) {
			segmentedUserData = userData [i].Split (',');

			string[] questionIDStrings = segmentedUserData [4].Split (' ');
			int[] questionIDInts = new int[questionIDStrings.Length - 1];

			string[] chaAnswerStrings = segmentedUserData[5].Split(' ');
			int[] chaAnswerInts = new int[chaAnswerStrings.Length - 1];

			string[] defAnswerStrings = segmentedUserData[6].Split(' ');
			int[] defAnswerInts = new int[questionIDStrings.Length - 1];

			int totalAnswers = segmentedUserData[6].Trim().Length;

			for (int j = 0; j < questionIDStrings.Length - 1; j++) {
				questionIDInts [j] = int.Parse (questionIDStrings [j]);
				chaAnswerInts [j] = int.Parse (chaAnswerStrings [j]);

				if(totalAnswers > 0){
					if(j < defAnswerStrings.Length){
						if(int.TryParse (defAnswerStrings [j], out defAnswerInts[j])){
							defAnswerInts [j] = int.Parse (defAnswerStrings [j]);
						}else{
							defAnswerInts [j] = -1;
						}
					}else{
						defAnswerInts [j] = -1;
					}
				}else{
					defAnswerInts [j] = -1;
				}
			}

			battles.Add (new Battle (int.Parse(segmentedUserData[0]), int.Parse(segmentedUserData[1]),
				int.Parse(segmentedUserData[2]), int.Parse(segmentedUserData[3]), questionIDInts, chaAnswerInts, defAnswerInts, segmentedUserData[7] == "1"));
		}

		int count = 0;
		foreach(Battle b in battles){
			if((b.challengerID == DataContainer.currentLoggedUser.userID && !b.isDefendersTurn) || b.defenderID == DataContainer.currentLoggedUser.userID && b.isDefendersTurn){
				count++;
			}
		}

		ChallengeButton.UpdateCount(count);

		yield return new WaitForSeconds (0);
	}
}