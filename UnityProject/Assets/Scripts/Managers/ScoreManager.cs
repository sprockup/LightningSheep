using UnityEngine;
using System;
using System.Collections;

public class ScoreManager {

	// Variable to hold the score during game play
	private static int currentScore;
	private static int currentHighScore;
	
	// Use this for initialization
	public ScoreManager()
	{
		currentScore = 0;
		currentHighScore = PlayerPrefs.GetInt("High Score");
		if (PlayerPrefs.GetInt("High Score") <= 0)
		{
			currentHighScore = 0;
		}
	}

	// Returns the current score
	public static int GetScore()
	{
		return currentScore;
	}
	
	// Increments the score my the given amount
	//	Also returns the score if needed
	public static int AddToScore(float amountToAdd)
	{
		currentScore += (int)amountToAdd;
		
		if (currentScore > currentHighScore)
			currentHighScore = currentScore;
			
		return currentScore;
	}
	
	// Returns the current score
	public static int GetHighScore()
	{
		// We only call this function at the end of a level so store the highscore
		PlayerPrefs.SetInt("High Score", currentHighScore);
		
		return currentHighScore;
	}
	
}
