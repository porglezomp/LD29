using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

public class Oracle {
	public static int FutureSteps = 1000;

	public static void Step() {
		foreach (FixedBody f in Universe.world.statics) {
			f.Attract(Universe.world.planets);
		}
		foreach (PlanetBody p in Universe.world.planets) {
			p.PhysicsStep();
			p.StoreFuture();
		}
	}

	public static void CalculateFuture () {
		LoadFuture(-1); // Go all the way forwards
		Step();
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
