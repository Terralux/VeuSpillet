using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DatabaseClassifications;

public class BattleContainer : MonoBehaviour {

	public Battle myBattle;

	public void OnClick(){
		if (myBattle.challengerID == DataContainer.currentLoggedUser.userID) {
			DataContainer.currentBattleID = myBattle.battleID;
			DataContainer.selectedQuiz.quizID = myBattle.quizID;
			Toolbox.FindRequiredComponent<EventSystem> ().OnBeganQuiz ();
		} else {
			DataContainer.currentBattleID = myBattle.battleID;
			DataContainer.selectedQuiz.quizID = myBattle.quizID;
			Toolbox.FindRequiredComponent<EventSystem> ().OnBeganQuiz ();
		}
	}
}
