using UnityEngine;
using System.IO;
using System.Collections;
using System.Collections.Generic;

public class AudioManager : MonoBehaviour {

	public GameObject auxiliar;		//objeto para variables temporales
	public static AudioManager instance = null;

	public AudioClip[] sounds;
	public string[] names;

	/*
	void Start(){
		setAudioManager ("background_sound");
	}
	*/
	//Reproduccion simple, puede haber multiples
	public void playSound(string name){
		if (System.Array.IndexOf(names,name)>=0) {
			GameObject inst = Instantiate (auxiliar);
			AudioClip temp = sounds[System.Array.IndexOf(names,name)];
			inst.GetComponent<Audio> ().PlaySoundOnce (temp);
		}
	}
	//Sonido en loop, solo puede haber 1
	public void setAudioManager(string bgs_name){
		//Comprobamos que no haya otra instancia de AudioManager
		if (instance == null) instance = this;
		else 
			if (instance != this) 
				Destroy (gameObject);
		DontDestroyOnLoad (gameObject);

		//Repdroducimos el sonido de fondo
		AudioSource aus = GetComponent<AudioSource> ();
		aus.clip = sounds[System.Array.IndexOf(names,bgs_name)];
		aus.loop = true;
		aus.Play();
		Debug.Log (aus.clip.name);
	}
}
