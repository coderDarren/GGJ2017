using UnityEngine;
using System.Collections;
using System;
using Types;

public class ProgressManager : MonoBehaviour {

	public delegate void UIStatTextDelegate(int amount);
	public event UIStatTextDelegate UpdateStarsText;
	public event UIStatTextDelegate UpdateResourcesText;

	public static ProgressManager Instance;

	const string INITIALIZED = "PROGRESS_INITIALIZED";
	const string RESOURCES = Remnant.GPGSIds.leaderboard_resources_earned;
	const string STARS = Remnant.GPGSIds.leaderboard_stars_earned;

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

	void InitializeProgress()
	{
		Array galaxies = Enum.GetValues(typeof(GalaxyType));
		foreach (GalaxyType galaxy in galaxies)
		{
			PlayerPrefs.SetInt(SessionManager.Instance.userId + galaxy.ToString() + "0", -1);
			for (int i = 1; i <= 10; i++) {
				int value = i == 1 ? 0 : -1; //first level of the galaxy will always be available
				PlayerPrefs.SetInt(SessionManager.Instance.userId + galaxy.ToString() + i.ToString(), value);
			}
		}
		PlayerPrefs.SetInt(SessionManager.Instance.userId + INITIALIZED, 1);
		PlayerPrefs.SetInt(SessionManager.Instance.userId + GalaxyType.HOME_GALAXY.ToString() + "0", 0); //home should always be available - especially after a progress reset
		SetResources(0);
		SetStars(0);
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
		return PlayerPrefs.GetInt(SessionManager.Instance.userId + pref);
	}

	public int GetStatus(GalaxyType galaxy, int level)
	{
		return PlayerPrefs.GetInt(SessionManager.Instance.userId + galaxy.ToString() + level.ToString());
	}

	public void CompleteProgress(GalaxyType galaxy, int level)
	{
		SetProgress(galaxy, level, 1);
	}

	public void SetProgress(GalaxyType galaxy, int level, int progress)
	{
		PlayerPrefs.SetInt(SessionManager.Instance.userId + galaxy.ToString() + level.ToString(), progress);
	}

	public int GetResources() {
		if (SessionManager.Instance.validUser) {
			return SessionManager.Instance.userResources;	
		}
		return PlayerPrefs.GetInt(SessionManager.Instance.userId + RESOURCES);
	}

	public int GetStars() {
		if (SessionManager.Instance.validUser) {
			return SessionManager.Instance.userStars;	
		}
		return PlayerPrefs.GetInt(SessionManager.Instance.userId + STARS);
	}

	public void AddResources(uint amount) {
		int current = GetResources();
		SetResources((uint)current + amount);
		SessionManager.Instance.IncrementEvent(Remnant.GPGSIds.event_resources_earned, amount);
	}

	void SetResources(uint amount) {
		PlayerPrefs.SetInt(SessionManager.Instance.userId + RESOURCES, (int)amount);
		SessionManager.Instance.ReportResourceScore(amount);
		
		try { UpdateResourcesText((int)amount); } catch(System.NullReferenceException e) {}
	}

	public void AddStars(uint amount) {
		int current = GetStars();
		SetStars((uint)current + amount);
		SessionManager.Instance.IncrementEvent(Remnant.GPGSIds.event_stars_earned, amount);
	}

	void SetStars(uint amount) {
		PlayerPrefs.SetInt(SessionManager.Instance.userId + STARS, (int)amount);
		SessionManager.Instance.ReportStarScore(amount);

		try { UpdateStarsText((int)amount); } catch(System.NullReferenceException e) {}
	}
}
