using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class PlanetBody : Body {

	public Vector2 initialVel;

	// Use this for initialization
	new void Start () {
		base.Start();
		velocity = new Vector2d(initialVel.x, initialVel.y);
	}
	
	// Update is called once per frame
	void FixedUpdate () {
		position += velocity * Time.fixedDeltaTime;
	}
	
	void Update () {
		transform.position = new Vector2((float) (position.x / Universe.scale), (float) (position.y / Universe.scale));
	}

	public void Attract(List<FallingBody> dynamics) {
		foreach (Body dyn in dynamics) {
			double delta = Vector2d.Distance(position, dyn.position);
			Vector2d vecDelta = (position - dyn.position).normalized / (delta * delta) * Universe.G * mass * Time.fixedDeltaTime;
			dyn.velocity += vecDelta;
		}
	}
}
