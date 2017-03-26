using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class generateLog : MonoBehaviour {

	GameObject[] posts;
	public GameObject post;
	public GameObject Parent;
	public GameObject content;

	string[] ds;
	string[] q;
	string[,] a;

	IEnumerator Start () {

		string URL = "http://veuspillet.dk/demo/getquiz.php";
		WWW ItemsData = new WWW (URL);
		yield return ItemsData;
		string dataString = ItemsData.text;
		ds = dataString.Split('#');

		string[] dl;
		q = new string[ds.Length-1];
		a = new string[ds.Length-1,4];
		for (int i = 0; i < ds.Length-1; i++) {
			dl = ds [i].Split (',');

			q [i] = dl [0];
			a [i,0] = dl [1];
			a [i,1] = dl [2];
			a [i,2] = dl [3];
			a [i,3] = dl [4];

		}

		URL = "http://veuspillet.dk/getlog.php";
		ItemsData = new WWW (URL);
		yield return ItemsData;
		dataString = ItemsData.text;
		ds = dataString.Split('#');

		posts = new GameObject[ds.Length-1];
		for (int i = 0; i < ds.Length-1; i++) {
			dl = ds [i].Split (',');

			posts[i] = Instantiate(post) as GameObject;
			posts[i].transform.SetParent(Parent.transform);
			posts [i].transform.GetChild (0).GetChild (0).GetComponent<Text> ().text = q[int.Parse( dl [1])];
			posts [i].transform.GetChild (1).GetChild (0).GetComponent<Text> ().text = a[int.Parse( dl [1]),0];
			posts [i].transform.GetChild (2).GetChild (0).GetComponent<Text> ().text = a[int.Parse( dl [1]),int.Parse( dl [2])];
			if (int.Parse (dl [2]) != 0)
				posts [i].transform.GetChild (2).GetChild (0).GetComponent<Text> ().color = Color.red;
			print ("" + i);
		}
		content.GetComponent<RectTransform> ().sizeDelta = new Vector2(content.GetComponent<RectTransform> ().sizeDelta.x, 100 * ds.Length - 1);
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
