using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using System.Collections;
using DatabaseClassifications;

public class Login : MonoBehaviour {

	public InputField bruger;
	public InputField password;

	public void InitiateLogin(){
		StartCoroutine (Verify ());
	}

	IEnumerator Verify(){
		int i = 0;
		bool userFound = false;
		int targetUser = -1;

		while (!userFound && i < SetupUsers.users.Count) {
			if (SetupUsers.users [i].userName == bruger.text && SetupUsers.users [i].userPassword == password.text) {
				userFound = true;
				targetUser = i;
			}
			i++;
		}

		if (userFound) {
			Debug.Log ("Logged in as: " + SetupUsers.users [targetUser].userName);
			DataContainer.currentLoggedUser = SetupUsers.users [targetUser];
			MainMenu.instance.Show ();
		}else{
			Debug.LogWarning ("No user found!");
		}

		yield return new WaitForSeconds (0.5f);
	}
}
