using UnityEngine;
using System.Collections;

public class Shooting : MonoBehaviour {

	public float min_angle = 25;
	float max_angle;

	[Range(15,150)]
	public float power = 50f;

	Vector3 dir;

	public GameObject ball;


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
			NormalShoot(dir, power);
		}
		
	}

	public void NormalShoot(Vector3 direction, float pow) 
	{
		direction.Normalize();
		direction = CheckAngle(direction);

		Debug.Log("Angle: " + Vector3.Angle(dir, Vector3.right));

		ball.GetComponent<Rigidbody>().AddForce(direction * pow, ForceMode.Impulse);
	}

	Vector3 CheckAngle(Vector3 direction)
	{
		if (Vector3.Angle(dir, Vector3.right) < min_angle)
		{
			Debug.Log("new angle: " + Vector3.Angle(new Vector3(0.5f, 0, 1), Vector3.right));
			return new Vector3(0.5f, 0, 1);
		}
		else if (Vector3.Angle(dir, Vector3.right) > max_angle)
		{
			return new Vector3(-0.5f, 0, 1);
		}

		return direction;
	}


}
