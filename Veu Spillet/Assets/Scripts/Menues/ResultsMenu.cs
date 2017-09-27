using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DatabaseClassifications;

public class ResultsMenu : BaseMenu {

	public static GameObject contentButton;
	public GameObject contentTarget;
	public static GameObject resultsButton;

	public void Awake(){
		if (instance != null) {
			Destroy (this);
		} else {
			instance = this;
		}
		if (Application.isMobilePlatform) {
			contentButton = Resources.Load ("Empty Button Mobile") as GameObject;
			resultsButton = Resources.Load ("Results Post Mobile") as GameObject;
		} else {
			contentButton = Resources.Load ("Empty Button") as GameObject;
			resultsButton = Resources.Load ("Results Post") as GameObject;
		}
		Hide ();
	}

	public static ResultsMenu instance;

	public override void Show ()
	{
		instance.gameObject.SetActive (true);
		InstantiateQuizButtons ();
	}

	public override void Hide ()
	{
		instance.gameObject.SetActive (false);
		Clear ();
	}

	public void OnClick(int buttonIndex){
		SetupQuestions.instance.LoadQuestionWithQuizID (SetupQuizzes.quizzes [buttonIndex].quizID);
		StartCoroutine (WaitForQuestionLoading (buttonIndex));
	}

	private static void InstantiateQuizButtons(){
		instance.ClearContentOnly ();
		int count = 0;
		foreach (Quiz quiz in SetupQuizzes.quizzes) {
			GameObject go = Instantiate (contentButton, instance.contentTarget.transform);
			go.GetComponentInChildren<Text> ().text = quiz.quizName;
			EmptyButtonContainer ebc = go.GetComponentInChildren<EmptyButtonContainer> ();
			ebc.myIndex = count;
			ebc.OnClickSendValue += instance.OnClick;
			count++;
		}
	}

	private static void InstantiateResultsButtons(int quizID){
		instance.ClearContentOnly ();

		foreach (QuizResult result in SetupResults.results) {
			if(result.quizID == quizID) {
								
				for (int i = 0; i < SetupQuestions.questions.Count; i++) {
					for(int j = 0; j < result.questionAnswers.Length; j++) {

						if (SetupQuestions.questions [i].questionID == result.questionIDs[j]) {
							GameObject go = Instantiate (resultsButton, instance.contentTarget.transform);

							go.GetComponent<ResultsContainer> ().Fill (
								SetupQuestions.questions [i].question,
								SetupQuestions.questions [i].answers [0],
								(result.questionAnswers [j] >= 0 ? SetupQuestions.questions [i].answers [result.questionAnswers [j]] : ""),
								SetupQuestions.questions [i].questionID
							);
						}
					}
				}
			}
		}
	}

	private IEnumerator WaitForQuestionLoading(int buttonIndex){
		yield return new WaitUntil (() => SetupQuestions.isReady);
		InstantiateResultsButtons (SetupQuizzes.quizzes [buttonIndex].quizID);
	}

	public void ClearContentOnly (){
		for (int i = 0; i < instance.contentTarget.transform.childCount; i++) {
			if (instance.contentTarget.transform.GetChild (i) != instance.transform) {
				if (instance.contentTarget.transform.GetChild (i).GetComponentInChildren<EmptyButtonContainer> () != null) {
					instance.contentTarget.transform.GetChild (i).GetComponentInChildren<EmptyButtonContainer> ().OnClickSendValue -= instance.OnClick;
				}
				Destroy (instance.contentTarget.transform.GetChild (i).gameObject);
			}
		}
	}

	public static void Clear(){
		instance.ClearContentOnly ();
	}
}