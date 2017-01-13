using UnityEngine;
using System.Collections;

public class Shooting : MonoBehaviour {

	public PhotonView photonView;

	public float min_angle = 25;
	float max_angle;

	[Range(15,150)]
	public float power = 50f;

	Vector3 dir;

	public GameObject ball;
	public bool isPlayerOne;

	float angle;
	Vector3 vct;

	// Use this for initialization
	void Awake () {
		max_angle = 180 - min_angle;
		dir = new Vector3(1, 0, Random.Range(-5f, 5f));

	}
	
	// Update is called once per frame
	void Update () 
	{
		if (Input.GetKeyDown(KeyCode.Space))
		{
			dir = new Vector3(Random.Range(-5f, 5f), 0, 1);
			Debug.Log("dir: " + dir);
			//NormalShoot(dir, power);
			photonView.RPC("RPCShoot", PhotonTargets.All, dir, power);
		}

		/*if (isPlayerOne)
		{
			vct = Input.mousePosition -transform.position;
			vct.Normalize();
			//angle = Vector3.Angle (new Vector3(vct.x, 0, vct.y), new Vector3(0, 0, 1));
			//angle = Vector2.Angle(vct, Vector2.one);		
		}*/
			
	}

	public void ShootLogic(Vector3 origin, Vector3 direction, float pow) 
	{
		direction.Normalize();
		//direction = CheckAngle(direction);
		ball.transform.position = origin;
		this.GetComponentInChildren<Animator>().SetTrigger("Shoot");

		ball.GetComponent<Rigidbody>().AddForce(direction * pow, ForceMode.Impulse);
	}

	Vector3 CheckAngle(Vector3 direction)
	{
		if (Vector3.Angle(direction, Vector3.forward) < min_angle)
		{
			return new Vector3(0.5f, 0, 1);
		}
		else if (Vector3.Angle(direction, Vector3.forward) > max_angle)
		{
			return new Vector3(-0.5f, 0, 1);
		}

		return direction;
	}

	void OnGUI() {

        GUI.Label(new Rect(10, 40, 333, 333), "Angle: " + angle + " mousPosition: " + vct);
    }

	public void NormalShoot(Vector3 origin, Vector3 direction, float pow) 
	{
		photonView.RPC("RPCShoot", PhotonTargets.All, origin, direction, pow);
	}

	[PunRPC]
	void RPCShoot(Vector3 ori, Vector3 d, float pow)
	{
		ShootLogic(ori,d, pow);
	}


}
