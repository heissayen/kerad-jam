using UnityEngine;
using System.Collections;

public class GameManager : MonoBehaviour {

	int p1Score;
	int p2Score;

	public GameObject p1, p2;

	// Use this for initialization
	void Awake () 
	{
		Messenger.AddListener(Messages.PlayerOneJoined, PlayerOneJoined);
		Messenger.AddListener(Messages.PlayerTwoJoined, PlayerTwoJoined);
		
	}
	
	void PlayerOneJoined()
	{
		p1.GetComponent<movement>().enabled = true;
		p1.GetComponent<manager>().isMine = true;
		p1.GetComponent<Shooting>().isPlayerOne = true;
	}

	void PlayerTwoJoined()
	{
		p2.GetComponent<movement>().enabled = true;
		p2.GetComponent<manager>().isMine = true;
		p2.GetComponent<Shooting>().isPlayerOne = false;
	}
}

