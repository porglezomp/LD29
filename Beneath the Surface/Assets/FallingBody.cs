using UnityEngine;
using System.Collections;

public class FallingBody : Body {

	// Use this for initialization
	void Start () {
		base.Start();
	}
	
	// Update is called once per frame
	void FixedUpdate () {
		position += velocity * Time.deltaTime;
		Debug.Log(velocity.x);
	}

	void Update () {
		transform.position = new Vector2((float) position.x / Universe.scale, (float) position.y / Universe.scale);
	}
}
