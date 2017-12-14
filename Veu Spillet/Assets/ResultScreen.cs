using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResultScreen : MonoBehaviour {

	public GameObject leftStar;
	public GameObject middleLeftStar;
	public GameObject middleStar;
	public GameObject middleRightStar;
	public GameObject rightStar;

	public GameObject starParticles;
	public GameObject completeParticles;
	public GameObject mediumRatingParticles;
	public GameObject maxRatingParticles;

	public float startDelay = 1f;
	public float starDelay = 1f;
	public float starDelayMultiplier = 0.3f;

	private int rating;

	public void SetSession(QuizSession current){
		CalculateRating (current.answers);
		StartCoroutine (ShowResults (current));
	}

	public void Hide(){
		leftStar.SetActive (false);
		middleLeftStar.SetActive (false);
		middleStar.SetActive (false);
		middleRightStar.SetActive (false);
		rightStar.SetActive (false);
	}

	private void CalculateRating(List<int> answers){
		int correct = 0;
		int total = 0;

		foreach (int answer in answers) {
			if (answer == 0) {
				correct++;
			}
			total++;
		}

		float percentageCompleted = ((float)correct) / ((float)total);

		if (percentageCompleted < 0.2f) {
			rating = 1;
		} else if (percentageCompleted < 0.4f) {
			rating = 2;
		} else if (percentageCompleted < 0.6f) {
			rating = 3;
		} else if (percentageCompleted < 0.8f) {
			rating = 4;
		} else {
			rating = 5;
		}
	}

	IEnumerator ShowResults(QuizSession current){
		yield return new WaitForSeconds (startDelay);
		yield return new WaitForSeconds (starDelay);
		leftStar.SetActive (true);
		Instantiate (starParticles, leftStar.transform.position - Camera.main.transform.forward, Quaternion.identity);

		if (rating > 1) {
			yield return new WaitForSeconds (starDelay + (starDelayMultiplier * starDelay * 1));
			rightStar.SetActive (true);
			Instantiate (starParticles, rightStar.transform.position - Camera.main.transform.forward, Quaternion.identity);

			if (rating > 2) {
				yield return new WaitForSeconds (starDelay + (starDelayMultiplier * starDelay * 2));
				middleLeftStar.SetActive (true);
				Instantiate (starParticles, middleLeftStar.transform.position - Camera.main.transform.forward, Quaternion.identity);

				if (rating > 3) {
					yield return new WaitForSeconds (starDelay + (starDelayMultiplier * starDelay * 3));
					middleRightStar.SetActive (true);
					Instantiate (starParticles, middleRightStar.transform.position - Camera.main.transform.forward, Quaternion.identity);

					if (rating > 4) {
						yield return new WaitForSeconds (starDelay + (starDelayMultiplier * starDelay * 4));
						middleStar.SetActive (true);
						Instantiate (starParticles, middleStar.transform.position - Camera.main.transform.forward, Quaternion.identity);
					}
				}
			}
		}

		yield return new WaitForSeconds (startDelay);

		switch (rating) {
		case 1:
			Instantiate (completeParticles, middleStar.transform.position - Camera.main.transform.forward, Quaternion.identity);
			break;
		case 5:
			Instantiate (maxRatingParticles, middleStar.transform.position - Camera.main.transform.forward, Quaternion.identity);
			break;
		default:
			Instantiate (mediumRatingParticles, middleStar.transform.position - Camera.main.transform.forward, Quaternion.identity);
			break;
		}
	}
}