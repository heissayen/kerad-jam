using UnityEngine;
using System.Collections;

public class fire : MonoBehaviour {
	
	private float velocidad = 40.0f;
	public GameObject punta;
	private Vector3 t = new Vector3 (0,0,0);
	public int horientacion = 1;

	// Use this for initialization
	void Start () {
		//vec = new Vector3(0,0,velocidad);
	}
	
	// Update is called once per frame
	void Update () {
		transform.Rotate (Vector3.up * Time.deltaTime * velocidad, Space.World);
		t += Vector3.up * Time.deltaTime * velocidad;
		if (t.y > 180)
			velocidad = -40.0f;
		else if(t.y < 0)
			velocidad = 40.0f;
		Debug.Log (punta.transform.position);
	}
}
