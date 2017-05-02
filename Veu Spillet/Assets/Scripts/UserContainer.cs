using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UserContainer : MonoBehaviour {

	public User myUser;

	public void OnClick(){

		foreach (Image i in transform.parent.GetComponentsInChildren<Image>()) {
			i.color = Color.white;
		}

		gameObject.GetComponent<Image> ().color = Color.green;
		GameObject.Find ("Challenge Panel").GetComponent<CreateBattle> ().setUser (myUser);
		Debug.Log ("User was selected on button");
	
	}

}
