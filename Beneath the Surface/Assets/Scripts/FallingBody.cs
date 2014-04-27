using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class FallingBody : Body {

	public FutureBody[] future = new FutureBody[Oracle.FutureSteps];

	// Use this for initialization
	new void Start () {
		base.Start();
	}

	void Update () {
		transform.position = new Vector2((float) (position.x / Universe.scale), (float) (position.y / Universe.scale));
		DrawFuture();
	}

	protected void DrawFuture() {
		if (lr) {
			lr.SetVertexCount(future.Length);
			for (int i = 0; i < future.Length; i++) {
				if (future[i] != null)
					lr.SetPosition(i, new Vector3((float) (future[i].position.x / Universe.scale), (float) (future[i].position.y / Universe.scale), 1));
			}
		}
	}

	public void StoreFuture(int loc) {
		future[loc] = new FutureBody(this);
	}

	public void RestoreFuture(int num = -1) {
		if (num > -1 && num < future.Length) {
			if (future[num] != null) {
				position = future[num].position;
				velocity = future[num].velocity;
			}
		} else {
			if (future.Length > 0 && future[future.Length - 1] != null) {
				position = future[future.Length - 1].position;
				velocity = future[future.Length - 1].velocity;
			}
		}
	}
}
