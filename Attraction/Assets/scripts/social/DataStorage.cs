﻿using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using Types;

#if UNITY_ANDROID
using GooglePlayGames;
using GooglePlayGames.BasicApi;
using GooglePlayGames.BasicApi.Events;
#endif

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
			for (int ship = 1; ship < 9; ship++) {				
				ShipType shipType = (ShipType)ship;
				string shipEventId = GPGSUtil.ShipId(shipType);
				string shipLivesId = PrefsUtil.ShipLivesId(shipType);
				
				FetchEventForLocalStorage(shipEventId);

				//remember, this is data that is set based on the idea that this user has never used this device before
				if (ship == 1) {
					IncrementEvent(shipEventId, 1); //purchase ship
					//SaveLocalData(shipLivesId, 5); //give first ship full 20 lives
				}
				//else 
				//SaveLocalData(shipLivesId, 0); //give all other ships 0 lives

				if (USER_ID != string.Empty) loadJobs++;
			}

			string playerShipId = PrefsUtil.MiscId(MiscType.PLAYER_SHIP_TYPE);
			//give the player a ship
			SaveLocalData(playerShipId, 0); //0 represents the first ship

			//loop through timestamps
			for (int timestamp = 0; timestamp < 8; timestamp++) {				
				TimestampType timestampType = (TimestampType)timestamp;
				string timestampId = PrefsUtil.TimestampId(timestampType);

				TimeUtil.SaveDateTime(timestampId);
			}

			//loop through tutorials
			for (int tutorial = 0; tutorial < 7; tutorial++) {				
				TutorialType tutorialType = (TutorialType)tutorial;
				string tutorialId = GPGSUtil.TutorialId(tutorialType);

				FetchEventForLocalStorage(tutorialId);

				if (USER_ID != string.Empty) loadJobs++;
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
		SaveLocalData(eventId, newValue);

		if (!SessionManager.Instance.validUser) {
			return;
		}

#if UNITY_ANDROID

		PlayGamesPlatform.Instance.Events.IncrementEvent(eventId, amount);

#elif UNITY_IOS

#elif !UNITY_ANDROID && !UNITY_IOS

#endif

	}

	public static void IncrementLevelAchievement(GalaxyType galaxy, int level, int amount) {
		
		string achievementId = GPGSUtil.GetGalaxyAchievementId(galaxy);
		if (achievementId == string.Empty) {
			return;
		}

#if UNITY_ANDROID

		PlayGamesPlatform.Instance.IncrementAchievement(
	 	   achievementId, amount, (success) => {
	 	   	if (success) {
	 	   		//PageManager.Instance.LoadPage(PageType.ACHIEVEMENT_PROGRESS);
	 	   		//AchievementProgressPage page = GameObject.FindObjectOfType<AchievementProgressPage>();
	 	   		//page.Configure(level - 1, level, 5);
	 	   	}
	   	});

#elif UNITY_IOS

#elif !UNITY_ANDROID && !UNITY_IOS

#endif
	}

	public static void IncrementAchievement(string achievementId, int amount) {
		
		if (achievementId == string.Empty) {
			return;
		}

#if UNITY_ANDROID

		PlayGamesPlatform.Instance.IncrementAchievement(
	 	   achievementId, amount, (success) => {
	 	   	if (success) {
	 	   		//PageManager.Instance.LoadPage(PageType.ACHIEVEMENT_PROGRESS);
	 	   		//AchievementProgressPage page = GameObject.FindObjectOfType<AchievementProgressPage>();
	 	   		//page.Configure(level - 1, level, 5);
	 	   	}
	   	});

#elif UNITY_IOS

#elif !UNITY_ANDROID && !UNITY_IOS

#endif
	}

	public static int GetLocalData(string dataId) {
		int ret = PlayerPrefs.GetInt(USER_ID+dataId);
		//if (debug) Debugger.Log("DATA STORAGE EVENT: Retrieving data for " + GPGSUtil.GetIdDecrypted(dataId) + " => " +ret, DebugFlag.TASK);
		return ret;
	}

	public static void SaveLocalData(string dataId, int amount) {
		//if (debug) Debugger.Log("DATA STORAGE EVENT: Saving data " +amount+ " to " +GPGSUtil.GetIdDecrypted(dataId), DebugFlag.TASK);
		PlayerPrefs.SetInt(USER_ID+dataId, amount);
	}

	public static void SaveTimeData(string dataId, string data) {
		//if (debug) Debugger.Log("DATA STORAGE EVENT: Saving data " +data+ " to " +dataId, DebugFlag.TASK);
		PlayerPrefs.SetString(USER_ID+dataId, data);
	}

	public static string GetTimeData(string dataId) {
		return PlayerPrefs.GetString(USER_ID+dataId);
	}

	static void FetchLeaderboardForLocalStorage(string leaderboardId) {
		if (!SessionManager.Instance.validUser) {
			//no valid user..no need to poll the cloud. initialize local
			if (leaderboardId == Remnant.GPGSIds.leaderboard_resources_earned) {
				SaveLocalData(leaderboardId, 500);
			}
			else {
				SaveLocalData(leaderboardId, 0);
			}
			return;
		}

		int fetchData = 0;

#if UNITY_ANDROID

		PlayGamesPlatform.Instance.LoadScores(
			leaderboardId,
			LeaderboardStart.PlayerCentered,
			1,
			LeaderboardCollection.Public,
			LeaderboardTimeSpan.AllTime,
			(data) => {	
				fetchData = (int)data.PlayerScore.value;

				//ensure player has some cash
				if (leaderboardId == Remnant.GPGSIds.leaderboard_resources_earned) {
					if (fetchData == 0) 
						ReportLeaderboardScore(leaderboardId, 500);
					else
						SaveLocalData(leaderboardId, fetchData);
				}
				else
					SaveLocalData(leaderboardId, fetchData);
				loadJobs--;
				Debugger.Log("Load Jobs => "+loadJobs, DebugFlag.STEP);
		});

#elif UNITY_IOS

#elif !UNITY_ANDROID && !UNITY_IOS

#endif

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

#if UNITY_ANDROID

		PlayGamesPlatform.Instance.Events.FetchEvent(
			DataSource.ReadCacheOrNetwork,
			eventId,
			(rs, e) => {
				fetchData = (int)e.CurrentCount; //store data

				SaveLocalData(eventId, fetchData);
				loadJobs--;
				Debugger.Log("Load Jobs => "+loadJobs, DebugFlag.STEP);
		});

#elif UNITY_IOS

#elif !UNITY_ANDROID && !UNITY_IOS

#endif

	}

}
