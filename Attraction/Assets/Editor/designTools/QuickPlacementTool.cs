using UnityEngine;
using System.Collections;
using UnityEditor;

[CustomEditor(typeof(QuickPlacement))]
public class QuickPlacementTool : Editor {

	RaycastHit2D hitInfo;
	Ray ray;

	QuickPlacement script;


	void OnSceneGUI()
	{
		script = (QuickPlacement)target;
		Event e = Event.current;
		if (e.modifiers == EventModifiers.Shift) {
			if (e.type == EventType.MouseUp) {
				Debug.Log("click");
				ray = HandleUtility.GUIPointToWorldRay(e.mousePosition);
				Vector2 origin = new Vector2(ray.origin.x, ray.origin.y);
				Vector2 direction = new Vector2(ray.direction.x, ray.direction.y);
				hitInfo = Physics2D.Raycast(origin, direction, Mathf.Infinity, script.placementMask);
				if (hitInfo != null) {
					Debug.Log("create");
					Vector3 pos = hitInfo.point;
					GameObject newObj = Instantiate(script.prefab, pos, Quaternion.identity) as GameObject;
					if (script.parent != null)
						newObj.transform.parent = script.parent.transform;
				}
			}
		}
		else if (e.modifiers == EventModifiers.Control) {
			if (e.type == EventType.MouseUp) {
				ray = HandleUtility.GUIPointToWorldRay(e.mousePosition);
				ray = HandleUtility.GUIPointToWorldRay(e.mousePosition);
				Vector2 origin = new Vector2(ray.origin.x, ray.origin.y);
				Vector2 direction = new Vector2(ray.direction.x, ray.direction.y);
				hitInfo = Physics2D.Raycast(origin, direction, Mathf.Infinity, script.prefabMask);
				if (hitInfo != null) {
					DestroyImmediate(hitInfo.collider.gameObject);
				}
			}
		}
	}

}
