using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using System.Collections;
using DatabaseClassifications;

public class Login : MonoBehaviour {

	public InputField bruger;
	public InputField passwordField;
	private string password = "";

	public GameObject loginScreen;

	public GameObject failedLoginPanel;

	void Awake(){
		passwordField.onValueChanged.AddListener (delegate {
			OnValueChanged ();
		});
	}

	public void InitiateLogin(){
		StartCoroutine (Verify ());
	}

	IEnumerator Verify(){
		int i = 0;
		bool userFound = false;
		int targetUser = -1;

		while (!userFound && i < SetupUsers.users.Count) {
			if (SetupUsers.users [i].userName == bruger.text && SetupUsers.users [i].userPassword == password) {
				userFound = true;
				targetUser = i;
			}
			i++;
		}

		if (userFound) {
			Debug.Log ("Logged in as: " + SetupUsers.users [targetUser].userName);
			DataContainer.currentLoggedUser = SetupUsers.users [targetUser];
			MainMenu.instance.Show ();
			loginScreen.SetActive (false);
			bruger.text = "";
			passwordField.text = "";
			password = "";
		}else{
			SetupUsers.instance.Reload ();

			failedLoginPanel.SetActive (true);
			yield return new WaitForSeconds (3f);
			failedLoginPanel.SetActive (false);
		}

		yield return new WaitForSeconds (0.5f);
	}

	public void OnValueChanged(){
		if (passwordField.text.Length > 0) {
			if (passwordField.text [passwordField.text.Length - 1] != '*') {
				if (passwordField.text.Length > password.Length) {
					password += passwordField.text [passwordField.text.Length - 1];
					passwordField.text = passwordField.text.Substring (0, passwordField.text.Length - 1) + "*";
				} else {
					if (password.Length > 0) {
						password = password.Substring (0, passwordField.text.Length - 1);
					}
				}
			}
		} else {
			password = "";
		}
	}

	public void QuitApplication(){
		Application.Quit ();
	}
}