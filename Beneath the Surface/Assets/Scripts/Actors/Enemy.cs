﻿using UnityEngine;
using System.Collections;

public class Enemy : FallingBody {

	void DrawRay(Vector2d start, Vector2d dir) { DrawRay(start, dir, Color.white); }
	void DrawRay(Vector2d start, Vector2d dir, Color color) {
		Debug.DrawRay(Universe.WorldToView(start), Universe.WorldToView(dir), color);
	}

	public float EngineForce = 100;
	Vector2d totalThrustVector;
	public GameObject SunParticles;
	float spawnTimer = 2;
	bool spawning = true;
	public Body target;

	// Use this for initialization
	new void Start () {
		base.Start();
		EnemySpawner.spawner.enemies.Add(this);
		target = GameObject.FindObjectOfType(typeof(PlayerShip)) as Body;
		gameObject.collider2D.enabled = false;
		Transform sun = Universe.world.statics[0].transform;
		Instantiate(SunParticles, transform.position, Quaternion.LookRotation(Vector3.forward, transform.position - sun.position));
	}

	void Thrust(Vector2d v, double weight = 1) {
		totalThrustVector += v.normalized * weight;
	}

	// Update is called once per frame
	void Update () {
		if (double.IsNaN(position.x) || double.IsNaN(position.y)) {
			position = new Vector2d(0, 0);
		}
		if (spawning) {
			if (spawnTimer > 0) {
				spawnTimer -= Time.deltaTime;
			} else {
				spawning = false;
				gameObject.collider2D.enabled = true;
			}
		}
		transform.position = new Vector2((float) (position.x / Universe.scale), (float) (position.y / Universe.scale));
		Body majorBody = null; double max = 0;
		Body minorBody = null; double second = 0;
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
			if (weight > second) {
				second = weight;
				minorBody = b;
			}
			if (weight > max) {
				second = max;
				max = weight;
				minorBody = majorBody;
				majorBody = b;
			}
		}

		if (GameManager.gamePlaying) { // Only go for the player if it exists
			Vector2d vectorToTarget = target.position - position;
			Vector2d radius = position - majorBody.position;
			DrawRay(position, vectorToTarget, Color.grey);
			if (vectorToTarget.magnitude < 3000) {
				Thrust(vectorToTarget, 1000/vectorToTarget.magnitude);
			}
			if (vectorToTarget.magnitude < radius.magnitude) {
				Thrust(vectorToTarget);
			}
		}

		if (majorBody == null) return; // If there's no gravity, give up
		
		RespondTo(majorBody, max);
		
		totalThrustVector *= 2; // Make the primary code more important than the secondary
		
		if (minorBody == null) return;
		
		RespondTo(minorBody, second);

//		Vector2 targetDir = (target.transform.position - transform.position).normalized;
//		transform.LookAt(Universe.WorldToView(target.future[0].position), -Vector3.forward);
//		Debug.DrawRay(transform.position, transform.forward);
//		Debug.DrawRay(transform.position, targetDir);
//		if (Vector2.Dot (targetDir, transform.forward) > .8) {
//			velocity += new Vector2d(transform.forward.x, transform.forward.y) * Time.deltaTime * EngineForce;
//		}
		
	}

	void RespondTo(Body b, double gforce) {
		// Compute ALL THE VECTORS <img src="https://i.imgflip.com/8etd0.jpg">
		Vector2d radius = position - b.position;
		Vector2d targetRadius = new Vector2d(0, 0);
		if (GameManager.gamePlaying) { 
			targetRadius = target.position - b.position;
			DrawRay(b.position, targetRadius);
		}
		
/*!*/	DrawRay(b.position, radius);

		Vector2d majorGravity = -radius.normalized * gforce;
		Vector2d normal = Vector3d.Project(velocity, radius.normalized);
/*!*/	DrawRay(position, normal, Color.red);
		Vector2d tangent = velocity - normal;
/*!*/	DrawRay(position, tangent, Color.green);

		double dot = Vector2d.Dot(normal, radius.normalized); // Dot product of normal velocity and radius (up or down?)
		if (!GameManager.gamePlaying || radius.magnitude < targetRadius.magnitude) { // We're below the target, or there isn't one
			if (dot < 0 && tangent.magnitude < normal.magnitude) { // We're falling downwards
				Vector2d thrustVector;
				if (tangent.magnitude != 0) thrustVector = tangent;
				else thrustVector = new Vector2d(-radius.y, radius.x);
				Thrust(thrustVector);
				Thrust(radius, 500/radius.magnitude);
			} else {
				
			}
		} else { // We're above the target
			if (dot < 0) { // We're falling downwards
				if (radius.magnitude > targetRadius.magnitude * 2) {
					Thrust(-normal);
				}
			} else { // We're getting higher
				Thrust(-normal);
			}
		}
	}

	new void OnDestroy() {
		base.OnDestroy();
		EnemySpawner.spawner.enemies.Remove(this);
	}

	void LateUpdate () {
		DrawRay(position, totalThrustVector*500);
		velocity += EngineForce * totalThrustVector.normalized * Time.deltaTime;
		totalThrustVector = Vector2d.zero;
	}

	void OnTriggerEnter2D (Collider2D other) {
		if (other.gameObject.tag == "Sun") {
			Phoenix(other.transform);
		} else if (other.gameObject.tag != "Border"){
			Crash(other.gameObject);
		}
	}

	void Crash (GameObject other) {
		Destroy(gameObject);
		GameManager.AddScore();
	}

	void Phoenix (Transform sun) {
		Instantiate(SunParticles, transform.position, Quaternion.LookRotation(Vector3.forward, transform.position - sun.position));
		Destroy(gameObject);
	}
}
