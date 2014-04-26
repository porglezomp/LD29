using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

public class PlanetBody : Body {

	public Vector2 initialVel;
	public List<FutureBody> future = new List<FutureBody>();

	// Use this for initialization
	new void Start () {
		base.Start();
		velocity = new Vector2d(initialVel.x, initialVel.y);
	}

	public void StoreFuture() {
		Debug.Log ("Storing future!");
		future.Add(new FutureBody(this));
		Debug.Log (future.Count);
	}

	public void RestoreFuture(int num = -1) {
		if (num > -1 && num < future.Count) {
			position = future[num].position;
			velocity = future[num].velocity;
		} else {
			if (future.Count > 0) {
				position = future.Last().position;
				velocity = future.Last().velocity;
			}
		}
	}
	
	void Update () {
		transform.position = new Vector2((float) (position.x / Universe.scale), (float) (position.y / Universe.scale));
		if (lr) {
			lr.SetVertexCount(future.Count);
			for (int i = 0; i < future.Count; i++) {
				lr.SetPosition(i, new Vector3((float) (future[i].position.x / Universe.scale), (float) (future[i].position.y / Universe.scale), 1));
			}
		}
	}

	public void Attract(List<FallingBody> dynamics) {
		foreach (Body dyn in dynamics) {
			double delta = Vector2d.Distance(position, dyn.position);
			Vector2d vecDelta = (position - dyn.position).normalized / (delta * delta) * Universe.G * mass * Time.fixedDeltaTime;
			dyn.velocity += vecDelta;
		}
	}

//	public void Bod
}
