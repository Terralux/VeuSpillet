using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DatabaseClassifications;

public class SetupCategories : MonoBehaviour {

	public static List<Category> categories = new List<Category>();

	void Awake(){
		StartCoroutine (Initiate ());
	}

	IEnumerator Initiate(){
		string URL = "http://veu-spillet.dk/Prototype/loadAllCategories.php";
		WWW ItemsData = new WWW (URL);
		yield return ItemsData;
		string dataString = ItemsData.text;
		string[] categoryData = dataString.Split('|');

		string[] segmentedCategoryData;
		for (int i = 0; i < categoryData.Length-1; i++) {
			segmentedCategoryData = categoryData [i].Split (',');

			categories.Add (new Category (int.Parse (segmentedCategoryData [0]), segmentedCategoryData [1]));
		}

		Debug.Log (categories.Count);
		yield return new WaitForSeconds (0);
	}
}