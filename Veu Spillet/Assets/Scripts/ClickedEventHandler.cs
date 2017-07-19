using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class ClickedEventHandler : MonoBehaviour, IPointerDownHandler {

	public ParticleSystem clickable_particle;
	public static GameObject correctAnswer_particle; 
	public static GameObject wrongAnswer_particle;
		
	// MY TWO PARTILE SYSTEMS CORRECT AND WRONG - ARE BEING SET TO 'NULL' AS SOON AS THE GAME STARTS! - BECAUSE IT IS GODDAMN PARTICLE SYSTEMS!

	void Awake(){
		correctAnswer_particle = Resources.Load ("CorrectParticleSystem") as GameObject;
		wrongAnswer_particle = Resources.Load ("WrongParticleSystem") as GameObject;
	}

	#region IPointerDownHandler implementation

	public void OnPointerDown (PointerEventData eventData)
	{

		if (eventData.pointerCurrentRaycast.gameObject.GetComponent<IsClickable> ()) {
			Debug.Log ("Object is clickable");
			Instantiate (clickable_particle, Camera.main.ScreenToWorldPoint(Input.mousePosition), Quaternion.identity);

		} else {
			
			Debug.Log ("Object is NOT clickable");
	
		}
			
	}

	#endregion

	public static void TriggerCorrect(){
		Instantiate (correctAnswer_particle, Camera.main.ScreenToWorldPoint(Input.mousePosition), Quaternion.identity);
	}

	public static void TriggerWrong(){
		Instantiate (wrongAnswer_particle, Camera.main.ScreenToWorldPoint(Input.mousePosition), Quaternion.identity);
	}
		

}