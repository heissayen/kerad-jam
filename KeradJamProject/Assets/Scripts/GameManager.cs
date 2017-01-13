using UnityEngine;
using System.Collections;

public class GameManager : MonoBehaviour {

	int p1Score;
	int p2Score;

	public ConnectAndJoinRandom connect;
	public GameObject p1, p2;

	// Use this for initialization
	void Awake () 
	{
		Messenger.AddListener(Messages.PlayerOneJoined, PlayerOneJoined);
		Messenger.AddListener(Messages.PlayerTwoJoined, PlayerTwoJoined);

		Messenger<int>.AddListener(Messages.PlayerOneGoal, AddScoreP1);
		Messenger<int>.AddListener(Messages.PlayerOneGoal, AddScoreP2);
		
	}

	void AddScoreP1(int score)
	{
		p1Score += score;
		p2.GetComponent<manager>().ResetBall();
	}
	void AddScoreP2(int score)
	{
		p2Score += score;
		p1.GetComponent<manager>().ResetBall();
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

		StartGame();
	}

	void SearchGame()
	{
		connect.createGame = true;
	}

	void StartGame()
	{
		//LoadGame
	}
}

