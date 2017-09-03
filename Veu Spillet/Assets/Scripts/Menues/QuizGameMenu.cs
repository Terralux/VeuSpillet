using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DatabaseClassifications;

public class QuizGameMenu : BaseMenu {

	public Text question;
	public Text[] answers;

	public static QuizSession currentSession;

	private string[] correctOrderQuestions;

	public Image timerImage;
	private RectTransform timerRectTransform;
	private float timerScaleMax;

	public void Awake(){
		if (instance != null) {
			Destroy (this);
		} else {
			instance = this;
		}
		timerRectTransform = timerImage.gameObject.GetComponent<RectTransform>();
		timerScaleMax = timerRectTransform.sizeDelta.x;
		Hide ();
	}

	public static QuizGameMenu instance;

	public static void Show (QuizSession currentSession){
		QuizGameMenu.currentSession = currentSession;
		SetupQuestions.instance.LoadQuestions (currentSession.quiz.quizID);
		instance.Show ();
	}

	public override void Show ()
	{
		instance.gameObject.SetActive (true);
		timerRectTransform.sizeDelta = new Vector2(timerScaleMax, timerRectTransform.sizeDelta.y);
	}

	public override void Hide ()
	{
		instance.gameObject.SetActive (false);
	}

	public static void InitiateQuiz(Question[] questions){
		currentSession.questions = questions;
		(instance as QuizGameMenu).SetupQuestionUI (currentSession.StartQuiz ());
	}

	public void SetupQuestionUI(string[] answers){
		//timerRectTransform.sizeDelta = new Vector2(timerScaleMax, timerRectTransform.sizeDelta.y);

		correctOrderQuestions = new string[]{ answers [0], answers [1], answers [2], answers [3] };

		for (int i = 0; i < 20; i++) {
			int rand = Random.Range (0, answers.Length);

			string temp = answers [i%4];
			answers [i%4] = answers [rand];
			answers [rand] = temp;
		}

		for (int i = 0; i < answers.Length; i++) {
			(instance as QuizGameMenu).answers [i].text = answers [i];
		}

		(instance as QuizGameMenu).question.text = currentSession.GetCurrentQuestion ();

		StartCoroutine(WaitForTimeOut(0f));
	}

	IEnumerator WaitForTimeOut(float timer){
		yield return new WaitForSeconds(0.02f);
		timer += 0.02f;

		float fraction = (30f - timer) / 30f;

		if(timer ==  30f){
			timerRectTransform.sizeDelta = new Vector2(0f, timerRectTransform.sizeDelta.y);
		}else{
			timerRectTransform.sizeDelta = new Vector2(fraction * timerScaleMax, timerRectTransform.sizeDelta.y);
		}

		timerImage.color = Color.Lerp(new Color(0f,1f,0f,0.5f), new Color(1f,0f,0f,0.5f), 1 - fraction);

		if(timer < 30f){
			StartCoroutine(WaitForTimeOut(timer));
		}else{
			AnsweredQuestion(-1);
		}
	}

	public void AnsweredQuestion(int buttonIndex){
		StopAllCoroutines();

		int convertedAnswerIndex = 0;

		if(buttonIndex >= 0){
			if (answers [buttonIndex].text == correctOrderQuestions [0]) {
				answers [buttonIndex].color = Color.green;
				ClickedEventHandler.TriggerCorrect ();
			} else {
				answers [buttonIndex].color = Color.red;
				ClickedEventHandler.TriggerWrong ();
			}

			for (int i = 0; i < answers.Length; i++) {
				if (answers [i].text == correctOrderQuestions[0]) {
					answers [i].color = Color.green;
				}
				if (answers [buttonIndex].text == correctOrderQuestions [i]) {
					convertedAnswerIndex = i;
				}
			}

			currentSession.StoreAnswer (convertedAnswerIndex);
		}else{
			ClickedEventHandler.TriggerWrong ();
			currentSession.StoreAnswer (-1);
		}

		StartCoroutine (WaitForNextQuestion ());
	}

	IEnumerator WaitForNextQuestion(){
		for (int i = 0; i < answers.Length; i++) {
			answers [i].GetComponent<Button> ().interactable = false;
		}

		yield return new WaitForSeconds (1.5f);

		for (int i = 0; i < answers.Length; i++) {
			answers [i].GetComponent<Button> ().interactable = true;
			answers [i].color = Color.white;
		}

		if (currentSession.hasMoreQuestions) {
			(instance as QuizGameMenu).SetupQuestionUI (currentSession.GetNextQuestion ());
		} else {
			QuizResultsMenu.Show (currentSession);
			Hide ();
			Clear ();
		}
	}

	public void Clear(){
		question.text = "";

		for (int i = 0; i < answers.Length; i++) {
			answers [i].text = "";
		}
	}
}