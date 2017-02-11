using UnityEngine;
using System.Collections;
using System;
using Types;

public class ProgressManager : MonoBehaviour {

	public delegate void UIStatTextDelegate(int amount);
	public event UIStatTextDelegate UpdateStarsText;
	public event UIStatTextDelegate UpdateResourcesText;

	public static ProgressManager Instance;

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

	public void ResetProgress()
	{
		DataStorage.LoadUser(SessionManager.Instance.userId, true);
	}

	public GalaxyType PreviousGalaxy(GalaxyType galaxy) {
		switch (galaxy) {
			case GalaxyType.DAHKRI_GALAXY: return GalaxyType.HOME_GALAXY;
			case GalaxyType.KYDOR_GALAXY: return GalaxyType.VIDON_GALAXY;
			case GalaxyType.ZAX_GALAXY: return GalaxyType.XILYANTIPHOR_GALAXY;
			case GalaxyType.MALIX_GALAXY: return GalaxyType.RYKTAR_GALAXY;
			case GalaxyType.XILYANTIPHOR_GALAXY: return GalaxyType.DAHKRI_GALAXY;
			case GalaxyType.VIDON_GALAXY: return GalaxyType.ZAX_GALAXY;
			case GalaxyType.RYKTAR_GALAXY: return GalaxyType.KYDOR_GALAXY;
			default: 
				return GalaxyType.HOME_GALAXY;
		}
	}

	public GalaxyType NextGalaxy(GalaxyType galaxy) {
		switch (galaxy) {
			case GalaxyType.DAHKRI_GALAXY: return GalaxyType.XILYANTIPHOR_GALAXY;
			case GalaxyType.KYDOR_GALAXY: return GalaxyType.RYKTAR_GALAXY;
			case GalaxyType.ZAX_GALAXY: return GalaxyType.VIDON_GALAXY;
			case GalaxyType.HOME_GALAXY: return GalaxyType.DAHKRI_GALAXY;
			case GalaxyType.XILYANTIPHOR_GALAXY: return GalaxyType.ZAX_GALAXY;
			case GalaxyType.VIDON_GALAXY: return GalaxyType.KYDOR_GALAXY;
			case GalaxyType.RYKTAR_GALAXY: return GalaxyType.MALIX_GALAXY;
			default:
				return GalaxyType.MALIX_GALAXY;
		}
	}

	public bool GalaxyIsAvailable(GalaxyType galaxy)
	{
		if (galaxy == GalaxyType.HOME_GALAXY) return true;

		GalaxyType previousGalaxy = PreviousGalaxy(galaxy);
		int stars = GetLevelStars(previousGalaxy, 5);

		if (stars > 0) {
			return true;
		}

		return false;
	}

	public bool LevelIsAvailable(GalaxyType galaxy, int level) 
	{
		//if level is 1, return true
		if (level == 1) return true;

		//otherwise we need to check if the previous level has been completed
		int checkLevel = level - 1;
		int stars = GetLevelStars(galaxy, checkLevel);
		
		if (stars > 0) {
			return true;
		}

		return false;
	}

	public bool LevelHasBeenAttempted(GalaxyType galaxy, int level) 
	{
		string dataId = GPGSUtil.GalaxyLevelAttemptsId(galaxy, level);
		int attempts = DataStorage.GetLocalData(dataId);
		if (attempts > 0) {
			return true;
		}
		return false;
	}

	public int GetLevelStars(GalaxyType galaxy, int level)
	{
		string dataId = GPGSUtil.GalaxyLevelStarsId(galaxy, level);
		return DataStorage.GetLocalData(dataId);
	}

	public void MarkLevelAttempted(GalaxyType galaxy, int level)
	{
		//Debug.Log("GALAXY "+galaxy+" LEVEL "+level+" IS BEING ATTEMPTED");
		string dataId = GPGSUtil.GalaxyLevelAttemptsId(galaxy, level);
		DataStorage.IncrementEvent(dataId, 1);
	}

	public void MarkLevelStars(GalaxyType galaxy, int level, int stars)
	{
		//Debug.Log("GALAXY "+galaxy+" LEVEL "+level+" IS EARNING " +stars+ " STARS");
		string dataId = GPGSUtil.GalaxyLevelStarsId(galaxy, level);
		DataStorage.IncrementEvent(dataId, (uint)stars);
	}

	public void MarkLevelWins(GalaxyType galaxy, int level)
	{
		//Debug.Log("GALAXY "+galaxy+" LEVEL "+level+" HAS BEEN WON");
		string dataId = GPGSUtil.GalaxyLevelWinsId(galaxy, level);
		DataStorage.IncrementEvent(dataId, 1);
	}

	public void MarkLevelAchievement(GalaxyType galaxy, int level) 
	{
		//Debug.Log("GALAXY "+galaxy+" LEVEL "+level+" HAS BEEN ACHIEVED");
		DataStorage.IncrementLevelAchievement(galaxy, level, 1);
	}

	public int GetResources() {
		return DataStorage.GetLocalData(Remnant.GPGSIds.leaderboard_resources_earned);
	}

	public int GetStars() {
		return DataStorage.GetLocalData(Remnant.GPGSIds.leaderboard_stars_earned);
	}

	public void AddResources(uint amount) {
		int current = GetResources();
		SetResources((uint)current + amount);
		DataStorage.IncrementEvent(Remnant.GPGSIds.event_resources_earned, amount);
	}

	void SetResources(uint amount) {
		DataStorage.ReportLeaderboardScore(Remnant.GPGSIds.leaderboard_resources_earned, amount);
		try { UpdateResourcesText((int)amount); } catch(System.NullReferenceException e) {}
	}

	public void AddStars(uint amount) {
		int current = GetStars();
		SetStars((uint)current + amount);
		DataStorage.IncrementEvent(Remnant.GPGSIds.event_stars_earned, amount);
	}

	void SetStars(uint amount) {
		DataStorage.ReportLeaderboardScore(Remnant.GPGSIds.leaderboard_stars_earned, amount);
		try { UpdateStarsText((int)amount); } catch(System.NullReferenceException e) {}
	}
}
