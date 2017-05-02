using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using System.Collections;

public class Login : MonoBehaviour {

	public InputField bruger;
	public InputField password;

	string[] users;
	string[] userIDs;
	string[] userNames;
	string[] userPassword;
	string user;

	// Use this for initialization
	IEnumerator Start () {

		string URL = "http://veuspillet.dk/getAllUsers.php";
		//string URL = "http://veuspillet.dk/getuser.php";

		WWW ItemsData = new WWW (URL);
		yield return ItemsData;
		string dataString = ItemsData.text;
		users = dataString.Split('|');

		string[] segmentedUserData;
		userIDs = new string[users.Length-1];
		userNames = new string[users.Length-1];
		userPassword = new string[users.Length-1];
		for (int i = 0; i < users.Length-1; i++) {
			segmentedUserData = users [i].Split (',');

			userIDs [i] = segmentedUserData [0];
			userNames [i] = segmentedUserData [2];
			userPassword [i] = segmentedUserData [3];

			print("" + userIDs[i] + " " + userNames [i] + " " + userPassword [i]);

		}

		yield return new WaitForSeconds (0);
	}

	public void InitiateLogin(){
		StartCoroutine (Verify ());
	}

	IEnumerator Verify(){
		int i = 0;
		bool userFound = false;

		print ("Collective User IDs: " + userIDs.Length);

		while (!userFound && i < userIDs.Length) {
			if (userNames [i] == bruger.text && userPassword [i] == password.text) {
				userFound = true;	
				user = userIDs [i];
			}
			i++;
		}

		if (userFound) {
			Debug.Log ("Logged in as: " + user);
			Toolbox.FindRequiredComponent<EventSystem> ().OnLoggedIn ();

			/*WWWForm form = new WWWForm();

			form.AddField("user", user);
			WWW www = new WWW("http://veuspillet.dk/set_u.php", form);
			print ("Send " + Time.time);

			yield return new WaitForSeconds (1f);

			Application.OpenURL ("http://veuspillet.dk/veuMenu/");
			*/
		}else{
			Debug.LogWarning ("No user found!");
			Toolbox.FindRequiredComponent<EventSystem> ().OnFailedLogin ();
		}

		yield return new WaitForSeconds (0.5f);
	}
}
