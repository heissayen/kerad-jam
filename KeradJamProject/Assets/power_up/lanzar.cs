using UnityEngine;
using System.Collections;

public class lanzar : MonoBehaviour {

	public GameObject punta;
	public GameObject audio_contr;
	private Rigidbody rb;
	private float fuerza = 50.0f;

	void OnTriggerEnter(Collider coll){
		//coll.gameObject.GetComponent<Rigidbody> ()
		Vector3 dir = punta.transform.position - transform.position;
		rb = coll.gameObject.GetComponent<Rigidbody> ();
		rb.velocity = Vector3.zero;
		rb.velocity = dir.normalized * fuerza;
		audio_contr.GetComponent<AudioManager> ().playSound ("power_up");
		Destroy (gameObject);
	}
}
