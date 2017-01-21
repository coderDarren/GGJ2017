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
			PlayerPrefs.SetInt(galaxy.ToString() + 0, -1);
			for (int i = 1; i <= 10; i++) {
				int value = i == 1 ? 0 : -1; //first level of the galaxy will always be available
				PlayerPrefs.SetInt(galaxy.ToString() + i.ToString(), value);
			}
		}
	}

	public int GetStatus(string pref)
	{
		return PlayerPrefs.GetInt(pref);
	}

	public int GetStatus(GalaxyType galaxy, int level)
	{
		return PlayerPrefs.GetInt(galaxy.ToString() + level.ToString());
	}

	public void CompleteProgress(GalaxyType galaxy, int level)
	{
		SetProgress(galaxy, level, 1);
	}

	public void SetProgress(GalaxyType galaxy, int level, int progress)
	{
		PlayerPrefs.SetInt(galaxy.ToString() + level.ToString(), progress);
	}
}
