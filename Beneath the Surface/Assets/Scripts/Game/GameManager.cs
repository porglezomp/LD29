using UnityEngine;
using System.Collections;

public class GameManager {
	public static bool gamePlaying = true;
	public static int score = 0;
	public static int highScore = 0;

	public static void ResetGame () {
		highScore = PlayerPrefs.GetInt("High Score");
		gamePlaying = true;
		score = 0;
	}

	public static void AddScore () {
		if (gamePlaying) {
			score++;
		}
	}

	public static void EndGame () {
		GameManager.gamePlaying = false;
		highScore = PlayerPrefs.GetInt("High Score");
		if (score > highScore) {
			highScore = score;
			PlayerPrefs.SetInt("High Score", score);
			PlayerPrefs.Save();
		}
	}
}
