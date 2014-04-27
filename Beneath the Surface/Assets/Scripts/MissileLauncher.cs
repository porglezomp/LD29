using UnityEngine;
using System.Collections;

public class MissileLauncher : MonoBehaviour {

	public Missile missile;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		if (Input.GetButtonDown("Missile")) {
			Missile m = GameObject.Instantiate(missile, transform.position, transform.rotation) as Missile;
			m.velocity = gameObject.GetComponent<PlayerShip>().velocity;
		}
	}
}
