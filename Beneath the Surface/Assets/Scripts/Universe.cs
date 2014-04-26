using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Universe : MonoBehaviour {

	public static double scale = 1E3;
	public static Universe world;
	public static double G = 6.67E-10;

	public List<FallingBody> dynamics;
	public List<FixedBody> statics;
	public List<PlanetBody> planets;

	// Make the static variable world
	void Awake () {
		world = this;
	}
	// Use this for initialization
	void Start () {

	}

	// Update is called once per frame
	void FixedUpdate () {
		foreach (FixedBody f in statics) {
			f.Attract(dynamics);
			f.Attract(planets);
		}
		foreach (PlanetBody p in planets) {
			p.Attract(dynamics);
		}
	}

	// Universe.AddBody instead of an instance
	public static void AddBody (Body b) { world._AddBody(b); }
	public void _AddBody (Body b) {
		if (b is FallingBody) {
			dynamics.Add(b as FallingBody);
		} else if (b is PlanetBody) {
			planets.Add(b as PlanetBody);
		} else if (b is FixedBody) {
			statics.Add(b as FixedBody);
		}
	}

	public static void RemoveBody (Body b) { world._RemoveBody(b); }
	public void _RemoveBody (Body b) {
		if (b is FallingBody) {
			dynamics.Remove(b as FallingBody);
		} else if (b is PlanetBody) {
			planets.Remove(b as PlanetBody);
		} else if (b is FixedBody) {
			statics.Remove(b as FixedBody);
		}
	}
}
