using System;
using UnityEngine;
using System.Collections;
using Types;

public class MenuSceneController : MonoBehaviour {

	public float environmentChangeRate = 5;

	EnvironmentColorModule environment;

	void Start()
	{
		environment = EnvironmentColorModule.Instance;
		StartCoroutine("LoopEnvironmentColor");
	}

	IEnumerator LoopEnvironmentColor()
	{
		int colorCount = Enum.GetNames(typeof(GalaxyColor)).Length;
		int color = UnityEngine.Random.Range(0, colorCount);
		while (true)
		{
			environment.SetEnvironmentColor((GalaxyColor)color);
			color++;
			color = UnityEngine.Random.Range(0, colorCount);
			yield return new WaitForSeconds(environmentChangeRate);
		}
	}
}
