using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using System.Collections;

public class Login : MonoBehaviour {

	public GameObject bruger;
	public GameObject password;

	string[] ds;
	string[] uid;
	string[] name;
	string[] pass;
	string user;

	// Use this for initialization
	IEnumerator Start () {
		
		string URL = "http://veuspillet.dk/getuser.php";
		WWW ItemsData = new WWW (URL);
		yield return ItemsData;
		string dataString = ItemsData.text;
		ds = dataString.Split('#');

		string[] dl;
		uid = new string[ds.Length-1];
		name = new string[ds.Length-1];
		pass = new string[ds.Length-1];
		for (int i = 0; i < ds.Length-1; i++) {
			dl = ds [i].Split (',');

			uid [i] = dl [0];
			name [i] = dl [2];
			pass [i] = dl [3];

			print(""+uid[i]+" "+name [i]+" "+pass [i]);

		}

		yield return new WaitForSeconds (0);
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public void verify(){
		StartCoroutine (verify2 ());
	}

	IEnumerator verify2(){
		int i = 0;
		bool userFound = false;
		print ("ul:" + uid.Length);
		while (!userFound && i < uid.Length) {
			if (name [i] == bruger.GetComponent<Text> ().text && pass [i] == password.GetComponent<Text> ().text) {
				userFound = true;	
				user = uid [i];
				print ("" + user);
			}
		}
		if (userFound) {
			
			WWWForm form = new WWWForm();

			form.AddField("user", user);
			WWW www = new WWW("http://veuspillet.dk/set_u.php", form);
			print ("Send " + Time.time);

			yield return new WaitForSeconds (1f);

			Application.OpenURL ("http://veuspillet.dk/veuMenu/");
		}
	}
}
