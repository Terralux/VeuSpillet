using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DatabaseClassifications;

public class ChallengeMenu : BaseMenu {
	
	public static ChallengeMenu instance;

	public static GameObject contentButton;

	public Transform contentTarget;

	private List<Battle> battlesMatchingQuizzes = new List<Battle>();

	void Awake(){
		if(instance){
			Destroy(this);
		}else{
			instance = this;
		}
		contentButton = Resources.Load ("Battle Result Post") as GameObject;
		Hide();
	}

	public override void Show(){
		gameObject.SetActive(true);
		InstantiateBattleButtons();
	}

	public override void Hide(){
		gameObject.SetActive(false);
	}

	public void OnClick(int buttonIndex){
		QuizSession battleSession = new QuizSession(true);
		battleSession.quiz = new Quiz(battlesMatchingQuizzes[buttonIndex].quizID,"",-1);
		battleSession.challenger = new User(battlesMatchingQuizzes[buttonIndex].challengerID,0,"","",false);
		battleSession.defender = new User(battlesMatchingQuizzes[buttonIndex].defenderID,0,"","",false);

		QuizGameMenu.Show(battleSession);
		ChallengeMenu.instance.Hide();
	}

	private static void InstantiateBattleButtons(){
		instance.battlesMatchingQuizzes.Clear();

		List<Battle> notSignificantBattles = new List<Battle>();
		List<Battle> significantBattles = new List<Battle>();

		for(int i = 0; i < ChallengeSetup.battles.Count; i++){
			if((ChallengeSetup.battles[i].isDefendersTurn && ChallengeSetup.battles[i].defenderID == DataContainer.currentLoggedUser.userID) || 
				(!ChallengeSetup.battles[i].isDefendersTurn && ChallengeSetup.battles[i].challengerID == DataContainer.currentLoggedUser.userID)){
				significantBattles.Add(ChallengeSetup.battles[i]);
			}else{
				notSignificantBattles.Add(ChallengeSetup.battles[i]);
			}
		}

		List<Battle> totalBattles = new List<Battle>();

		totalBattles.AddRange(significantBattles);
		totalBattles.AddRange(notSignificantBattles);

		int count = 0;
		foreach (Battle battle in totalBattles) {
			for(int i = 0; i < SetupQuizzes.quizzes.Count; i++){
				if(SetupQuizzes.quizzes[i].quizID == battle.quizID){
					instance.battlesMatchingQuizzes.Add(battle);

					GameObject go = Instantiate (contentButton, instance.contentTarget.transform);
					BattleResultPostFiller brpf = go.GetComponentInChildren<BattleResultPostFiller> ();

					int totalQuestions = battle.questionIDs.Length;
					int correctAnswersDef = 0;
					int correctAnswersCha = 0;

					for(int j = 0; j < battle.questionIDs.Length; j++){
						if(battle.challengerAnswers.Length > j){
							if(battle.challengerAnswers[j] == 0){
								correctAnswersCha++;
							}
						}
						if(battle.defenderAnswers.Length > j){
							if(battle.defenderAnswers[j] == 0){
								correctAnswersDef++;
							}
						}
					}

					string opponentName = SetupUsers.instance.GetUserWithID(battle.challengerID == DataContainer.currentLoggedUser.userID ? battle.defenderID : battle.challengerID).userName;

					brpf.Fill(opponentName,
						SetupQuizzes.quizzes[i].quizName, 
						battle.questionIDs.Length, 
						(float)Math.Round(((float)correctAnswersCha/(float)battle.questionIDs.Length), 2), 
						(float)Math.Round(((float)correctAnswersDef/(float)battle.questionIDs.Length),2));
					brpf.myIndex = count;
					brpf.OnClickSendValue += instance.OnClick;
					count++;
				}
			}
		}
	}

	public void Clear(){
		for (int i = 0; i < instance.contentTarget.transform.childCount; i++) {
			if (instance.contentTarget.transform.GetChild (i) != instance.transform) {
				instance.contentTarget.transform.GetChild (i).GetComponentInChildren<BattleResultPostFiller> ().OnClickSendValue -= instance.OnClick;
				Destroy (instance.contentTarget.transform.GetChild (i).gameObject);
			}
		}
	}
}