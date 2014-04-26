using UnityEngine;
using UnityEditor;
using System.Collections;


[CustomEditor(typeof(RailsBody))]

public class RailsBodyEditor : Editor {

	public override void OnInspectorGUI () {
		DrawDefaultInspector();
	}

	public void OnSceneGUI () {
//		RailsBody t = target as RailsBody;
//		Vector2d sun = Universe.world.statics[0];
//		if (Distance(t.apo, sun) < Distance(t.peri, sun))
//		Vector2 point2 = new Vector2((float) (t.apo.x/Universe.scale), (float) (t.apo.y/Universe.scale));
//		point2 = Handles.PositionHandle(point2, Quaternion.identity);
//		//t.apo = new Vector2d(apo.x, apo.y)*Universe.scale;
//		//t.peri = new Vector2d(peri.x, peri.y)*Universe.scale;
//		if (GUI.changed)
//			EditorUtility.SetDirty (target);
	}
}
