using UnityEngine;
using System.Collections;

public class OpenURL : MonoBehaviour {

	public string URLString;
	public Color baseColor;
	public Color mouseOver = Color.white;
	
	void OnMouseOver () {
		guiTexture.color = mouseOver;
	}
	
	void OnMouseExit () {
		guiTexture.color = baseColor;
	}
	
	// Update is called once per frame
	void OnMouseDown () {
		Application.OpenURL(URLString);
	}
}
