using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuizContainer : MonoBehaviour {

	public Quiz myQuiz;

	public void OnClick(){
		Toolbox.FindRequiredComponent<EventSystem> ().OnBeganQuiz ();
		DataContainer.q = myQuiz;
	}
}