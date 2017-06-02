using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DatabaseClassifications;

public class CategoryContainer : MonoBehaviour {

	public Category myCategory;

	public void OnClick(){

		foreach (Image i in transform.parent.GetComponentsInChildren<Image>()) {
			i.color = Color.white;
		}

		gameObject.GetComponent<Image> ().color = Color.green;
		GameObject.Find ("Challenge Panel").GetComponent<CreateBattle> ().setCategory(myCategory);
		Debug.Log ("Category was selected on button");
	}

}
