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
	private List<int[]> lookUpTable = new List<int[]> ();
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
		lookUpTable.Add (new int[]{ 0, 1, 2, 3 });

		string[] questionAnswers = new string[questions [currentQuestionIndex].answers.Length];

		for (int i = 0; i < 4; i++) {
			questionAnswers [i] = questions [currentQuestionIndex].answers [i];
		}

		for (int i = 0; i < 10; i++) {
			int rand = UnityEngine.Random.Range (0, 4);

			string temp = questionAnswers [i % 4];
			questionAnswers [i % 4] = questionAnswers [rand];
			questionAnswers [rand] = temp;

			int tempInt = lookUpTable[currentQuestionIndex] [i % 4];
			lookUpTable[currentQuestionIndex] [i % 4] = lookUpTable[currentQuestionIndex] [rand];
			lookUpTable[currentQuestionIndex] [rand] = tempInt;
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
		answers.Add(lookUpTable[currentQuestionIndex] [answerIndex]);
	}

	public int GetCurrentAnswer(){
		for (int i = 0; i < lookUpTable[currentQuestionIndex].Length; i++) {
			if (lookUpTable[currentQuestionIndex] [i] == 0) {
				return i;
			}
		}
		return -1;
	}

	public int GetCorrectAnswer(int questionIndex){
		for (int i = 0; i < lookUpTable[questionIndex].Length; i++) {
			if (lookUpTable[questionIndex] [i] == 0) {
				return i;
			}
		}
		return -1;
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