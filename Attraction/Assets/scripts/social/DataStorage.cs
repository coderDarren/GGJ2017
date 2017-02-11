using UnityEngine;
using System.Collections;
using Types;
using GooglePlayGames;
using GooglePlayGames.BasicApi;
using GooglePlayGames.BasicApi.Events;

public class DataStorage {

	static string INITIALIZED = "INITIALIZED";
	static string USER_ID;
	
	public static bool LOADING_USER { get; private set; }

	public static void LoadUser(string user, bool reset) {

		USER_ID = user;

		//determine if this user has data stored on this device
		int localStorageStatus = PlayerPrefs.GetInt(USER_ID+INITIALIZED);

		if (localStorageStatus != 1 || reset) {

			LOADING_USER = true;

			//loop galaxies starting with index 2 (HOME)
			int galaxy = 2;
			for (galaxy = 2; galaxy <= 9; galaxy++) {
				GalaxyType galaxyType = (GalaxyType)galaxy;

				//loop through levels
				for (int level = 1; level <= 5; level++) {
					string attemptsEventId = GPGSUtil.GalaxyLevelAttemptsId(galaxyType, level);
					string winsEventId = GPGSUtil.GalaxyLevelWinsId(galaxyType, level);
					string starsEventId = GPGSUtil.GalaxyLevelStarsId(galaxyType, level);
					FetchEventForLocalStorage(attemptsEventId);
					FetchEventForLocalStorage(winsEventId);
					FetchEventForLocalStorage(starsEventId);
				}
			}

			//leaderboard stars, resources
			FetchLeaderboardForLocalStorage(Remnant.GPGSIds.leaderboard_stars_earned);
			FetchLeaderboardForLocalStorage(Remnant.GPGSIds.leaderboard_resources_earned);
			//event stars, resources
			FetchEventForLocalStorage(Remnant.GPGSIds.event_stars_earned);
			FetchEventForLocalStorage(Remnant.GPGSIds.event_resources_earned);
			//event ships
			FetchEventForLocalStorage(Remnant.GPGSIds.event_ships_purchased);

			PlayerPrefs.SetInt(USER_ID+INITIALIZED, 1);

			LOADING_USER = false;
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
		return PlayerPrefs.GetInt(USER_ID+dataId);
	}

	public static void SaveLocalData(string dataId, int amount) {
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
				fetchData = (int)data.Scores[0].value;
				SaveLocalData(leaderboardId, fetchData);
				//for (int i = 0; i < data.Scores.Length; i++) {
				//	Debug.LogError("SCORE DATA => "+data.Scores[i].value);
				//}
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
				//Debug.LogError("EVENT "+eventId+" VALUE IS " +fetchData);
		});
	}

}
