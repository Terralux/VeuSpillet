using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EventSystem : MonoBehaviour {

	public delegate void VoidEvent();

	public VoidEvent OnLoggedIn;
	public VoidEvent OnFailedLogin;
	public VoidEvent OnLoggedOut;
	public VoidEvent OnSelectedQuizMenu;
	public VoidEvent OnSelectedResultsMenu;
	public VoidEvent OnPickedAQuizFormat;
}