using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ChallengeButton : BaseMenu {

	public static ChallengeButton instance;

	public Text counter;

	void Awake(){
		if(instance){
			Destroy(this);
		}else{
			instance = this;
		}
		Hide();
	}

	public override void Show (){
		HighscoreHandler.instance.Hide();
		gameObject.SetActive(true);
	}

	public override void Hide (){
		gameObject.SetActive(false);
	}

	public static void UpdateCount(int newChallengeCount){
		instance.counter.text = newChallengeCount.ToString();
	}

	public void GoToChallengeMenu (){
		ChallengeMenu.instance.Show();
		instance.Hide();
		BackToMenu.instance.Show();
	}
}