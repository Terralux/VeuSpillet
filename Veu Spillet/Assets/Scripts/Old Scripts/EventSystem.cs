using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DatabaseClassifications;

public class EventSystem : MonoBehaviour {

	public delegate void VoidEvent();
	public delegate void CreateBattleEvent(Category c, User u);

	public VoidEvent OnLoggedIn;
	public VoidEvent OnFailedLogin;
	public VoidEvent OnLoggedOut;
	public VoidEvent OnSelectedQuizMenu;
	public VoidEvent OnSelectedResultsMenu;
	public CreateBattleEvent OnPickedAQuizFormat;
	public VoidEvent OnBeganQuiz;
}