using UnityEngine;
using System.Collections;

public class PlayerShip : FallingBody {

	public float RotSpeed = 100;
	public float EngineForce = 100;
	public GameObject Explosion;
	public Renderer fire;

	// Use this for initialization
	new void Start () {
		base.Start();
	}

	// Update is called once per frame
	void Update () {
		lr.SetColors(new Color(.1f, .1f, .1f, 1f), Color.black);
		transform.position = new Vector2((float) (position.x / Universe.scale), (float) (position.y / Universe.scale));
		if (Input.GetAxis("Horizontal") != 0) {
			transform.Rotate(new Vector3(0, 0, -1), Input.GetAxis("Horizontal") * Time.deltaTime * RotSpeed, Space.World);
		}
		if (Input.GetAxis("Vertical") > 0) {
			velocity += new Vector2d(transform.forward.x, transform.forward.y) * Time.deltaTime * EngineForce;
			fire.enabled = true;
		} else {
			fire.enabled = false;
		}
	}

	void OnTriggerEnter2D (Collider2D other) {
		if (other.gameObject.tag != "Missile" && other.gameObject.tag != "Border") {
			GameObject.Instantiate(Explosion, transform.position, Quaternion.identity);
			Destroy(gameObject);
		}
	}

	new void OnDestroy () {
		base.OnDestroy();
		GameManager.EndGame();
	}
}
