using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DatabaseClassifications;

public class SetupCategories : MonoBehaviour {

	public static List<Category> categories = new List<Category>();

	public static SetupCategories instance;

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
		string URL = "http://veu-spillet.dk/Prototype/loadAllCategories.php";
		WWW ItemsData = new WWW (URL);
		yield return ItemsData;

		categories.Clear ();

		string dataString = ItemsData.text;
		string[] categoryData = dataString.Split('|');

		string[] segmentedCategoryData;
		for (int i = 0; i < categoryData.Length-1; i++) {
			segmentedCategoryData = categoryData [i].Split (',');

			categories.Add (new Category (int.Parse (segmentedCategoryData [0]), segmentedCategoryData [1]));
		}

		yield return new WaitForSeconds (0);
	}
}