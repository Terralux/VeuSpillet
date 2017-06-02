using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DatabaseClassifications;

public class CreateBattle : MonoBehaviour {

	public User selectedUser;
	public Category selectedCategory = new Category ();

	public void setUser(User newUser){
		selectedUser = newUser;
		if (selectedCategory.id > 0) {
			Toolbox.FindRequiredComponent<EventSystem> ().OnPickedAQuizFormat (selectedCategory, selectedUser);
		}
	}

	public void setCategory(Category newCategory){
		selectedCategory = newCategory;
		if (selectedUser.userID > 0) {
			Toolbox.FindRequiredComponent<EventSystem> ().OnPickedAQuizFormat (selectedCategory, selectedUser);
		}
	}
}
