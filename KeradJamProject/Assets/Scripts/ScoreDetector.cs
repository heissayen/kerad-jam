using UnityEngine;
using System.Collections;

public class ScoreDetector : MonoBehaviour {

	public enum Side {PLAYER_ONE, PLAYER_TWO}
	public Side side;
	
	int score;
	public int Score{ get { return score;}  set { score = value;}}

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
				// add score to player2
			}
			;break;
			case Side.PLAYER_TWO:
			{
				// add score to player1
			}
			;break;
		}

	}
	
}
