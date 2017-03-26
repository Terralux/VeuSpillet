using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class loadPage : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public void jumpToScene(string page){
	if (page == "quickmix")
		SceneManager.LoadScene("quickmix");

	}

	public void jumpToURL(string page){
		Application.OpenURL ("http://www.veucenterost.dk/");
	}
}
