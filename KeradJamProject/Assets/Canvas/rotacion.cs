using UnityEngine;
using System.Collections;

public class rotacion : MonoBehaviour {
	public float velocidad;
	private float act_rot;
	void Update () {
		act_rot += velocidad;
		transform.rotation = Quaternion.Euler (0, 0, act_rot);
	}
}
