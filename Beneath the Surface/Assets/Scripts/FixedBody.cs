using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class FixedBody : Body {

	// Use this for initialization
	new void Start () {
		base.Start();
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	// Attract a second body
	public void Attract(List<FallingBody> dynamics) {
		foreach (Body dyn in dynamics) {
			double delta = Vector2d.Distance(position, dyn.position);
			Vector2d vecDelta = (position - dyn.position).normalized / (delta * delta) * Universe.G * mass * Time.fixedDeltaTime;
			dyn.velocity += vecDelta;
//			dyn.StoreFuture();
		}
	}
	// Just copypaste the body of the code into here
	public void Attract(List<PlanetBody> dynamics) {
		foreach (Body dyn in dynamics) {
			double delta = Vector2d.Distance(position, dyn.position);
			Vector2d vecDelta = (position - dyn.position).normalized / (delta * delta) * Universe.G * mass * Time.fixedDeltaTime;
			dyn.velocity += vecDelta;
		}
	}
}
