using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DatabaseClassifications;

public class CategoryContainer : MonoBehaviour {

	public Category myCategory;

	public void OnClick(){
		QuizCategoryMenu.instance.ChoseACategory (myCategory);
		Debug.Log ("Clicked on Category");
	}

}
