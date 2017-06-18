using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace DatabaseClassifications{

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
		public string[] answers;

		public Question(int questionID, string question, string correctAnswer, string wrongAnswer1, string wrongAnswer2, string wrongAnswer3){
			this.questionID = questionID;
			this.question = question;
			answers = new string[4];
			answers [0] = correctAnswer;
			answers [1] = wrongAnswer1;
			answers [2] = wrongAnswer2;
			answers [3] = wrongAnswer3;
		}
	}

	public struct User{
		public int userID;
		public int teamID;
		public string userName;
		public string userPassword;
		public bool isAdmin;

		public User(int userID, int teamID, string userName, string userPassword, bool isAdmin){
			this.userID = userID;
			this.teamID = teamID;
			this.userName = userName;
			this.userPassword = userPassword;
			this.isAdmin = isAdmin;
		}
	}

	public struct Quiz{
		public int quizID;
		public string quizName;
		public int categoryID;
		public int userID;
		public int level;

		public Quiz(int quizID, string quizName, int categoryID){
			this.quizID = quizID;
			this.quizName = quizName;
			this.categoryID = categoryID;
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
		public int quizID;
		public int[] questionIDs;
		public int[] questionAnswers;
		public int userID;

		public QuizResult(int quizID, int[] questionIDs, int[] questionAnswers, int userID){
			this.quizID = quizID;
			this.questionIDs = questionIDs;
			this.questionAnswers = questionAnswers;
			this.userID = userID;
		}

		public void AddQuestion(int questionID, int questionAnswer){
			
		}
	}

	public struct QuizBattleResult{
		public int battleID;
		public int[] questionIDs;
		public int[] questionAnswersChallenger;
		public int[] questionAnswersDefender;
		public int challengerID;
		public int defenderID;

		public QuizBattleResult(int battleID, int[] questionIDs, int[] questionAnswersChallenger, int challengerID, int defenderID){
			this.battleID = battleID;
			this.questionIDs = questionIDs;
			this.questionAnswersChallenger = questionAnswersChallenger;
			this.questionAnswersDefender = new int[30];
			this.challengerID = challengerID;
			this.defenderID = defenderID;
		}

		public QuizBattleResult(int battleID, int[] questionIDs, int[] questionAnswersChallenger, int[] questionAnswersDefender, int challengerID, int defenderID){
			this.battleID = battleID;
			this.questionIDs = questionIDs;
			this.questionAnswersChallenger = questionAnswersChallenger;
			this.questionAnswersDefender = questionAnswersDefender;
			this.challengerID = challengerID;
			this.defenderID = defenderID;
		}
	}
}