using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using Types;
using GooglePlayGames;
using GooglePlayGames.BasicApi;
using GooglePlayGames.BasicApi.Events;
using DebugServices;

public class DataStorage {

	public static bool LOADING_USER 
	{
		get { return loadJobs > 0 ? true : false; }
	}

	static string INITIALIZED = "INITIALIZED";
	static string USER_ID;

	static bool debug = false;
	static int loadJobs = 0;
	
	public static void LoadUser(string user, bool reset) {

		USER_ID = user;

		//determine if this user has data stored on this device
		int localStorageStatus = PlayerPrefs.GetInt(USER_ID+INITIALIZED);

		if (localStorageStatus != 1 || reset) {

			//loop galaxies starting with index 2 (HOME)
			int galaxy = 2;
			for (galaxy = 2; galaxy <= 9; galaxy++) {
				GalaxyType galaxyType = (GalaxyType)galaxy;

				//loop through levels
				for (int level = 1; level <= 5; level++) {
					string attemptsEventId = GPGSUtil.GalaxyLevelAttemptsId(galaxyType, level);
					string winsEventId = GPGSUtil.GalaxyLevelWinsId(galaxyType, level);
					string starsEventId = GPGSUtil.GalaxyLevelStarsId(galaxyType, level);

					loadJobs += 3;

					FetchEventForLocalStorage(attemptsEventId);
					FetchEventForLocalStorage(winsEventId);
					FetchEventForLocalStorage(starsEventId);
				}
			}

			loadJobs += 5;

			//leaderboard stars, resources
			FetchLeaderboardForLocalStorage(Remnant.GPGSIds.leaderboard_stars_earned);
			FetchLeaderboardForLocalStorage(Remnant.GPGSIds.leaderboard_resources_earned);
			//event stars, resources
			FetchEventForLocalStorage(Remnant.GPGSIds.event_stars_earned);
			FetchEventForLocalStorage(Remnant.GPGSIds.event_resources_earned);
			//event ships
			FetchEventForLocalStorage(Remnant.GPGSIds.event_ships_purchased);

			PlayerPrefs.SetInt(USER_ID+INITIALIZED, 1);
		}
	}

	public static void ReportLeaderboardScore(string leaderboardId, uint score) {
		if (!SessionManager.Instance.validUser) {
			SaveLocalData(leaderboardId, (int)score);
			return;
		}

		SaveLocalData(leaderboardId, (int)score);
		Social.ReportScore(score, leaderboardId, (bool success) => {});
	}

	public static void IncrementEvent(string eventId, uint amount) {

		int newValue = GetLocalData(eventId) + (int)amount;

		if (!SessionManager.Instance.validUser) {
			SaveLocalData(eventId, newValue);
			return;
		}

		PlayGamesPlatform.Instance.Events.IncrementEvent(eventId, amount);
		SaveLocalData(eventId, newValue);
	}

	public static void IncrementLevelAchievement(GalaxyType galaxy, int level, int amount) {
		
		string achievementId = GPGSUtil.GetGalaxyAchievementId(galaxy);
		if (achievementId == string.Empty) {
			return;
		}

		PlayGamesPlatform.Instance.IncrementAchievement(
	 	   achievementId, amount, (success) => {
	 	   	if (success) {
	 	   		//PageManager.Instance.LoadPage(PageType.ACHIEVEMENT_PROGRESS);
	 	   		//AchievementProgressPage page = GameObject.FindObjectOfType<AchievementProgressPage>();
	 	   		//page.Configure(level - 1, level, 5);
	 	   	}
	   	});
	}

	public static int GetLocalData(string dataId) {
		int ret = PlayerPrefs.GetInt(USER_ID+dataId);
		if (debug) Debugger.Log("Retrieving data for " + GPGSUtil.GetIdDecrypted(dataId) + " => " +ret, DebugFlag.TASK);
		return ret;
	}

	public static void SaveLocalData(string dataId, int amount) {
		if (debug) Debugger.Log("Saving data " +amount+ " to " +GPGSUtil.GetIdDecrypted(dataId), DebugFlag.TASK);
		PlayerPrefs.SetInt(USER_ID+dataId, amount);
	}

	static void FetchLeaderboardForLocalStorage(string leaderboardId) {
		if (!SessionManager.Instance.validUser) {
			//no valid user..no need to poll the cloud. initialize local
			SaveLocalData(leaderboardId, 0);
			return;
		}

		int fetchData = 0;

		PlayGamesPlatform.Instance.LoadScores(
			leaderboardId,
			LeaderboardStart.PlayerCentered,
			1,
			LeaderboardCollection.Public,
			LeaderboardTimeSpan.AllTime,
			(data) => {	
				fetchData = (int)data.PlayerScore.value;
				SaveLocalData(leaderboardId, fetchData);
				loadJobs--;
				Debugger.Log("Load Jobs => "+loadJobs, DebugFlag.STEP);
		});
	}

	/// <summary>
	/// Used to initialize data based on google cloud storage
	/// </summary>
	static void FetchEventForLocalStorage(string eventId) {
		if (!SessionManager.Instance.validUser) {
			//no valid user..no need to poll the cloud. initialize local
			SaveLocalData(eventId, 0);
			return;
		}

		int fetchData = 0;

		PlayGamesPlatform.Instance.Events.FetchEvent(
			DataSource.ReadCacheOrNetwork,
			eventId,
			(rs, e) => {
				fetchData = (int)e.CurrentCount; //store data
				SaveLocalData(eventId, fetchData);
				loadJobs--;
				Debugger.Log("Load Jobs => "+loadJobs, DebugFlag.STEP);
		});
	}

}
