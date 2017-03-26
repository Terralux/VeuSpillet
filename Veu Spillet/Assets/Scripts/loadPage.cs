using UnityEngine;
public class LoadPage : MonoBehaviour {
	public void jumpToURL(string page){
		Application.OpenURL (page);
	}
}