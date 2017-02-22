using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using Types;
using GooglePlayGames;
using GooglePlayGames.BasicApi;
using GooglePlayGames.BasicApi.Events;
using DebugServices;
using Util;

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

					if (USER_ID != string.Empty) loadJobs += 3;

					FetchEventForLocalStorage(attemptsEventId);
					FetchEventForLocalStorage(winsEventId);
					FetchEventForLocalStorage(starsEventId);
				}
			}

			//loop through ships
			for (int ship = 0; ship < 8; ship++) {
				ShipType shipType = (ShipType)ship;
				string shipEventId = GPGSUtil.ShipId(shipType);
				string shipLivesId = PrefsUtil.ShipLivesId(shipType);
				string playerShipId = PrefsUtil.MiscId(MiscType.PLAYER_SHIP_TYPE);

				FetchEventForLocalStorage(shipEventId);

				//remember, this is data that is set based on the idea that this user has never used this device before
				if (ship == 0) {
					IncrementEvent(shipEventId, 1); //purchase ship
					SaveLocalData(shipLivesId, 5); //give first ship full 20 lives
				}
				else 
					SaveLocalData(shipLivesId, 0); //give all other ships 0 lives

				//give the player a ship
				SaveLocalData(playerShipId, 0); //0 represents the first ship

				if (USER_ID != string.Empty) loadJobs++;
			}

			//loop through timestamps
			for (int timestamp = 0; timestamp < 8; timestamp++) {
				TimestampType timestampType = (TimestampType)timestamp;
				string timestampId = PrefsUtil.TimestampId(timestampType);

				TimeUtil.SaveDateTime(timestampId);
			}

			if (USER_ID != string.Empty) loadJobs += 5;

			//leaderboard stars, resources
			FetchLeaderboardForLocalStorage(Remnant.GPGSIds.leaderboard_stars_earned);
			FetchLeaderboardForLocalStorage(Remnant.GPGSIds.leaderboard_resources_earned);
			//event stars, resources
			FetchEventForLocalStorage(Remnant.GPGSIds.event_stars_earned);
			FetchEventForLocalStorage(Remnant.GPGSIds.event_resources_earned);
			FetchEventForLocalStorage(Remnant.GPGSIds.event_resources_spent);
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
		if (debug) Debugger.Log("DATA STORAGE EVENT: Retrieving data for " + GPGSUtil.GetIdDecrypted(dataId) + " => " +ret, DebugFlag.TASK);
		return ret;
	}

	public static void SaveLocalData(string dataId, int amount) {
		if (debug) Debugger.Log("DATA STORAGE EVENT: Saving data " +amount+ " to " +GPGSUtil.GetIdDecrypted(dataId), DebugFlag.TASK);
		PlayerPrefs.SetInt(USER_ID+dataId, amount);
	}

	public static void SaveTimeData(string dataId, string data) {
		if (debug) Debugger.Log("DATA STORAGE EVENT: Saving data " +data+ " to " +dataId, DebugFlag.TASK);
		PlayerPrefs.SetString(USER_ID+dataId, data);
	}

	public static string GetTimeData(string dataId) {
		return PlayerPrefs.GetString(USER_ID+dataId);
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
