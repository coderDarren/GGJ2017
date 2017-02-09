using UnityEngine;
using System.Collections;
using System;
using Types;

public class ProgressManager : MonoBehaviour {

	public static ProgressManager Instance;

	const string INITIALIZED = "PROGRESS_INITIALIZED";

	SessionManager session;

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
		session = SessionManager.Instance;
	}

	void InitializeProgress()
	{
		Array galaxies = Enum.GetValues(typeof(GalaxyType));
		foreach (GalaxyType galaxy in galaxies)
		{
			PlayerPrefs.SetInt(session.userId + galaxy.ToString() + "0", -1);
			for (int i = 1; i <= 10; i++) {
				int value = i == 1 ? 0 : -1; //first level of the galaxy will always be available
				PlayerPrefs.SetInt(session.userId + galaxy.ToString() + i.ToString(), value);
			}
		}
		PlayerPrefs.SetInt(session.userId + INITIALIZED, 1);
		PlayerPrefs.SetInt(session.userId + GalaxyType.HOME_GALAXY.ToString() + "0", 0); //home should always be available - especially after a progress reset
	}

	public void CheckResetProgress()
	{
		int init = GetStatus(INITIALIZED);
		if (init == 1)
			return;
		InitializeProgress();
	}

	public void ResetProgress()
	{
		InitializeProgress();
	}

	public int GetStatus(string pref)
	{
		return PlayerPrefs.GetInt(session.userId + pref);
	}

	public int GetStatus(GalaxyType galaxy, int level)
	{
		return PlayerPrefs.GetInt(session.userId + galaxy.ToString() + level.ToString());
	}

	public void CompleteProgress(GalaxyType galaxy, int level)
	{
		SetProgress(galaxy, level, 1);
	}

	public void SetProgress(GalaxyType galaxy, int level, int progress)
	{
		PlayerPrefs.SetInt(session.userId + galaxy.ToString() + level.ToString(), progress);
	}
}
