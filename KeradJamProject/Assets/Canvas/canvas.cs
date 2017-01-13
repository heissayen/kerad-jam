using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class canvas : MonoBehaviour {

	private float start_time;
	public float loading_time = 3.0f;

	public GameObject pantalla_carga;
	public GameObject pantalla_menu;
	public GameObject pantalla_busqueda;
	public GameObject cabecera_menu;
	public GameObject cabecera_puntuacion;

	public Sprite[] skins;
	public Sprite[] stats;
	public GameObject jugador;
	public GameObject barras;
	private int puntero = 0;

	// Use this for initialization
	void Start () {
		pantalla_carga.SetActive (true);
		pantalla_menu.SetActive(false);
		pantalla_busqueda.SetActive(false);
		cabecera_menu.SetActive (false);
		cabecera_puntuacion.SetActive (false);
		start_time = Time.time;
		jugador.GetComponent<Image> ().sprite = skins [puntero];
		barras.GetComponent<Image> ().sprite = stats [puntero];
	}

	void Update(){
		if (Time.time > start_time + loading_time) {
			pantalla_carga.SetActive (false);
			pantalla_menu.SetActive(true);
			cabecera_menu.SetActive (true);
		}
	}

	public void battle(){
		pantalla_busqueda.SetActive (true);
		cabecera_menu.SetActive (false);
		//CODIGO DE BUSQUEDA DE RIVAL
	}

	public void back(){
		cabecera_menu.SetActive (true);
		pantalla_busqueda.SetActive (false);
		//CODIGO CANCELAR BUSQUEDA DEL RIVAL
	}

	public void next(){
		jugador.GetComponent<Image> ().sprite = skins [(puntero+1)%skins.Length];
		barras.GetComponent<Image> ().sprite = stats [(puntero+1)%skins.Length];
	}

	public void previous(){
		jugador.GetComponent<Image> ().sprite = skins [(puntero-1)%skins.Length];
		barras.GetComponent<Image> ().sprite = stats [(puntero-1)%skins.Length];
	}
}
