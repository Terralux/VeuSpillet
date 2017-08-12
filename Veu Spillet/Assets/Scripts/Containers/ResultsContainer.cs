using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ResultsContainer : MonoBehaviour {

	public Text question;
	public Text correct;
	public Text userAnswer;

	private int id;

	public Sprite flag;
	public Button flagButton;

	public Text flagText;
	private Sprite previousFlagIcon;

	public void Fill(string question, string correctAnswer, string userAnswer, int questionID){
		this.question.text = question;
		this.correct.text = correctAnswer;
		this.userAnswer.text = userAnswer;

		if (correctAnswer == userAnswer) {
			GetComponent<Image> ().color = Color.green;
		} else {
			GetComponent<Image> ().color = Color.red;
		}

		id = questionID;
	}

	public void ReportThisQuestion(){
		if(flagButton.gameObject.GetComponent<Image> ().sprite == flag){
			flagButton.gameObject.GetComponent<Image> ().sprite = previousFlagIcon;
			flagText.enabled = true;
		}else{
			SetupQuestions.instance.ReportQuestion(id);

			flagText.enabled = false;
			previousFlagIcon = flagButton.gameObject.GetComponent<Image> ().sprite;
			flagButton.gameObject.GetComponent<Image> ().sprite = flag;
		}
	}
}