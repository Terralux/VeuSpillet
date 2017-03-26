using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public struct Category{
	public int id;
	public string name;

	public Category(int id, string name){
		this.id = id;
		this.name = name;
	}
}

public struct Battle{
	public int battleID;
	public int challengerID;
	public int defenderID;
	public int quizID;

	public int stat;

	public Battle(int battleID, int challengerID, int defenderID, int quizID, int stat){
		this.battleID = battleID;
		this.challengerID = challengerID;
		this.defenderID = defenderID;
		this.quizID = quizID;
		this.stat = stat;
	}
}

public struct Question{
	public int questionID;
	public string question;
	public string correctAnswer;
	public string wrongAnswer1;
	public string wrongAnswer2;
	public string wrongAnswer3;

	public Question(int questionID, string question, string correctAnswer, string wrongAnswer1, string wrongAnswer2, string wrongAnswer3){
		this.questionID = questionID;
		this.question = question;
		this.correctAnswer = correctAnswer;
		this.wrongAnswer1 = wrongAnswer1;
		this.wrongAnswer2 = wrongAnswer2;
		this.wrongAnswer3 = wrongAnswer3;
	}
}

public struct User{
	public int userID;
	public int teamID;
	public string userName;
	public string userPassword;

	public User(int userID, int teamID, string userName, string userPassword){
		this.userID = userID;
		this.teamID = teamID;
		this.userName = userName;
		this.userPassword = userPassword;
	}
}

public struct Quiz{
	public int quizID;
	public string quizName;
	public int categoryID;
	public int userID;
	public int level;

	public Quiz(int quizID, string quizName){
		this.quizID = quizID;
		this.quizName = quizName;
		categoryID = 0;
		userID = 0;
		level = 0;
	}

	public Quiz(int quizID, string quizName, int categoryID, int userID, int level){
		this.quizID = quizID;
		this.quizName = quizName;
		this.categoryID = categoryID;
		this.userID = userID;
		this.level = level;
	}
}

public struct QuizResult{
	public int[] questionIDs;
	public int[] questionAnswers;
	public int userID;

	public QuizResult(int[] questionIDs, int[] questionAnswers, int userID){
		this.questionIDs = questionIDs;
		this.questionAnswers = questionAnswers;
		this.userID = userID;
	}
}

public struct QuizBattleResult{
	public int[] questionIDs;
	public int[] questionAnswersChallenger;
	public int[] questionAnswersDefender;
	public int challengerID;
	public int defenderID;

	public QuizBattleResult(int[] questionIDs, int[] questionAnswersChallenger, int challengerID, int defenderID){
		this.questionIDs = questionIDs;
		this.questionAnswersChallenger = questionAnswersChallenger;
		this.questionAnswersDefender = new int[30];
		this.challengerID = challengerID;
		this.defenderID = defenderID;
	}

	public QuizBattleResult(int[] questionIDs, int[] questionAnswersChallenger, int[] questionAnswersDefender, int challengerID, int defenderID){
		this.questionIDs = questionIDs;
		this.questionAnswersChallenger = questionAnswersChallenger;
		this.questionAnswersDefender = questionAnswersDefender;
		this.challengerID = challengerID;
		this.defenderID = defenderID;
	}
}