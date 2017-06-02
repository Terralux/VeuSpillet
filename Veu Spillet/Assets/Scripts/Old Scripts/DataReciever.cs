using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DatabaseClassifications;

public abstract class DataReceiver : MonoBehaviour {
	public abstract void ReceiveCategories(Category[] categories);
	public abstract void ReceiveBattles(Battle[] battles);
	public abstract void ReceiveUsers(User[] users);
	public abstract void ReceiveQuestions(Question[] questions);
	public abstract void ReceiveQuizzes(Quiz[] quizzes);
	public abstract void ReceiveBattleResult(QuizBattleResult battleResult, Question[] questions);
}