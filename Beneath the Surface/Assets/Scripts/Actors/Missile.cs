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
			foreach (FallingBody b in Universe.world.characters) {
				if (b is Enemy) {
					Vector2d deltad = b.position - position;
					Vector2 delta = new Vector2((float) deltad.x, (float) deltad.y).normalized;
					if (Vector2.Dot (delta, transform.forward) > .9) {
						vector += delta * .5f;
					}
				}
			}
			velocity += new Vector2d(vector.x, vector.y).normalized * Time.deltaTime * missileForce;
			fuel -= Time.deltaTime;
		}
		time -= Time.deltaTime;
		transform.position = Universe.WorldToView(position);
		if (time < 0) Detonate();
	}

	void OnTriggerEnter2D(Collider2D other) {
		if (other.gameObject.tag != "Player") {
			Detonate();
		}
	}

	void Detonate() {
		GameObject.Instantiate(explosion, transform.position, Quaternion.identity);
		Destroy(gameObject);
	}
}
