using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BattleResultPostFiller : MonoBehaviour {

	public delegate void VoidEvent(int value);
	public VoidEvent OnClickSendValue;

	public Text question;
	public Text correct;
	public Text challengerAnswer;
	public Text defenderAnswer;

	[HideInInspector]
	public int myIndex;

	public void Fill(string quizName, int totalQuestions, float challengerPercentage, float defenderPercentage){
		this.question.text = quizName;

		this.correct.text = totalQuestions.ToString();
		this.correct.transform.parent.GetComponent<Image>().color = Color.green;

		this.challengerAnswer.text = (challengerPercentage * 100f).ToString() + "%";
		this.defenderAnswer.text = (defenderPercentage * 100f).ToString() + "%";

		if(challengerPercentage < 0.5f){
			this.challengerAnswer.transform.parent.GetComponent<Image>().color = Color.Lerp(Color.red, Color.yellow, challengerPercentage * 2f);
		}else{
			this.challengerAnswer.transform.parent.GetComponent<Image>().color = Color.Lerp(Color.yellow, Color.green, (challengerPercentage * 2) - 1);
		}

		if(defenderPercentage < 0.5f){
			this.defenderAnswer.transform.parent.GetComponent<Image>().color = Color.Lerp(Color.red, Color.yellow, defenderPercentage * 2f);
		}else{
			this.defenderAnswer.transform.parent.GetComponent<Image>().color = Color.Lerp(Color.yellow, Color.green, (defenderPercentage * 2) - 1);
		}
	}

	public void OnClick(){
		if (OnClickSendValue != null) {
			OnClickSendValue (myIndex);
		}
	}
}