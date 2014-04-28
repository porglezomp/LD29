using UnityEngine;
using System.Collections;

public class GameGUI : MonoBehaviour {

	public Color baseColor;
	public Color mouseOver = Color.white;
	public GUIText scoreBoard;
	public GUIText finalScore;
	public GUIText highScore;

	void Update () {
		scoreBoard.text = "SCORE: " + GameManager.score;
		finalScore.text = "FINAL SCORE: " + GameManager.score;
		highScore.text = "HIGH SCORE: " + GameManager.highScore;
	}

	void OnMouseOver () {
		guiText.color = mouseOver;
	}

	void OnMouseExit () {
		guiText.color = baseColor;
	}

	void OnMouseDown () {
		Application.LoadLevel(0);
	}
}
