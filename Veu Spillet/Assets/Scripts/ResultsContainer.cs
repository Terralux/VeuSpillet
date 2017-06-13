using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ResultsContainer : MonoBehaviour {

	public Text question;
	public Text correct;
	public Text userAnswer;

	public void Fill(string question, string correctAnswer, string userAnswer){
		this.question.text = question;
		this.correct.text = correctAnswer;
		this.userAnswer.text = userAnswer;

		if (correctAnswer == userAnswer) {
			GetComponent<Image> ().color = Color.green;
		} else {
			GetComponent<Image> ().color = Color.red;
		}
	}
}