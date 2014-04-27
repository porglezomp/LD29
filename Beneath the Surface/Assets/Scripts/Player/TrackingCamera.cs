using UnityEngine;
using System.Collections;

public class TrackingCamera : MonoBehaviour {

	public Transform target;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		float fac = .01f * Time.deltaTime;
		float x = Mathf.Lerp(transform.position.x, target.position.x, fac);
		float y = Mathf.Lerp(transform.position.y, target.position.y, fac);
		transform.position = new Vector3(x, y, transform.position.z);
	}
}
