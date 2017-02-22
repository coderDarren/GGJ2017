﻿using UnityEngine;
using System.Collections;
using UnityEngine.SocialPlatforms;
using Types;
using Util;
using Menu;

using GooglePlayGames;
using GooglePlayGames.BasicApi;
using GooglePlayGames.BasicApi.Events;

public class SessionManager : MonoBehaviour {

	public static SessionManager Instance;

	public string userName { get; private set; }
	public string userId { get; private set; }
	public bool validUser { 
		get { return PlayGamesPlatform.Instance.IsAuthenticated(); }
	}

	SceneLoader scene;

	void Start() {
		if (Instance == null) {
			Instance = this;
			scene = SceneLoader.Instance;

#if UNITY_ANDROID
			ConfigureGooglePlay();
			Social.localUser.Authenticate(ProcessAuthentication);
#elif !UNITY_ANDROID
			StartCoroutine("WaitToStart");
#endif
		}
	}

	IEnumerator WaitToStart() {
		while (DataStorage.LOADING_USER) {
			yield return null;
		}
		ProgressManager.Instance.StartCoroutine("ProcessTimestamps");
		scene.LoadScene(GameScene.MENU);
	}

	void ConfigureGooglePlay() {
		PlayGamesClientConfiguration config = new PlayGamesClientConfiguration.Builder()
																			  .Build();
		PlayGamesPlatform.InitializeInstance(config);
		PlayGamesPlatform.DebugLogEnabled = false;
		PlayGamesPlatform.Activate();
	}

	void ProcessAuthentication(bool success) {
		if (success) {
			userName = Social.localUser.userName;
			userId = Social.localUser.id;
		} else {
			userName = "Guest";
			userId = string.Empty;
		}
		DataStorage.LoadUser(userId, false);
		//ProgressManager.Instance.AddResources(125000);
		StartCoroutine("WaitToStart");
	}

	void Logout() {
		PlayGamesPlatform.Instance.SignOut();
		userName = "Guest";
		userId = string.Empty;
		PageManager.Instance.TurnOffPage(PageType.SOCIAL, PageType.SOCIAL_LOAD);
	}

	void Login() {
		Social.localUser.Authenticate((success) => {
			if (success) {
				userName = Social.localUser.userName;
				userId = Social.localUser.id;
			} else {
				userName = "Guest";
				userId = string.Empty;
			}
			PageManager.Instance.TurnOffPage(PageType.SOCIAL, PageType.SOCIAL_LOAD);
		});
	}

	public void HandleLogin() {

		if (validUser) {
			Logout();
		}
		else {
			Login();
		}
	}

	public void ShowAchievements() {

		PlayGamesPlatform.Instance.ShowAchievementsUI();
	}

	public void ShowLeaderboard() {

		PlayGamesPlatform.Instance.ShowLeaderboardUI();
	}

	public void ShowLeaderboard(string leaderboardId) {

		PlayGamesPlatform.Instance.ShowLeaderboardUI(leaderboardId);
	}
}
