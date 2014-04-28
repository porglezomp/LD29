using UnityEngine;
using System.Collections;

public class HideMessage : MonoBehaviour {

	public bool displayWhile;
	GUIText text;

	// Use this for initialization
	void Start () {
		text = gameObject.GetComponent<GUIText>();
	}
	
	// Update is called once per frame
	void Update () {
		if (GameManager.gamePlaying == displayWhile) {
			text.enabled = true;
		} else {
			text.enabled = false;
		}
	}
}
