using UnityEngine;
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

	public delegate void RefreshDelegate();
	public static event RefreshDelegate RefreshSocialUsability;

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
		yield return new WaitForSeconds(2);
		scene.LoadScene(GameScene.MENU);
	}

	void ConfigureGooglePlay() {
		PlayGamesClientConfiguration config = new PlayGamesClientConfiguration.Builder()
																			  .Build();
		PlayGamesPlatform.InitializeInstance(config);
		PlayGamesPlatform.DebugLogEnabled = true;
		PlayGamesPlatform.Activate();
	}

	void ProcessAuthentication(bool success) {
		if (success) {
			userName = Social.localUser.userName;
			userId = Social.localUser.id;
		} else {
			userId = string.Empty;
		}
		DataStorage.LoadUser(userId, false);
		StartCoroutine("WaitToStart");
	}

	IEnumerator Logout() {
		yield return null;
		PlayGamesPlatform.Instance.SignOut();
		while (validUser) {
			yield return null;
		}
		Login();
	}

	void Login() {
		//ConfigureGooglePlay();
		Social.localUser.Authenticate((success) => {
			if (success) {
				userName = Social.localUser.userName;
				userId = Social.localUser.id;
			} else {
				userId = string.Empty;
			}
			DataStorage.LoadUser(userId, false);
			StopCoroutine("WaitToRefreshSocialUsability");
			StartCoroutine("WaitToRefreshSocialUsability");
		});
	}

	IEnumerator WaitToRefreshSocialUsability() {
		while (DataStorage.LOADING_USER) {
			yield return null;
		}
		RefreshSocialUsability(); 
	}

	public void HandleLogin() {

#if UNITY_ANDROID
		StopCoroutine("Logout");
		StartCoroutine("Logout");
#endif

	}

	public void ShowAchievements() {

		PlayGamesPlatform.Instance.ShowAchievementsUI();
	}

	public void ShowLeaderboard() {

		PlayGamesPlatform.Instance.ShowLeaderboardUI();
	}

}
