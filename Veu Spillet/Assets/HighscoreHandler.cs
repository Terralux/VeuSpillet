using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HighscoreHandler : BaseMenu {

	public static HighscoreHandler instance;

	List<HighscoreField> highscoreFields = new List<HighscoreField>();

	void Awake(){
		if(instance) {
			Destroy(this);
		}else{
			instance = this;
		}
		highscoreFields.AddRange(transform.GetComponentsInChildren<HighscoreField>());
		Hide();
	}

	void OnEnable(){
		StartCoroutine(GetHighscores());
	}

	IEnumerator GetHighscores(){
		string URL = "http://veu-spillet.dk/Prototype/loadHighscore.php/";
		WWW ItemsData = new WWW (URL);
		yield return ItemsData;

		string dataString = ItemsData.text;
		string[] highscoreData = dataString.Split('|');

		string[] segmentedHighscoreData;
		for (int i = 0; i < highscoreData.Length - 1; i++) {
			segmentedHighscoreData = highscoreData[i].Split('¤');
			highscoreFields[i].UpdateField(segmentedHighscoreData[0], segmentedHighscoreData[1]);
		}

		yield return new WaitForSeconds (0);
	}

	public override void Show ()
	{
		instance.gameObject.SetActive(true);
	}

	public override void Hide ()
	{
		instance.gameObject.SetActive(false);
	}
}