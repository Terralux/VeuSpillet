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
	public string correctAnswer;
	public string wrongAnswer1;
	public string wrongAnswer2;
	public string wrongAnswer3;

	public Question(int questionID, string correctAnswer, string wrongAnswer1, string wrongAnswer2, string wrongAnswer3){
		this.questionID = questionID;
		this.correctAnswer = correctAnswer;
		this.wrongAnswer1 = wrongAnswer1;
		this.wrongAnswer2 = wrongAnswer2;
		this.wrongAnswer3 = wrongAnswer3;
	}
}

public struct User{
	public int userID;
	public string userName;

	public User(int userID, string userName){
		this.userID = userID;
		this.userName = userName;
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