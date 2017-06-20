using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Networking;

public class WebTestScript : MonoBehaviour {

	private Text myText;

	void Start () {
		myText = GetComponent<Text> ();
		StartCoroutine (Test ());
	}
	
	IEnumerator Test () {
		/*
		UnityWebRequest request = UnityWebRequest.Get ("http://veu-spillet.dk/Prototype/loadAllUsers.php");

		yield return request.Send ();

		if (request.GetResponseHeaders ().Count > 0)
			foreach (KeyValuePair<string, string> entry in request.GetResponseHeaders())
				Debug.Log (entry.Key + " : " + entry.Value);

		myText.text = request.downloadHandler.text;

		if (request.downloadHandler.text != "") {
			Debug.Log (request.downloadHandler.text);
		}
		*/

		string URL = "http://veu-spillet.dk/Prototype/loadAllUsers.php";

		WWW ItemsData = new WWW (URL);
		yield return ItemsData;

		myText.text = ItemsData.text;
	}
}