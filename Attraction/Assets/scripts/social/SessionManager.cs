using UnityEngine;
using System.Collections;
using UnityEngine.SocialPlatforms;
using Types;
using Util;

#if UNITY_ANDROID
	using GooglePlayGames;
	using GooglePlayGames.BasicApi;
#endif

public class SessionManager : MonoBehaviour {

	public static SessionManager Instance;

	public delegate void RefreshDelegate();
	public static event RefreshDelegate RefreshSocialUsability;

	public string userName { get; private set; }
	public string userId { get; private set; }
	public bool validUser { 
		get { return Social.localUser.authenticated; }
	}

	SceneLoader scene;

	void Start() {
		if (Instance == null) {
			scene = SceneLoader.Instance;

#if UNITY_ANDROID
			ConfigureGooglePlay();
#elif !UNITY_ANDROID
			StartCoroutine("WaitToStart");
#endif

			Instance = this;
		}
	}

	IEnumerator WaitToStart() {
		yield return new WaitForSeconds(2);
		scene.LoadScene(GameScene.MENU);
	}

	void ConfigureGooglePlay() {
		PlayGamesClientConfiguration config = new PlayGamesClientConfiguration.Builder().Build();
		PlayGamesPlatform.InitializeInstance(config);
		PlayGamesPlatform.Activate();
		Social.localUser.Authenticate(ProcessAuthentication);
	}

	void ProcessAuthentication(bool success) {
		if (success) {
			userName = Social.localUser.userName;
			userId = Social.localUser.id;
		}
		StartCoroutine("WaitToStart");
	}

	IEnumerator Logout() {
		PlayGamesPlatform.Instance.SignOut();

		while (validUser) {
			yield return null;
		}

		Login();
	}

	void Login() {
		PlayGamesClientConfiguration config = new PlayGamesClientConfiguration.Builder().Build();
		PlayGamesPlatform.InitializeInstance(config);
		PlayGamesPlatform.Activate();
		Social.localUser.Authenticate((bool success) => {
			RefreshSocialUsability();
		});
	}

	public void HandleLogin() {

#if UNITY_ANDROID
		StopCoroutine("Logout");
		StartCoroutine("Logout");
#endif

	}

	public void ShowAchievements() {

#if UNITY_ANDROID
		Social.ShowAchievementsUI();
#endif

	}

	public void ShowLeaderboard() {

#if UNITY_ANDROID
		Social.ShowLeaderboardUI();
#endif

	}
}
