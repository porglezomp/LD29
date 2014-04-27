using UnityEngine;
using System.Collections;

public class CleanParticles : MonoBehaviour {
	
	void Update () {
		if (!particleSystem.IsAlive()) {
			Destroy(gameObject);
		}
	}
}
