using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuizResultsMenu : BaseMenu {
	
	public static QuizResultsMenu instance;

	public static QuizSession currentSession;
	public GameObject contentTarget;
	private static GameObject contentButton;

	public void Awake(){
		if (instance != null) {
			Destroy (this);
		} else {
			instance = this;
		}
		if (Application.isMobilePlatform) {
			contentButton = Resources.Load ("Results Post Mobile") as GameObject;
		} else {
			contentButton = Resources.Load ("Results Post") as GameObject;
		}
		Hide ();
	}

	public static void Show (QuizSession currentSession)
	{
		QuizResultsMenu.currentSession = currentSession;
		instance.Show ();

		for (int i = 0; i < currentSession.questions.Length; i++) {
			GameObject go = Instantiate (contentButton, instance.contentTarget.transform);

			go.GetComponent<ResultsContainer> ().Fill (
				currentSession.questions [i].question,
				currentSession.questions [i].answers [0],
				(currentSession.answers [i] >= 0 ? currentSession.questions [i].answers [currentSession.answers [i]] : ""),
				currentSession.questions [i].questionID
			);
		}

		currentSession.SaveToDatabase ();
	}

	public override void Show ()
	{
		instance.gameObject.SetActive (true);
	}

	public override void Hide ()
	{
		instance.gameObject.SetActive (false);
	}

	public void GoToMainMenu(){
		MainMenu.instance.Show ();
		Clear ();
		Hide ();
	}

	public void Clear(){
		foreach (Transform t in instance.contentTarget.transform) {
			if (t != instance.contentTarget.transform) {
				Destroy (t.gameObject);
			}
		}
	}
}