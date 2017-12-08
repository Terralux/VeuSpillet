using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EmptyButtonContainer : MonoBehaviour {

	public delegate void VoidEvent(int value);
	public VoidEvent OnClickSendValue;

	[HideInInspector]
	public int myIndex;

	public void OnClick(){
		if (OnClickSendValue != null) {
			OnClickSendValue (myIndex);
		}
	}
}