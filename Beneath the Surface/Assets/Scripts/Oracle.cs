using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

public class Oracle {
	public static int FutureSteps = 500;

	public static void DynamicStep(int time) {
		foreach (PlanetBody p in Universe.world.planets) {
			p.RestoreFuture(time);
		}
		foreach (FixedBody f in Universe.world.statics) {
			f.Attract(Universe.world.dynamics);
		}
		foreach (PlanetBody p in Universe.world.planets) {
			p.Attract(Universe.world.dynamics);
		}
		foreach (FallingBody f in Universe.world.dynamics) {
			f.PhysicsStep();
			f.StoreFuture(time);
		}
	}

	public static void Step() {
		foreach (FixedBody f in Universe.world.statics) {
			f.Attract(Universe.world.planets);
		}
		foreach (PlanetBody p in Universe.world.planets) {
			p.PhysicsStep();
			p.StoreFuture();
		}
//		foreach (FixedBody f in statics) {
//			f.Attract(dynamics);
//			f.Attract(planets);
//		}
//		foreach (PlanetBody p in planets) {
//			p.Attract(dynamics);
//		}
	}

	public static void CalculateFuture () {
		LoadFuture(-1); // Go all the way forwards
		Step();
	}

	public static void Dynamics() {
		LoadFuture(0);
		for (int i = 0; i < FutureSteps; i++) {
			DynamicStep(i);
		}
		foreach (FallingBody f in Universe.world.dynamics) {
			f.RestoreFuture(0);
		}
		LoadFuture(0);
	}

	public static void LoadFuture(int time) {
		foreach (PlanetBody p in Universe.world.planets) {
			p.RestoreFuture(time);
		}
	}

	public static void ReturnToPresent() {
		LoadFuture(0);
	}
}
