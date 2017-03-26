using UnityEngine;
using System.Collections;

public class hover : MonoBehaviour {

	Vector3 start;
	public Vector2 speed;

	// Use this for initialization
	void Start () {
		start = transform.position;
	}
	
	// Update is called once per frame
	void Update () {
		this.transform.position = start + new Vector3(Mathf.Cos(Time.time/speed.x)*20,Mathf.Sin(Time.time/speed.y)*20,0);
	}
}
