using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Universe : MonoBehaviour {

	public static float scale = 1000;
	public static Universe world;

	public List<FallingBody> dynamics;
	public List<FixedBody> statics;

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
		}
	}

	// Universe.AddBody instead of an instance
	public static void AddBody (Body b) { world._AddBody(b); }
	public void _AddBody (Body b) {
		if (b is FallingBody) {
			dynamics.Add(b as FallingBody);
		} else if (b is RailsBody) {
			// Later
		} else if (b is FixedBody) {
			statics.Add(b as FixedBody);
		}
	}

	public static void RemoveBody (Body b) { world._RemoveBody(b); }
	public void _RemoveBody (Body b) {
		if (b.GetType() == typeof(FallingBody)) {
			dynamics.Remove(b as FallingBody);
		} else if (b.GetType() == typeof(RailsBody)) {
			// Later
		} else if (b.GetType() == typeof(FixedBody)) {
			statics.Remove(b as FixedBody);
		}
	}
}
