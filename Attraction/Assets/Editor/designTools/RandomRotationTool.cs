using UnityEngine;
using System.Collections;
using UnityEditor;

public class RandomRotationTool : EditorWindow {

	Transform parentObject;
	float minYAngle;
	float maxYAngle;

	[MenuItem("Design Tools/Random Rotation Tool")]
	static void OpenWindow()
	{
		RandomRotationTool w = (RandomRotationTool)GetWindow(typeof(RandomRotationTool));
		w.Show();
	}

	void OnGUI()
	{
		EditorGUILayout.BeginHorizontal();
		GUILayout.Label("Parent Object");
		parentObject = (Transform)EditorGUILayout.ObjectField(parentObject, typeof(Transform), true);
		EditorGUILayout.EndHorizontal();
		EditorGUILayout.BeginHorizontal();
		GUILayout.Label("Min: ");
		minYAngle = EditorGUILayout.FloatField(minYAngle);
		EditorGUILayout.EndHorizontal();
		EditorGUILayout.BeginHorizontal();
		GUILayout.Label("Max: ");
		maxYAngle = EditorGUILayout.FloatField(maxYAngle);
		EditorGUILayout.EndHorizontal();

		if (GUILayout.Button("Randomize"))
		{
			ApplyRandomSizes();
		}
	}

	void ApplyRandomSizes()
	{
		float randomAngle = Random.Range(minYAngle, maxYAngle);

		foreach(Transform t in parentObject)
		{
			if (t == parentObject)
				return;
			t.eulerAngles = Vector3.forward * randomAngle;
			randomAngle = Random.Range(minYAngle, maxYAngle);
		}
	}
}
