using UnityEngine;
using System.Collections.Generic;
using UnityEditor;

public class RandomSizeTool : EditorWindow {

	Transform parentObject;
	float minValue = 0.1f, maxValue = 0.5f;
	float minLimit = 0.001f, maxLimit = 1;

	[MenuItem("Design Tools/Random Sizing Tool")]
	static void OpenWindow()
	{
		RandomSizeTool w = (RandomSizeTool)GetWindow(typeof(RandomSizeTool));
		w.Show();
	}

	void OnGUI()
	{
		EditorGUILayout.BeginHorizontal();
		GUILayout.Label("Parent Object");
		parentObject = (Transform)EditorGUILayout.ObjectField(parentObject, typeof(Transform), true);
		EditorGUILayout.EndHorizontal();
		EditorGUILayout.BeginHorizontal();
		GUILayout.Label("Size Threshold");
		EditorGUILayout.MinMaxSlider(ref minValue, ref maxValue, minLimit, maxLimit);
		EditorGUILayout.EndHorizontal();
		EditorGUILayout.BeginHorizontal();
		GUILayout.Label("Size Threshold => ");
		GUILayout.Label("Min: "+minValue);
		GUILayout.Label("Max: "+maxValue);
		EditorGUILayout.EndHorizontal();

		if (GUILayout.Button("Randomize"))
		{
			ApplyRandomSizes();
		}
	}

	void ApplyRandomSizes()
	{
		float randomSize = Random.Range(minValue, maxValue);

		foreach(Transform t in parentObject)
		{
			if (t == parentObject)
				return;
			t.localScale = Vector3.one * randomSize;
			randomSize = Random.Range(minValue, maxValue);
		}
	}
}
