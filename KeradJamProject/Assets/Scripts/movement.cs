using UnityEngine;
using System.Collections;

public class movement : MonoBehaviour {
	public int fields = 5;
	public int pos = 3;
	public GameObject[] refs;
	public float cd = 0.5f;
	private float cd_time;

	public float LerpTime = 0.2f;
	private bool isLerping;

	private Vector3 startPosition;
	private Vector3 endPosition;

	private float  timeStartedLerping;


	void StartLerping(){
		isLerping = true;
		timeStartedLerping = Time.time;

		startPosition = transform.position;
		endPosition = new Vector3 (refs[pos].transform.position.x * 1.0f, 0, 0);
	}
	
	// Update is called once per frame
	void Update () {
		if (transform.position.x != refs[pos].transform.position.x * 1.0f) {
			StartLerping ();
		}
	}

	void FixedUpdate(){
		if(isLerping){
			float timeSinceStarted = Time.time - timeStartedLerping;
			float percentageComplete = timeSinceStarted / LerpTime;

			transform.position = Vector3.Lerp (startPosition, endPosition, percentageComplete);
			if(percentageComplete >= 0.95f){
				isLerping = false;
				transform.position = new Vector3 (pos * 1.0f, 0, 0);
			}
		}
	}

	public void right(int d){
		if (pos + d < fields && Time.time > cd_time + cd) {
			pos += d;
			cd_time = Time.time;
		}
	}

	public void left(int d){
		if (pos - d > 0 && Time.time > cd_time + cd) {
			pos -= d;
			cd_time = Time.time;
		}
	}
}
