using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class GameManager : MonoBehaviour {

	public int p1Score;
	public int p2Score;

	bool searching;

	public Text p1Text, p2Text;

	public ConnectAndJoinRandom connect;
	public GameObject p1, p2;

	// Use this for initialization
	void Awake () 
	{
		Messenger.AddListener(Messages.PlayerOneJoined, PlayerOneJoined);
		Messenger.AddListener(Messages.PlayerTwoJoined, PlayerTwoJoined);

		Messenger<int>.AddListener(Messages.PlayerOneGoal, AddScoreP1);
		Messenger<int>.AddListener(Messages.PlayerTwoGoal, AddScoreP2);
		
		Messenger.AddListener(Messages.SearchGame, SearchGame);

		p1Text.text = p1Score.ToString();
		p2Text.text = p2Score.ToString();
	}

	void OnDisable()
	{
		Messenger.RemoveListener(Messages.PlayerOneJoined, PlayerOneJoined);
		Messenger.RemoveListener(Messages.PlayerTwoJoined, PlayerTwoJoined);

		Messenger<int>.RemoveListener(Messages.PlayerOneGoal, AddScoreP1);
		Messenger<int>.RemoveListener(Messages.PlayerTwoGoal, AddScoreP2);
		
		Messenger.RemoveListener(Messages.SearchGame, SearchGame);
	}

	void Update()
	{
		if (searching)
		{
			if(PhotonNetwork.playerList.Length > 1)
			{
				StartGame();
				searching = false;
			}
				
		}
	}

	void AddScoreP1(int score)
	{
		p1Score += score;
		p1Text.text = p1Score.ToString();
		p2.GetComponent<manager>().ResetBall();
		CheckWinCondition();
	}
	void AddScoreP2(int score)
	{
		p2Score += score;
		p2Text.text = p2Score.ToString();
		p1.GetComponent<manager>().ResetBall();
		CheckWinCondition();
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

	void SearchGame()
	{
		connect.createGame = true;
		searching = true;
	}

	void StartGame()
	{
		//LoadGameScreen
		Messenger.Broadcast(Messages.StartGame);
	}

	void CheckWinCondition()
	{
		if (p1Score >= 15)
		{
			Messenger.Broadcast(Messages.PlayerOneWins);
			PhotonNetwork.Disconnect();
			if (p1.GetComponent<manager>().isMine)
			{
				Messenger.Broadcast(Messages.Winner);
			}
			else
			{
				Messenger.Broadcast(Messages.Loser);
			}
		}
		else if (p2Score >= 15)
		{
			Messenger.Broadcast(Messages.PlayerTwoWins);
			PhotonNetwork.Disconnect();

			if (p2.GetComponent<manager>().isMine)
			{
				Messenger.Broadcast(Messages.Winner);
			}
			else
			{
				Messenger.Broadcast(Messages.Loser);
			}
		}
	}
}

