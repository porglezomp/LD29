using UnityEngine;
using System.Collections;

public class FutureBody {
	public Vector2d position;
	public Vector2d velocity;

	public FutureBody(Body b) {
		position = b.position;
		velocity = b.velocity;
	}
}

public class Body : MonoBehaviour {

	public Vector2d position;
	public Vector2d velocity;
	public double mass = 1;
	public LineRenderer lr;
	public Material lineRendererMaterial;

	// Use this for initialization
	protected void Start () {
		lr = gameObject.AddComponent(typeof(LineRenderer)) as LineRenderer;
		lr.SetColors(Color.grey, Color.black);
		lr.SetWidth(.1f, .05f);
		lr.material = lineRendererMaterial;
		position = new Vector2d(transform.position.x * Universe.scale, transform.position.y * Universe.scale);
		if (double.IsNaN(position.x) || double.IsNaN(position.y)) {
			position = new Vector2d(0, 0);
		}
		Universe.AddBody(this);
	}
	
	public void PhysicsStep () {
		position += velocity * Time.fixedDeltaTime;
	}

	public void OnDestroy () {
		Universe.RemoveBody(this);
	}
}
