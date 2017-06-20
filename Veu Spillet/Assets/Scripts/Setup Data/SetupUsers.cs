using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using DatabaseClassifications;

public class SetupUsers : MonoBehaviour {

	public static SetupUsers instance;

	public static List<User> users = new List<User>();

	void Awake(){
		if (instance != null) {
			Destroy (this);
		} else {
			instance = this;
		}
		StartCoroutine (Initiate ());
	}

	public void Reload(){
		StartCoroutine (Initiate ());
	}

	IEnumerator Initiate(){
		string URL = "http://veu-spillet.dk/Prototype/loadAllUsers.php";

		WWW ItemsData = new WWW (URL);
		yield return ItemsData;

		users.Clear ();

		string dataString = ItemsData.text;
		string[] userData = dataString.Split('|');

		string[] segmentedUserData;
		for (int i = 0; i < userData.Length-1; i++) {
			segmentedUserData = userData [i].Split (',');

			users.Add (new User (int.Parse (segmentedUserData [0]), int.Parse (segmentedUserData [1]), segmentedUserData [2], segmentedUserData [3], "1" == segmentedUserData [4]));
		}

		yield return new WaitForSeconds (0);
	}
}