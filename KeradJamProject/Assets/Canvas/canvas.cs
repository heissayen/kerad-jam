using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class canvas : MonoBehaviour {

	private float start_time;
	public float loading_time = 1.5f;

	public GameObject pantalla_carga;
	public GameObject pantalla_menu;
	public GameObject pantalla_busqueda;
	public GameObject cabecera_puntuacion;

	public Sprite[] skins;
	public Sprite[] stats;
	public Sprite[] icons;
	public GameObject jugador;
	public GameObject barras;
	public GameObject icono;
	public GameObject nubes;
	private float  timeStartedLerping;
	public float LerpTime = 300.0f;
	private bool isLerping;
	private Vector3 startPosition;
	private Vector3 endPosition;
	private int puntero = 0;

	// Use this for initialization
	void Start () {
		pantalla_carga.SetActive (true);
		pantalla_menu.SetActive(false);
		pantalla_busqueda.SetActive(false);
		cabecera_puntuacion.SetActive (false);
		start_time = Time.time;
		jugador.GetComponent<Image> ().sprite = skins [puntero];
		barras.GetComponent<Image> ().sprite = stats [puntero];
		icono.GetComponent<Image> ().sprite = icons [puntero];
		timeStartedLerping = Time.time;
		startPosition = nubes.transform.position;
		endPosition = new Vector3 (-startPosition.x, startPosition.y, 0.0f);
		isLerping = true;
	}

	void Update(){
		if (Time.time > start_time + loading_time) {
			pantalla_carga.SetActive (false);
			pantalla_menu.SetActive(true);
		}

		if (isLerping) {
			float timeSinceStarted = Time.time - timeStartedLerping;
			float percentageComplete = timeSinceStarted / LerpTime;
			nubes.transform.position = Vector3.Lerp (startPosition, endPosition, percentageComplete);
			if (percentageComplete >= 0.95f) {
				isLerping = false;
				nubes.transform.position = endPosition;
			}
		}
	}
	//Display de menu de busqueda llamado desde menu principal
	public void battle(){
		pantalla_busqueda.SetActive (true);
		//CODIGO DE BUSQUEDA DE RIVAL
	}
	//Display de menu principal llamado desde menu de busqueda
	public void back(){
		pantalla_menu.SetActive(true);
		pantalla_busqueda.SetActive (false);
		//CODIGO CANCELAR BUSQUEDA DEL RIVAL
	}
	//Display dentro de partida llamado desde menu de busqueda
	public void match(){
		pantalla_busqueda.SetActive (false);
		cabecera_puntuacion.SetActive (true);
	}
	//Display de menu principal desde menu de partida
	public void menu(){
		pantalla_menu.SetActive(true);
		cabecera_puntuacion.SetActive (false);
	}

	//Funcion para cambiar el pj desde menu principal
	public void next(){
		++puntero;
		jugador.GetComponent<Image> ().sprite = skins [Mathf.Abs((puntero)%skins.Length)];
		barras.GetComponent<Image> ().sprite = stats [Mathf.Abs((puntero)%skins.Length)];
		icono.GetComponent<Image> ().sprite = icons [Mathf.Abs((puntero)%skins.Length)];
	}

	//Funcion para cambiar el pj desde menu principal
	public void previous(){
		--puntero;
		jugador.GetComponent<Image> ().sprite = skins [Mathf.Abs((puntero)%skins.Length)];
		barras.GetComponent<Image> ().sprite = stats [Mathf.Abs((puntero)%skins.Length)];
		icono.GetComponent<Image> ().sprite = icons [Mathf.Abs((puntero)%skins.Length)];
	}
}
