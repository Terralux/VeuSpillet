using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class EmailHandler : MonoBehaviour {

	public static EmailHandler instance;

	void Awake(){
		if(instance){
			Destroy(this);
		}else{
			instance = this;
		}
	}

	public static void SendEmail (string emailAddress){
		instance.StartCoroutine(instance.SendInvitation(emailAddress));
	}

	public IEnumerator SendInvitation(string email){
		WWWForm myForm = new WWWForm();
		myForm.AddField ("email", email);

		WWW www = new WWW ("http://veu-spillet.dk/Prototype/sendInvitation.php", myForm);
		yield return www;

		Debug.Log(www.text);
		yield return new WaitForEndOfFrame();
	}
}