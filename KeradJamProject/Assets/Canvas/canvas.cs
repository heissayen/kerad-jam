using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class canvas : MonoBehaviour {

	private float start_time;
	public float loading_time = 1.5f;
	bool loaded = false;

	public GameObject pantalla_carga;
	public GameObject pantalla_menu;
	public GameObject pantalla_busqueda;
	public GameObject cabecera_puntuacion;
	public GameObject winlose;

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

		Messenger.AddListener(Messages.StartGame, StartGame);
		Messenger.AddListener(Messages.Winner, DisplayWinner);
		Messenger.AddListener(Messages.Loser, DisplayLoser);
	}

	void Update(){
		if (Time.time > start_time + loading_time && !loaded) {
			pantalla_carga.SetActive (false);
			pantalla_menu.SetActive(true);
			loaded = true;
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
		Messenger.Broadcast(Messages.SearchGame);
	}
	//Display de menu principal llamado desde menu de busqueda
	public void back(){
		pantalla_menu.SetActive(true);
		pantalla_busqueda.SetActive (false);
		PhotonNetwork.Disconnect();
		//CODIGO CANCELAR BUSQUEDA DEL RIVAL
	}
	//Display dentro de partida llamado desde menu de busqueda
	public void match(){
		pantalla_busqueda.SetActive (false);
		cabecera_puntuacion.SetActive (true);
		winlose.SetActive(false);
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

	void StartGame()
	{
		pantalla_menu.SetActive(false);
		pantalla_busqueda.SetActive(false);
		cabecera_puntuacion.SetActive (true);
	}

	void DisplayWinner()
	{
		winlose.SetActive(true);
		winlose.transform.FindChild("w").gameObject.SetActive(true);
		winlose.transform.FindChild("ScoreP2").GetComponent<Text>().text = FindObjectOfType<GameManager>().p2Score.ToString();
		winlose.transform.FindChild("ScoreP1").GetComponent<Text>().text = FindObjectOfType<GameManager>().p1Score.ToString();
	}

	void DisplayLoser()
	{
		winlose.SetActive(true);
		winlose.transform.FindChild("l").gameObject.SetActive(true);
		winlose.transform.FindChild("ScoreP2").GetComponent<Text>().text = FindObjectOfType<GameManager>().p2Score.ToString();
		winlose.transform.FindChild("ScoreP1").GetComponent<Text>().text = FindObjectOfType<GameManager>().p1Score.ToString();
	}

	void OnDisable()
	{
		Messenger.RemoveListener(Messages.StartGame, StartGame);
		Messenger.RemoveListener(Messages.Winner, DisplayWinner);
		Messenger.RemoveListener(Messages.Loser, DisplayLoser);
	}
}
