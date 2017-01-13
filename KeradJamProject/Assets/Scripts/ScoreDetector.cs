using UnityEngine;
using System.Collections;

public class ScoreDetector : MonoBehaviour {

	public enum Side {PLAYER_ONE, PLAYER_TWO}
	public Side side;
	
	public int score;

	void OnTriggerEnter(Collider other)
	{
		if (other.tag == "Ball")
		{
			//SCORE LOGIC
			// Multiplayer: Which player?
			AddScore(side, score);
			ResetBallPosition();
		}
	}

	void ResetBallPosition(){}

	void AddScore(Side side, int score)
	{
		switch (side)
		{
			case Side.PLAYER_ONE:
			{
				Messenger<int>.Broadcast(Messages.PlayerTwoGoal, score);
			}
			;break;
			case Side.PLAYER_TWO:
			{
				Messenger<int>.Broadcast(Messages.PlayerOneGoal, score);
			}
			;break;
		}

	}
	
}
