using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DatabaseClassifications;

public class QuizContainer : MonoBehaviour {

	public Quiz myQuiz;

	public void OnClick(){
		DataContainer.selectedQuiz = myQuiz;
		Toolbox.FindRequiredComponent<EventSystem> ().OnBeganQuiz ();
	}
}