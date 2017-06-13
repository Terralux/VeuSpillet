using DatabaseClassifications;
using System.Collections.Generic;

public class QuizSession {
	public bool isChallengingUser;
	public Category category;
	public Quiz quiz;

	public User defender;

	public Question[] questions;

	private int currentQuestionIndex = 0;
	private int[] lookUpTable = new int[]{ 0, 1, 2, 3 };
	private List<int> answers = new List<int> ();

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

		string[] questionAnswers = questions [currentQuestionIndex].answers;
		lookUpTable = new int[]{ 0, 1, 2, 3 };

		for (int i = 0; i < 10; i++) {
			int rand = UnityEngine.Random.Range (0, 4);

			string temp = questionAnswers [i % 4];
			questionAnswers [i % 4] = questionAnswers [rand];
			questionAnswers [rand] = temp;

			int tempInt = lookUpTable [i % 4];
			lookUpTable [i % 4] = lookUpTable [rand];
			lookUpTable [rand] = tempInt;
		}

		if (currentQuestionIndex == questions.Length - 1) {
			hasMoreQuestions = false;
		}

		return questionAnswers;
	}

	public string GetCurrentQuestion(){
		return questions [currentQuestionIndex].question;
	}

	public void StoreAnswer(int answerIndex){
		answers.Add(lookUpTable [answerIndex]);
	}

	public int GetCorrectAnswer(){
		for (int i = 0; i < lookUpTable.Length; i++) {
			if (lookUpTable [i] == 0) {
				return i;
			}
		}
		return -1;
	}
}