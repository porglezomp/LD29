using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Universe : MonoBehaviour {

	public static float scale = 1000;

	public List<FallingBody> dynamics;
	public List<FixedBody> statics;
	// Use this for initialization
	void Start () {
		dynamics = new List<FallingBody>(Object.FindObjectsOfType(typeof(FallingBody)) as FallingBody[]);
		statics = new List<FixedBody>(Object.FindObjectsOfType(typeof(FixedBody)) as FixedBody[]);
	}
	
	// Update is called once per frame
	void FixedUpdate () {
		foreach (FixedBody f in statics) {
			f.Attract(dynamics);
		}
	}
}
