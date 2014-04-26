using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;

public class RailsBody : Body {

	public Vector2d point2;
	double MeanMotion;
//	bool rail = false;

	// Use this for initialization
	new void Start () {
		base.Start();
	}
	
	// Update is called once per frame
	void Update () {
//		if (!rail) { GenerateRail(); } // Make sure the rail is created after the scene is ready
		double t = Time.time;
		position = Rail(t);
	}

//	void GenerateRail() {
//		FixedBody sun = Universe.world.statics[0];
//		double a = Vector2d.Distance(apo, peri)/2;
//		MeanMotion = Math.Sqrt((sun.mass * Universe.G)/Math.Pow(a, 3));
//		rail = true;
//	}

	Vector2d Rail(double t) {
		return position;
	}
	
	public void Attract(List<FallingBody> dynamics) {
		foreach (FallingBody dyn in dynamics) {
			double delta = Vector2d.Distance(position, dyn.position);
			Vector2d vecDelta = (position - dyn.position).normalized / (delta * delta) * Universe.G * mass * Time.fixedDeltaTime;
			dyn.velocity += vecDelta;
		}
	}
}
