using UnityEngine;
using System.Collections;

public class ballrandommov : MonoBehaviour {

	// Use this for initialization
	void Start () {
		GetComponent<Rigidbody>().AddForce(new Vector3(2.2f, 0, 1)* 15f, ForceMode.Impulse);
	
	}
	
	
}
