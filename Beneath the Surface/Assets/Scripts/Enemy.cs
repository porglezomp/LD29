using UnityEngine;
using System.Collections;

public class Enemy : FallingBody {

	public float EngineForce = 100;
	public PlayerShip target;

	// Use this for initialization
	new void Start () {
		base.Start();
		target = GameObject.FindObjectOfType(typeof(PlayerShip)) as PlayerShip;
	}
	
	// Update is called once per frame
	void Update () {
		transform.position = new Vector2((float) (position.x / Universe.scale), (float) (position.y / Universe.scale));
		Vector2 targetDir = (target.transform.position - transform.position).normalized;
		transform.LookAt(Universe.WorldToView(target.future[0].position), -Vector3.forward);
		Debug.DrawRay(transform.position, transform.forward);
		Debug.DrawRay(transform.position, targetDir);
		if (Vector2.Dot (targetDir, transform.forward) > .8) {
			velocity += new Vector2d(transform.forward.x, transform.forward.y) * Time.deltaTime * EngineForce;
		}
	}
}
