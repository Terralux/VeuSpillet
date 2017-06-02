using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using System.Collections;

public class GameMaster : MonoBehaviour {

	public GameObject[] buttons;
	public GameObject clock;
	public GameObject mountain;
    public float timeForEachQuestion = 20f;
    public float totalScore = 0f;
    public float lastScore = 0f;
    public float sandLeft = 100;
    public int correct = 0;
    public int wrong = 0;
    public int questionNumber = 0;
    public float timeStamp = 0f;
    public int rnd;
	public string quizNumber;
	public int userNumber;

	string[] ds;
    string[] q;
    string[,] a;

	bool updateReady = false;

	/* = { "Klik på det korrekte svar", "Hvad man man få i SVU?", "Kan ledige få SVU?","Hvad betyder VEU?" }
	   = { {"Korrekt","Forkert","Forkert","Forkert" },
                    {"80% af højeste dagpengesats","Højeste dagpengesats ","80% løntab, beregnet på baggrund af sidste 3 måneders løn","100% løntab, beregnet på baggrund af sidste 3 måneders løn" },
                    {"Nej","Ja, hvis det godkendes af kommunen/jobcentret","Ja, hvis man har haft arbejde i mindst 2 år","Ja, hvis man er medlem af en A-kasse" },                     
        {"Voksen- og efteruddannelse","Vejlednings- og erhvervsuddannelse","Videregående efteruddannelse","Voksen eliteuddannelse" } }
*/
    GameObject sandTop, sandBottom;
    public float sandBottomStartPosY;
    public float sandBottomStartSizeY;

    Text question, countDown, total, c, w, timeLeft;
    Text[] answers;

	string dataLog;
	//string dataLogURL = "veuspillet.dk/dataLog.php";


	IEnumerator Start () {
		

		quizNumber = "" + userNumber + ""+System.DateTime.Now.ToString ("yy:MM:dd:HH:mm:ss");
		Debug.Log(quizNumber);

		string URL = "http://veuspillet.dk/demo/getquiz.php";
		WWW ItemsData = new WWW (URL);
		yield return ItemsData;
		string dataString = ItemsData.text;
		ds = dataString.Split('#');

		string[] dl;
		q = new string[ds.Length-1];
		a = new string[ds.Length-1,4];
		for (int i = 0; i < ds.Length-1; i++) {
			dl = ds [i].Split (',');

			q [i] = dl [0];
			a [i,0] = dl [1];
			a [i,1] = dl [2];
			a [i,2] = dl [3];
			a [i,3] = dl [4];
	
		}

        answers = new Text[4];

        question = GameObject.Find("Question").GetComponent<Text>();
        countDown = GameObject.Find("countDown").GetComponent<Text>();
        total = GameObject.Find("TotalScore").GetComponent<Text>();
		timeLeft = GameObject.Find("SandLeft").GetComponent<Text>();

        c = GameObject.Find("Correct").GetComponent<Text>();
        w = GameObject.Find("Wrong").GetComponent<Text>();
        answers[0] = GameObject.Find("Text0").GetComponent<Text>();
        answers[1] = GameObject.Find("Text1").GetComponent<Text>();
        answers[2] = GameObject.Find("Text2").GetComponent<Text>();
        answers[3] = GameObject.Find("Text3").GetComponent<Text>();

        sandTop = GameObject.Find("sandTop");
        sandBottom = GameObject.Find("sandBund");
        sandBottomStartPosY = sandBottom.transform.position.y;
        sandBottomStartSizeY = sandBottom.transform.localScale.y;

        question.text = q[0];
		Debug.Log(q[0]);
        answers[0].text = a[0, 0];
        answers[1].text = a[0, 1];
        answers[2].text = a[0, 2];
        answers[3].text = a[0, 3];

		updateReady = true;
    }


    void Update () {
		if (updateReady) {
			sandLeft -= Time.deltaTime * 10;

			countDown.text = "" + (int)(timeForEachQuestion + timeStamp - Time.time + 0.99f);
			timeLeft.text = "" + (int)(sandLeft);
			if ((Time.time > timeStamp + timeForEachQuestion))
				buttonClick (-1);
		
			sandTop.GetComponent<Image> ().fillAmount = Mathf.Pow (sandLeft / 1000f, 0.5f);

			sandBottom.GetComponent<Image> ().fillAmount = Mathf.Pow (totalScore / 100000f, 0.2f);
			//sandBottom.transform.position = new Vector2 (sandBottom.transform.position.x, sandBottomStartPosY - 390f + 390f * Mathf.Pow (totalScore / 100000f, 0.2f));
			float clockRation = ((float)(timeForEachQuestion + timeStamp - Time.time))/timeForEachQuestion;
			clock.GetComponent<Image>().fillAmount = clockRation;
			clock.GetComponent<Image> ().color = new Color (1 - clockRation, clockRation, 0,(1 - clockRation)/4*3+0.25f);
		}
	}

    public void buttonClick(int b)
	{
		StartCoroutine (fadeButtons());

		if (b == rnd) {
			correct++;
			c.text = "" + correct;
			sandLeft += (20 + timeStamp - Time.time) * 2;
		} else {
			wrong++;
			w.text = "" + wrong;
			sandLeft -= (20 + timeStamp - Time.time) * 5;
		}
		totalScore += 20 + timeStamp - Time.time;
		total.text = "" + (int)(totalScore * 100);

		StartCoroutine (logData (1,1,(b+4-rnd),1));

		Random.Range (0, q.Length);

		questionNumber = (questionNumber + Random.Range (0, q.Length)) % q.Length;
		if (questionNumber >= q.Length)
			questionNumber = 0;
		question.text = q [questionNumber];

		rnd = Random.Range (0, 4);
		print (rnd);
		answers [(0 + rnd) % 4].text = a [questionNumber, 0];
		answers [(1 + rnd) % 4].text = a [questionNumber, 1];
		answers [(2 + rnd) % 4].text = a [questionNumber, 2];
		answers [(3 + rnd) % 4].text = a [questionNumber, 3];

		timeForEachQuestion -= 0.1f;

		timeStamp = Time.time;

		if (sandLeft < 0)
			SceneManager.LoadScene ("quizstarter");
	
    }

	IEnumerator logData(int quizNum, int question, int ans, int ts){
	
		WWWForm form = new WWWForm();
		form.AddField("quiz", ""+99);
		form.AddField("question", ""+questionNumber);
		form.AddField("answer", ""+ans);
		form.AddField("total", ""+totalScore);
		//WWW www = new WWW(dataLogURL, form);
		print ("Send " + Time.time);

		yield return new WaitForSeconds (0);
	}

	IEnumerator fadeButtons(){
		for (int i = 0; i < 4; i++) {
			buttons [i].SetActive (false);
		}
		for (int i = 4; i < 8; i++) {
			buttons [i].SetActive (true);
		}
		yield return new WaitForSeconds (1.5f);
		for (int i = 0; i < 4; i++) {
			buttons [i].GetComponent<RectTransform> ().localScale = Vector3.zero;
			buttons [i].SetActive (true);
		}
		for (int i = 0; i < 4; i++) {
			float start = Time.time;
			while (Time.time - start <= 0.5f) {
				yield return new WaitForSeconds (0.01f);
				buttons [i].GetComponent<RectTransform> ().localScale = Vector3.one * Mathf.Sin( (Time.time - start)*4)*1.2f;
			}
			buttons [i].GetComponent<RectTransform> ().localScale = Vector3.one;
		}
		for (int i = 4; i < 8; i++) {
			buttons [i].SetActive (false);
		}

	}
}
