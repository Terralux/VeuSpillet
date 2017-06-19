using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DatabaseClassifications;

public class DatabaseDeleter : MonoBehaviour {

	public static DatabaseDeleter instance; 

	void Awake(){
		if (instance != null) {
			Destroy (this);
		} else {
			instance = this;
		}
	}

	public void DeleteUser(User user){
		StartCoroutine (DeleteUserFromDatabase (user));
	}

	private IEnumerator DeleteUserFromDatabase(User user){
		WWWForm myForm = new WWWForm();
		myForm.AddField ("userID", user.userID);

		WWW www = new WWW ("http://veu-spillet.dk/Prototype/deleteUser.php", myForm);
		yield return www;

		if (www.text != "Error") {
			DeleteFromListMenu.Clear ();
		} else {
			DeleteFromListMenu.instance.Error ();
		}
	}

	public void DeleteQuiz(Quiz quiz){
		StartCoroutine (DeleteQuizFromDatabase (quiz));
	}

	private IEnumerator DeleteQuizFromDatabase(Quiz quiz){
		WWWForm myForm = new WWWForm();
		myForm.AddField ("quizID", quiz.quizID);

		WWW www = new WWW ("http://veu-spillet.dk/Prototype/deleteQuiz.php", myForm);
		yield return www;

		if (www.text != "Error") {
			DeleteFromListMenu.Clear ();
		} else {
			DeleteFromListMenu.instance.Error ();
		}
	}

	public void DeleteCategory(Category category){
		StartCoroutine (DeleteCategoryFromDatabase (category));
	}

	private IEnumerator DeleteCategoryFromDatabase(Category category){
		WWWForm myForm = new WWWForm();
		myForm.AddField ("categoryID", category.id);

		WWW www = new WWW ("http://veu-spillet.dk/Prototype/deleteCategory.php", myForm);
		yield return www;

		if (www.text != "Error") {
			DeleteFromListMenu.Clear ();
		} else {
			DeleteFromListMenu.instance.Error ();
		}
	}

	public void DeleteQuestion(Question question){
		StartCoroutine (DeleteQuestionFromDatabase (question));
	}

	private IEnumerator DeleteQuestionFromDatabase(Question question){
		WWWForm myForm = new WWWForm();
		myForm.AddField ("questionID", question.questionID);

		WWW www = new WWW ("http://veu-spillet.dk/Prototype/deleteQuestion.php", myForm);
		yield return www;

		if (www.text != "Error") {
			DeleteFromListMenu.Clear ();
		} else {
			DeleteFromListMenu.instance.Error ();
		}
	}
}