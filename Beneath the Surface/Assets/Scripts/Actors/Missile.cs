using UnityEngine;
using System.Collections;

public class Missile : FallingBody {

	public GameObject explosion;
	public float fuel = 2;
	public float time = 5;
	public float missileForce = 1;
	Enemy target;

	// Use this for initialization
	new void Start () {
		base.Start();
	}
	
	// Update is called once per frame
	void Update () {
		while (fuel > 0) {
			Vector2 vector = transform.forward;
			float distance = 1000;
			Vector2 target = Vector2.zero;
			foreach (FallingBody b in Universe.world.characters) {
				if (b is Enemy) {
					float delta = Vector2.Distance(transform.position, b.transform.position);
					if (delta < 1) Detonate();
					if (delta < distance) {
						target = b.transform.position;
					}
				}
			}
			if (distance < 1000) {
				vector += (target - (Vector2) transform.position) * .1f;
			}
			velocity += (new Vector2d(vector.x, vector.y).normalized)* Time.deltaTime * missileForce;
			fuel -= Time.deltaTime;
		}
		time -= Time.deltaTime;
		transform.position = Universe.WorldToView(position);
		if (time < 0) Detonate();
	}

	void OnTriggerEnter2D(Collider2D other) {
		if (other.gameObject.tag != "Player" && other.gameObject.tag != "Border") {
			Detonate();
		}
	}

	void Detonate() {
		GameObject.Instantiate(explosion, transform.position, Quaternion.identity);
		Destroy(gameObject);
	}
}
