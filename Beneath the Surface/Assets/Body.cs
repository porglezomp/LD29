using UnityEngine;
using System.Collections;

public class Body : MonoBehaviour {

	public Vector2d position;
	public Vector2d velocity;
	public double mass = 1;

	// Use this for initialization
	protected void Start () {
		position = new Vector2d(transform.position.x * Universe.scale, transform.position.y * Universe.scale);
		Universe.AddBody(this);
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
