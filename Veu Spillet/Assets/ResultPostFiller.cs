using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ResultPostFiller : MonoBehaviour {

	public Text questionTextField;

	public Image challengerAnswer;
	public Text challengerTextField;

	public Image defenderAnswer;
	public Text defenderTextField;

	public void FillPost(QuizBattleResult qbr, Question q){
		questionTextField.text = q.question;

		int count = 0;
		foreach(int id in qbr.questionIDs){
			if(id == q.questionID){
				if (qbr.questionAnswersChallenger [count] == 0) {
					challengerAnswer.color = Color.green;
				} else {
					challengerAnswer.color = Color.red;
				}

				switch(qbr.questionAnswersChallenger [count]){
				case 0:
					challengerTextField.text = q.correctAnswer;
					break;
				case 1:
					challengerTextField.text = q.wrongAnswer1;
					break;
				case 2:
					challengerTextField.text = q.wrongAnswer2;
					break;
				case 3:
					challengerTextField.text = q.wrongAnswer3;
					break;
				}

				if (qbr.questionAnswersDefender [count] == 0) {
					defenderAnswer.color = Color.green;
				} else {
					defenderAnswer.color = Color.red;
				}

				switch(qbr.questionAnswersDefender [count]){
				case 0:
					defenderTextField.text = q.correctAnswer;
					break;
				case 1:
					defenderTextField.text = q.wrongAnswer1;
					break;
				case 2:
					defenderTextField.text = q.wrongAnswer2;
					break;
				case 3:
					defenderTextField.text = q.wrongAnswer3;
					break;
				}
			}
			count++;
		}
	}
}
