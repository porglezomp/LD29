using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class FallingBody : Body {

	// Use this for initialization
	new void Start () {
		base.Start();
	}

	void Update () {
		transform.position = new Vector2((float) (position.x / Universe.scale), (float) (position.y / Universe.scale));
	}
}
