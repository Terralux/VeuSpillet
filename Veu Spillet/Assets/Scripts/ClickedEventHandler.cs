using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class ClickedEventHandler : MonoBehaviour {

	public ParticleSystem clickable_particle;
	public static GameObject correctAnswer_particle; 
	public static GameObject wrongAnswer_particle;

	void Awake(){
		correctAnswer_particle = Resources.Load ("CorrectParticleSystem") as GameObject;
		wrongAnswer_particle = Resources.Load ("WrongParticleSystem") as GameObject;
	}

	/*
	public void OnPointerDown (PointerEventData eventData){
		if (eventData.pointerCurrentRaycast.gameObject.GetComponent<IsClickable> ()) {
			Instantiate (clickable_particle, Camera.main.ScreenToWorldPoint(Input.mousePosition), Quaternion.identity);
		}
	}
	*/

	public static void TriggerCorrect(){
		Instantiate (correctAnswer_particle, Camera.main.ScreenToWorldPoint(Input.mousePosition) + Camera.main.transform.forward, Quaternion.identity);
	}

	public static void TriggerWrong(){
		Instantiate (wrongAnswer_particle, Camera.main.ScreenToWorldPoint(Input.mousePosition) + Camera.main.transform.forward, Quaternion.identity);
	}
}