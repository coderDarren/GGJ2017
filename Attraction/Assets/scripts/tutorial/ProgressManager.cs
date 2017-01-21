using UnityEngine;
using System.Collections;
using System;
using Types;

public class ProgressManager : MonoBehaviour {

	public static ProgressManager Instance;

	const string INITIALIZED = "PROGRESS_INITIALIZED";

	void Awake()
	{
		if (Instance != null)
		{
			Destroy(gameObject);
		}
		else
		{
			Instance = this;
		}
	}

	void Start()
	{
		int init = GetStatus(INITIALIZED);
		if (init != 1)
			InitializeProgress();
	}

	void InitializeProgress()
	{
		Array galaxies = Enum.GetValues(typeof(GalaxyType));
		foreach (GalaxyType galaxy in galaxies)
		{
			PlayerPrefs.SetInt(galaxy.ToString(), 0);
		}
	}

	public int GetStatus(string pref)
	{
		return PlayerPrefs.GetInt(pref);
	}

	public int GetStatus(GalaxyType galaxy)
	{
		return PlayerPrefs.GetInt(galaxy.ToString());
	}

	public void CompleteProgress(GalaxyType galaxy)
	{
		PlayerPrefs.SetInt(galaxy.ToString(), 1);
	}
}
