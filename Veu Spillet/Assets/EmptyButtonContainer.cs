using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EmptyButtonContainer : MonoBehaviour {

	public delegate void VoidEvent(int value);
	public VoidEvent OnClickSendValue;

	[HideInInspector]
	public int myIndex;

	public void OnClick(){
		//DeleteFromListMenu.instance.OnClick (myIndex);

		if (OnClickSendValue != null) {
			OnClickSendValue (myIndex);
		}
	}
}