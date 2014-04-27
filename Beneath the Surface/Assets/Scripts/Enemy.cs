using UnityEngine;
using System.Collections;

public class Enemy : FallingBody {

	void DrawRay(Vector2d start, Vector2d dir) { DrawRay(start, dir, Color.white); }
	void DrawRay(Vector2d start, Vector2d dir, Color color) {
		Debug.DrawRay(Universe.WorldToView(start), Universe.WorldToView(dir), color);
	}

	public float EngineForce = 100;
//	public PlayerShip target;
	public Body target;

	// Use this for initialization
	new void Start () {
		base.Start();
//		target = GameObject.FindObjectOfType(typeof(PlayerShip)) as PlayerShip;
	}

	void Thrust(Vector2d v) {
		velocity += EngineForce * v.normalized * Time.deltaTime;
	}

	// Update is called once per frame
	void Update () {
		transform.position = new Vector2((float) (position.x / Universe.scale), (float) (position.y / Universe.scale));
		Body majorBody = null; double max = 0;
		// Find what's pulling on you the most
		foreach (Body b in Universe.world.statics) {
			double distance = Vector2d.Distance(b.position, position);
			distance *= distance;
			double weight = b.mass / distance; // m/r^2
			if (weight > max) {
				max = weight;
				majorBody = b;
			}
		}
		foreach (Body b in Universe.world.planets) {
			double distance = Vector2d.Distance(b.position, position);
			distance *= distance;
			double weight = b.mass / distance; // m/r^2
			if (weight > max) {
				max = weight;
				majorBody = b;
			}
		}

		if (majorBody == null) return; // If there's no gravity, nothing'll work anyway

		// Compute ALL THE VECTORS <img src="https://i.imgflip.com/8etd0.jpg">
		Vector2d radius = (position - majorBody.position);
		Vector2d targetRadius = (target.position - majorBody.position);
/*!*/	DrawRay(majorBody.position, radius);
		DrawRay(majorBody.position, targetRadius);
		Vector2d majorGravity = -radius.normalized * max;
		Vector2d normal = Vector3d.Project(velocity, radius.normalized);
/*!*/	DrawRay(position, normal, Color.red);
		Vector2d tangent = velocity - normal;
/*!*/	DrawRay(position, tangent, Color.green);

		if (radius.magnitude < targetRadius.magnitude) { // We're below the target
			double dot = Vector2d.Dot(normal, radius.normalized);
			if (dot < 0 && tangent.magnitude < normal.magnitude) { // We're falling downwards
				Vector2d thrustVector;
				if (tangent.magnitude != 0) thrustVector = tangent;
				else thrustVector = new Vector2d(-radius.y, radius.x);
				Thrust(thrustVector);
			}
			
		} else { // We're above the target

		}
//		Vector2 targetDir = (target.transform.position - transform.position).normalized;
//		transform.LookAt(Universe.WorldToView(target.future[0].position), -Vector3.forward);
//		Debug.DrawRay(transform.position, transform.forward);
//		Debug.DrawRay(transform.position, targetDir);
//		if (Vector2.Dot (targetDir, transform.forward) > .8) {
//			velocity += new Vector2d(transform.forward.x, transform.forward.y) * Time.deltaTime * EngineForce;
//		}
	}
}
