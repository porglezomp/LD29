using UnityEngine;
using System.Collections;

public class FallingBody : Body {

	// Use this for initialization
	new void Start () {
		base.Start();
	}
	
	// Update is called once per frame
	void FixedUpdate () {
		position += velocity * Time.fixedDeltaTime;
	}

	void Update () {
		transform.position = new Vector2((float) (position.x / Universe.scale), (float) (position.y / Universe.scale));
	}
}
