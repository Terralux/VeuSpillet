using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DatabaseClassifications;

public class QuizContainer : MonoBehaviour {

	public Quiz myQuiz;

	public void OnClick(){
		QuizMenu.instance.ChoseAQuiz (myQuiz);
	}
}