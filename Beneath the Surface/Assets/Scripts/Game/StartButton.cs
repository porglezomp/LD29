using UnityEngine;
using System.Collections;

public class StartButton : MonoBehaviour {

	public Color baseColor;
	public Color mouseOver = Color.white;

	void OnMouseOver () {
		guiText.color = mouseOver;
	}

	void OnMouseExit () {
		guiText.color = baseColor;
	}

	void OnMouseDown () {
		Application.LoadLevel(1);
	}
}
