using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class TrackingCamera : MonoBehaviour {

	public Body target;
	Rect currentRect = new Rect(0, 0, 36, 50);

	// Use this for initialization
	void Start () {
	
	}

	static double epsilon = .0000000001;

	// Update is called once per frame
	void Update () {
		List<double> influences = new List<double>();
		List<Vector2> include = new List<Vector2>();
		List<Vector2> zoom = new List<Vector2>();
		double sunInfluence = 1;
		foreach (Body b in Universe.world.statics) {
			double distance = Vector2d.Distance(target.position, b.position);
			double influence = b.mass / (distance * distance) * epsilon;
			sunInfluence = influence;
		}
		foreach (Body b in Universe.world.planets) {
			double distance = Vector2d.Distance(target.position, b.position);
			double influence = b.mass / (distance * distance) * epsilon;
			influences.Add(influence);
		}
		for (int i = 0; i < influences.Count; i++) {
			double ratio = influences[i]/sunInfluence;
			if (ratio > .1) {
				include.Add (Universe.world.planets[i].transform.position);
				Debug.Log (Universe.world.planets[i].transform.position);
			}
			if (ratio > 1) {
				zoom.Add (Universe.world.planets[i].transform.position);
				Debug.Log (Universe.world.planets[i].transform.position);
			}
		}

		if (GameManager.gamePlaying) BuildRect(sunInfluence, include, zoom);
		else currentRect = RectLerp(currentRect, new Rect(0, 0, 36, 50), .1f);

		transform.position = currentRect.center;
		camera.orthographicSize = -currentRect.height * 0.5f;
	}

	void BuildRect(double sun, List<Vector2> include, List<Vector2> zoom) {
		if (sun < 2.5) {
			Debug.Log ("We're FREE!");
		}

		Vector2 focus = target.transform.position;
		float top = focus.y;
		float bot = focus.y;
		float left = focus.x;
		float right = focus.x;
		float padding;
		Rect newRect;

		if (zoom.Count > 0) { // Zoom on stuff
			foreach (Vector2 point in zoom) {
				if (point.y > top) top = point.y;
				else if (point.y < bot) bot = point.y;
				if (point.x < left) left = point.x;
				else if (point.x > right) right = point.x;
			}
			padding = 1;
		} else { // Show the normal screen
			Vector2 sunPos = Universe.world.statics[0].transform.position;
			if (sunPos.y > top) top = sunPos.y;
			else if (sunPos.y < bot) bot = sunPos.y;
			if (sunPos.x < left) left = sunPos.x;
			else if (sunPos.x > right) right = sunPos.x;

			foreach (Vector2 point in include) {
				if (point.y > top) top = point.y;
				else if (point.y < bot) bot = point.y;
				if (point.x < left) left = point.x;
				else if (point.x > right) right = point.x;
			}
			padding = 2;
		}

		top += padding; right += padding;
		bot -= padding; left -= padding;
		newRect = new Rect(left, top, right - left, bot - top); // LRWH
		currentRect = RectLerp(currentRect, newRect, .1f);
	}

	Rect RectLerp(Rect a, Rect b, float t) {
		float top = Mathf.Lerp (a.yMax, b.yMax, t);
		float bottom = Mathf.Lerp (a.yMin, b.yMin, t);
		float left = Mathf.Lerp (a.xMin, b.xMin, t);
		float w = Mathf.Lerp(a.width, b.width, t);
		float h = Mathf.Lerp(a.height, b.height, t);
		return new Rect(left, top, w, h);
	}
}
