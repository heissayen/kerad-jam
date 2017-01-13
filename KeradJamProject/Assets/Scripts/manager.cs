using UnityEngine;
using System.Collections;

public class manager : MonoBehaviour {
	//Control de la bola
	public bool withBall = false;
	private float withBallSec;
	private float withBallMaxSec = 3.0f;

	//Variables para Swipe
	private float fingerStartTime = 0.0f;
	private Vector2 fingerStartPos = Vector2.zero;
	private bool isSwipe = false;
	private bool isMove = false;
	private float minSwipeDist = 20.0f;
	private float minSwipeTime = 0.2f;

	public bool isMine = false;
	PhotonView photonView;

	//GameObjects y scripts
	public GameObject ball;
	private movement scr_mov;
	private Shooting scr_shot;

	void Start () {
		scr_mov = GetComponent<movement> ();
		scr_shot = GetComponent<Shooting> ();
		photonView = GetComponent<PhotonView>();
	}
	
	// Update is called once per frame
	void Update () {
		/*if (withBall && Time.time > withBallSec + withBallMaxSec) {
			scr_shot.NormalShoot (new Vector3(0,0,90),50.0f);
			withBall = false;
		}*/
		if (Input.touchCount > 0 && isMine) {
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
					if (isSwipe && isMove && gestureDist > minSwipeDist) {
						Vector2 direction = touch.position - fingerStartPos;
						if (withBall) {
							scr_shot.NormalShoot (ball.transform.position ,new Vector3 (direction.x, 0, direction.y), 50.0f);
						}
						else {
							if(direction.x > 0)
							{
								scr_mov.right (1);
								photonView.RPC("MoveRight", PhotonTargets.Others);
							}				
							else
							{
								scr_mov.left (1);
								photonView.RPC("MoveLeft", PhotonTargets.Others);
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

		if (Input.GetKeyDown(KeyCode.RightArrow) &isMine)
		{
			MoveRight();
		}

	}

	public void ResetBall()
	{
		ball.GetComponent<Rigidbody>().velocity = Vector3.zero;
		ball.transform.position = this.transform.position + this.transform.forward*2;
		withBall = true;
	}

	void OnTriggerEnter(Collider coll){
		if (coll.gameObject.tag == "Ball") {
			withBall = true;
			withBallSec = Time.time;
			ball.GetComponent<Rigidbody>().velocity = Vector3.zero;
		}
	}

	[PunRPC]
	void UpdateBallAndPlayer()
	{
		
	}

	[PunRPC]
	void MoveRight()
	{
		scr_mov.right(1);
	}

	[PunRPC]
	void MoveLeft()
	{
		scr_mov.left(1);
	}
}
