using UnityEngine;
using System.Collections;

public class ForceBox : MonoBehaviour {

	public Vector2 forceVector;
	Vector2d realForceVector;

	void Start () {
		realForceVector = new Vector2d(forceVector.x, forceVector.y);
	}

	void OnTriggerStay2D (Collider2D other) {
		Debug.Log ("One's here!");
		if (other.gameObject.GetComponent<Body>() is FallingBody) {
			Debug.Log ("It's a good one!");
			other.gameObject.GetComponent<Body>().velocity = realForceVector;
		}
	}
}
