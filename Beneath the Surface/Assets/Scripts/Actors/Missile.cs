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
			Vector2 vector = transform.forward * Time.deltaTime * missileForce;
			velocity += new Vector2d(vector.x, vector.y);
			fuel -= Time.deltaTime;
		}
		time -= Time.deltaTime;
		transform.position = Universe.WorldToView(position);
		if (time < 0) Detonate();
	}

	void OnTriggerEnter2D() {
		Detonate();
	}

	void Detonate() {
		GameObject.Instantiate(explosion, transform.position, Quaternion.identity);
		Destroy(gameObject);
	}
}
