using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DatabaseClassifications;

public class ChangeQuestionPanel : MonoBehaviour {
	public InputField question;
	public InputField correctAnswer;
	public InputField wrongAnswer1;
	public InputField wrongAnswer2;
	public InputField wrongAnswer3;

	private Quiz quiz;

	public static GameObject contentButton;
	public GameObject contentTarget;

	public GameObject feedbackPanel;

	private int quizIndex;
	private static Question selectedQuestion;

	void Awake(){
		if (Application.isMobilePlatform) {
			contentButton = Resources.Load ("Empty Button Mobile") as GameObject;
		} else {
			contentButton = Resources.Load ("Empty Button") as GameObject;
		}
	}

	public void Init (Question q){
		selectedQuestion = q;
		question.text = q.question;
		correctAnswer.text = q.answers[0];
		wrongAnswer1.text = q.answers[1];
		wrongAnswer2.text = q.answers[2];
		wrongAnswer3.text = q.answers[3];

		InstantiateQuizButtons();
	}

	public void OnClick(int quizButton){
		if (quizIndex >= 0) {
			contentTarget.transform.GetChild (quizIndex).GetComponent<Image> ().color = Color.white;
		}
		contentTarget.transform.GetChild (quizButton).GetComponent<Image> ().color = new Color (1f, 0.7f, 0f, 1f);

		quizIndex = quizButton;
		quiz = SetupQuizzes.quizzes [quizButton];
	}

	private void InstantiateQuizButtons(){
		int count = 0;

		foreach (Quiz quiz in SetupQuizzes.quizzes) {

			GameObject go = Instantiate (contentButton, contentTarget.transform);

			Debug.Log(quiz.quizID + " : " + selectedQuestion.quizID);
			if(quiz.quizID == selectedQuestion.quizID){
				Debug.Log("I got this far!");
				go.GetComponent<Animator>().SetTrigger("Highlighted");
			}

			go.GetComponentInChildren<Text> ().text = quiz.quizName;
			EmptyButtonContainer ebc = go.GetComponentInChildren<EmptyButtonContainer> ();
			ebc.myIndex = count;
			ebc.OnClickSendValue += OnClick;
			count++;
		}
	}

	public void UpdateQuestion(){
		if (quiz.quizID != 0) {
			Question newQuestion = new Question (quiz.quizID, selectedQuestion.questionID, question.text, correctAnswer.text, wrongAnswer1.text, wrongAnswer2.text, wrongAnswer3.text);
			DatabaseSaver.instance.SaveQuestion (newQuestion);
			StartCoroutine (EnableFeedback ());
			ClearFieldsOnly ();
		}
	}

	public void Clear(){
		for (int i = 0; i < contentTarget.transform.childCount; i++) {
			if (contentTarget.transform.GetChild (i) != transform) {
				contentTarget.transform.GetChild (i).GetComponentInChildren<EmptyButtonContainer> ().OnClickSendValue -= OnClick;
				Destroy (contentTarget.transform.GetChild (i).gameObject);
			}
		}

		ClearFieldsOnly ();
	}

	public void ClearFieldsOnly (){
		quiz = new Quiz ();

		if (contentTarget.transform.childCount > quizIndex) {
			contentTarget.transform.GetChild (quizIndex).GetComponent<Image> ().color = Color.white;
		}

		question.text = "";
		correctAnswer.text = "";
		wrongAnswer1.text = "";
		wrongAnswer2.text = "";
		wrongAnswer3.text = "";
	}

	private IEnumerator EnableFeedback(){
		feedbackPanel.SetActive (true);
		yield return new WaitForSeconds (2f);
		feedbackPanel.SetActive (false);
	}
}