using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class FixedBody : Body {

	// Use this for initialization
	void Start () {
		base.Start();
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public void Attract(List<FallingBody> dynamics) {
		foreach (FallingBody dyn in dynamics) {
			double delta = Vector2d.Distance(position, dyn.position);
			Vector2d vecDelta = (position - dyn.position).normalized / (delta * delta) * mass * 10000;
			dyn.velocity += vecDelta;
		}
	}
}
