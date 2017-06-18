using DatabaseClassifications;
using UnityEngine;
using System.Collections.Generic;

public class QuizSession {
	public bool isChallengingUser;
	public Category category;
	public Quiz quiz;

	public User defender;

	public Question[] questions;

	private int currentQuestionIndex = 0;
	public List<int> answers = new List<int> ();

	public bool hasMoreQuestions = true;

	public QuizSession(bool isChallengingUser){
		this.isChallengingUser = isChallengingUser;
	}

	public string[] StartQuiz(){
		currentQuestionIndex = -1;
		answers = new List<int> ();
		hasMoreQuestions = true;
		return GetNextQuestion ();
	}

	public string[] GetNextQuestion(){
		currentQuestionIndex++;

		string[] questionAnswers = new string[questions [currentQuestionIndex].answers.Length];

		for (int i = 0; i < 4; i++) {
			questionAnswers [i] = questions [currentQuestionIndex].answers [i];
		}

		if (currentQuestionIndex == questions.Length - 1) {
			hasMoreQuestions = false;
		}

		return questionAnswers;
	}

	public string GetCurrentQuestion() {
		return questions [currentQuestionIndex].question;
	}

	public void StoreAnswer(int answerIndex) {
		answers.Add (answerIndex);
	}

	public int GetCurrentAnswer(){
		return 0;
	}

	public int GetCorrectAnswer(int questionIndex){
		return answers[questionIndex];
	}

	public int GetAnswerAt(int index){
		if (index < answers.Count) {
			return answers [index];
		} else {
			UnityEngine.Debug.LogWarning ("Tried to get Answer out of range");
			return -1;
		}
	}

	public void SaveToDatabase(){
		DatabaseSaver.instance.SaveSession (this);
	}
}