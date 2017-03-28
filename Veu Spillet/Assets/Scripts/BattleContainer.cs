using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BattleContainer : MonoBehaviour {

	public Battle myBattle;

	public void OnClick(){
		if (myBattle.challengerID == DataContainer.currentLoggedUser.userID) {
			DataContainer.currentBattle = myBattle;
			DataContainer.selectedQuiz.quizID = myBattle.quizID;
			Toolbox.FindRequiredComponent<EventSystem> ().OnBeganQuiz ();
		} else {
			DataContainer.currentBattle = myBattle;
			DataContainer.selectedQuiz.quizID = myBattle.quizID;
			Toolbox.FindRequiredComponent<EventSystem> ().OnBeganQuiz ();
		}
	}
}
