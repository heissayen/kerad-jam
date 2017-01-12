using UnityEngine;
using System.Collections;

public class manager : MonoBehaviour {
	//Control de la bola
	private bool withBall = false;
	private float withBallSec;
	private float withBallMaxSec = 3.0f;

	//Variables para Swipe
	private float fingerStartTime = 0.0f;
	private Vector2 fingerStartPos = Vector2.zero;
	private bool isSwipe = false;
	private bool isMove = false;
	private float minSwipeDist = 50.0f;
	private float minSwipeTime = 0.2f;

	//GameObjects y scripts
	public GameObject player;
	public GameObject ball;
	private movement scr_mov;
	private Shooting scr_shot;

	void Start () {
		scr_mov = player.GetComponent<movement> ();
		scr_shot = player.GetComponent<Shooting> ();
	}
	
	// Update is called once per frame
	void Update () {
		if (withBall && Time.time > withBallSec + withBallMaxSec) {
			scr_shot.NormalShoot (new Vector3(0,0,90),50.0f);
			withBall = false;
		}
		if (Input.touchCount > 0) {
			foreach (Touch touch in Input.touches) {
				switch (touch.phase) {
				case TouchPhase.Began:
					isMove = false;
					isSwipe = true;
					fingerStartTime = Time.time;
					fingerStartPos = touch.position;
					break;
				case TouchPhase.Canceled:
					isMove = false;
					isSwipe = false;
					break;
				case TouchPhase.Moved:
					isMove = true;
					break;
				case TouchPhase.Ended:
					float gestureTime = Time.time - fingerStartTime;
					float gestureDist = (touch.position - fingerStartPos).magnitude;
					if (isSwipe && isMove && gestureDist > minSwipeDist && gestureTime > minSwipeTime) {
						Vector2 direction = touch.position - fingerStartPos;
						Vector2 swipeType = Vector2.zero;
						if (withBall) {
							scr_shot.NormalShoot (new Vector3 (direction.x, 0, direction.y), 50.0f);
						} else {
							if (Mathf.Abs (direction.x) > Mathf.Abs (direction.y))
								swipeType = Vector2.right * Mathf.Sign (direction.x);
							else
								swipeType = Vector2.up * Mathf.Sign (direction.y);
							if (swipeType.x != 0.0f) {
								if (swipeType.x > 0.0f)
									scr_mov.right (1);
								else
									scr_mov.left (1);
							}
						}
					}
					isSwipe = false;
					isMove = false;
					withBall = false;
					break;
				}
			}
		}
	}

	void OnTriggerEnter(Collider coll){
		if (coll.gameObject.tag == "Ball") {
			withBall = true;
			withBallSec = Time.time;
		}
	}
}
