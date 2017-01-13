using UnityEngine;
using System.Collections;

public class Audio : MonoBehaviour {
	AudioSource aus;

	public void PlaySoundOnce (AudioClip song){
		StartCoroutine (PlaySoundCoroutine (song));
	}
	IEnumerator PlaySoundCoroutine(AudioClip sound){
		aus = GetComponent<AudioSource> ();
		aus.PlayOneShot (sound);
		yield return new WaitForSeconds (sound.length);
		Destroy (gameObject);
	}
}
